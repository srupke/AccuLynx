﻿using AccuLynx.Twitter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AccuLynx.Twitter.Managers
{
    /// <summary>
    /// This class handles all the communication with the Twitter API.
    /// </summary>
    public sealed class TwitterApiManager
    {
        private static readonly TwitterApiManager _instance = new TwitterApiManager();

        public const string OauthVersion = "1.0";
        public const string OauthSignatureMethod = "HMAC-SHA1";
        public const string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public string ConsumerKey { set; get; }
        public string ConsumerKeySecret { set; get; }
        public string AccessToken { set; get; }
        public string AccessTokenSecret { set; get; }

        private TwitterApiManager() { }

        public static TwitterApiManager GetTwitterApiManager()
        {         
            return _instance;
        }

        #region Public Methods

        public void SetupInstance(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerKeySecret = consumerKeySecret;
            this.AccessToken = accessToken;
            this.AccessTokenSecret = accessTokenSecret;
        }

        public string GetTweets(string screenName, int count)
        {
            string resourceUrl = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json");

            var requestParameters = new SortedDictionary<string, string>
            {
                { "count", count.ToString() },
                { "screen_name", screenName }
            };

            var response = GetResponse(resourceUrl, TwitterRequestMethod.GET, requestParameters);

            return response;
        }

        public TwitterAnalysisPhraseModel SearchForPhrase(string phrase, int order)
        {
            this.SetupInstance(
                "y1nevIGaM8OxWGrulAUY4isFu"/*consumerKey*/,
                "dczIkRq7sjVX4JeO51PTi6kvS1MVC5mnFOljT4DLLBVemYA33i"/*consumerKeySecret*/,
                "955923380359876609-OwJ5ZImuldU0Kp9G6z1CUw0cpTsP0vu"/*accessToken*/,
                "Mqbf5OIaStvfgW5rj7UiEUPpeUF576Xl8YvgxzRY5lUrN"/*accessTokenSecret*/);

            var phraseModel = new TwitterAnalysisPhraseModel() { PhraseText = phrase, OrderId = order };
            var result = this.Search(phrase);
            dynamic jsonResult = JsonConvert.DeserializeObject(result);

            var jsonResultStatus = (JArray)jsonResult.statuses;
            var numberOfTweets = jsonResultStatus.Count;

            foreach (var tweet in jsonResultStatus)
            {
                

                var tweetCreatedAt = DateTime.ParseExact(tweet.Value<string>("created_at"), Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                var tweetText = tweet.Value<string>("text");
                var tweetTruncated = tweet.Value<bool>("truncated");
                var retweetCount = tweet.Value<int>("retweet_count");
                var favoriteCount = tweet.Value<int>("favorite_count");

                var user = tweet.Value<JObject>("user");

                var tweetUser = user.Value<string>("name");
                var tweetScreenName = user.Value<string>("screen_name");

                var tweetInfo = new TwitterAnalysisPhraseDetailModel();
                tweetInfo.User = tweetUser;
                tweetInfo.ScreenName = tweetScreenName;
                tweetInfo.TweetCreatedAt = tweetCreatedAt;
                tweetInfo.TweetText = tweetText;
                tweetInfo.TweetTextTruncated = tweetTruncated;
                tweetInfo.RetweetCount = retweetCount;
                tweetInfo.FavoriteCount = favoriteCount;

                phraseModel.Details.Add(tweetInfo);
            }

            return phraseModel;
        }

        #endregion

        #region Private Methods

        private string Search(string wordOrPhrase)
        {
            string resourceUrl = string.Format("https://api.twitter.com/1.1/search/tweets.json");

            var requestParameters = new SortedDictionary<string, string>
            {
                { "q", wordOrPhrase },
                { "lang", "en" },
                { "result_type", "popular" }, // popular, mixed, recent
                { "count", "100" }, // Max value is 100
                { "include_entities", "false" }
            };

            var response = GetResponse(resourceUrl, TwitterRequestMethod.GET, requestParameters);

            return response;
        }

        private string GetResponse(string resourceUrl, TwitterRequestMethod method, SortedDictionary<string, string> requestParameters)
        {
            ServicePointManager.Expect100Continue = false;
            WebRequest request = null;
            string resultString = string.Empty;

            if (method == TwitterRequestMethod.POST)
            {
                var postBody = requestParameters.ToWebString();

                request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = method.ToString();
                request.ContentType = "application/x-www-form-urlencoded";

                using (var stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                }
            }
            else if (method == TwitterRequestMethod.GET)
            {
                request = (HttpWebRequest)WebRequest.Create(resourceUrl + "?"
                    + requestParameters.ToWebString());
                request.Method = method.ToString();
            }
            else
            {
                //other verbs can be addressed here...
            }

            if (request != null)
            {
                var authHeader = CreateHeader(resourceUrl, method, requestParameters);
                request.Headers.Add("Authorization", authHeader);
                var response = request.GetResponse();

                using (var sd = new StreamReader(response.GetResponseStream()))
                {
                    resultString = sd.ReadToEnd();
                    response.Close();
                }
            }

            return resultString;
        }

        private string CreateOauthNonce()
        {
            return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        }

        private static string CreateOAuthTimestamp()
        {

            var nowUtc = DateTime.UtcNow;
            var timeSpan = nowUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            return timestamp;
        }

        private string CreateOauthSignature(string resourceUrl, TwitterRequestMethod method, string oauthNonce, string oauthTimestamp, SortedDictionary<string, string> requestParameters)
        {
            //firstly we need to add the standard oauth parameters to the sorted list
            requestParameters.Add("oauth_consumer_key", ConsumerKey);
            requestParameters.Add("oauth_nonce", oauthNonce);
            requestParameters.Add("oauth_signature_method", OauthSignatureMethod);
            requestParameters.Add("oauth_timestamp", oauthTimestamp);
            requestParameters.Add("oauth_token", AccessToken);
            requestParameters.Add("oauth_version", OauthVersion);

            var sigBaseString = requestParameters.ToWebString();

            var signatureBaseString = string.Concat
            (method.ToString(), "&", Uri.EscapeDataString(resourceUrl), "&",
                                Uri.EscapeDataString(sigBaseString.ToString()));

            //Using this base string, we then encrypt the data using a composite of the 
            //secret keys and the HMAC-SHA1 algorithm.
            var compositeKey = string.Concat(Uri.EscapeDataString(ConsumerKeySecret), "&",
                                             Uri.EscapeDataString(AccessTokenSecret));

            string oauthSignature;
            using (var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(
                    hasher.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
            }

            return oauthSignature;
        }
        
        private string CreateHeader(string resourceUrl, TwitterRequestMethod method, SortedDictionary<string,string> requestParameters)
        {
            var oauthNonce = CreateOauthNonce();
            var oauthTimestamp = CreateOAuthTimestamp();
            var oauthSignature = CreateOauthSignature(resourceUrl, method, oauthNonce, oauthTimestamp, requestParameters);

            //The oAuth signature is then used to generate the Authentication header. 
            const string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method =\"{1}\", " +
                                         "oauth_timestamp=\"{2}\", oauth_consumer_key =\"{3}\", " +
                                         "oauth_token=\"{4}\", oauth_signature =\"{5}\", " +
                                         "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                           Uri.EscapeDataString(oauthNonce),
                                           Uri.EscapeDataString(OauthSignatureMethod),
                                           Uri.EscapeDataString(oauthTimestamp),
                                           Uri.EscapeDataString(ConsumerKey),
                                           Uri.EscapeDataString(AccessToken),
                                           Uri.EscapeDataString(oauthSignature),
                                           Uri.EscapeDataString(OauthVersion)
                );

            return authHeader;
        }

        #endregion
    }
}

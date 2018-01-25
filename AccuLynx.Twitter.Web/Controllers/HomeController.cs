using AccuLynx.Twitter.Managers;
using AccuLynx.Twitter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace AccuLynx.Twitter.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTwitterAnalysis(TwitterAnalysisModel analysis)
        {
            var apiManager = TwitterApiManager.GetTwitterApiManager();

            apiManager.SetupInstance(
                "y1nevIGaM8OxWGrulAUY4isFu"/*consumerKey*/,
                "dczIkRq7sjVX4JeO51PTi6kvS1MVC5mnFOljT4DLLBVemYA33i"/*consumerKeySecret*/,
                "955923380359876609-OwJ5ZImuldU0Kp9G6z1CUw0cpTsP0vu"/*accessToken*/,
                "Mqbf5OIaStvfgW5rj7UiEUPpeUF576Xl8YvgxzRY5lUrN"/*accessTokenSecret*/);

            var result1 = apiManager.Search(analysis.WordOrPhrase1);


            dynamic jsonResult = JsonConvert.DeserializeObject(result1);

            var jsonResultStatus = (JArray)jsonResult.statuses;
            var numberOfTweets = jsonResultStatus.Count;

            if (numberOfTweets > 0)
            {
                var mostRecentTweetText = jsonResultStatus[0].Value<string>("text");
                var mostRecentTweetUser = jsonResultStatus[0].Value<JObject>("user");
            }

            var searchMetadata = (JObject)jsonResult.search_metadata;




            var result2 = apiManager.Search(analysis.WordOrPhrase2);

            var manager = TwitterDalManager.GetTwitterDalManager();

            manager.AddTwitterAnalysis(analysis);

            return Json(new { });
        }

        public ActionResult GetTwitterAnalysisList()
        {
            var manager = TwitterDalManager.GetTwitterDalManager();

            return Json(new { analysisList = manager.GetTwitterAnalysisList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
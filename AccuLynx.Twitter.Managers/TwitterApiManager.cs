namespace AccuLynx.Twitter.Managers
{
    /// <summary>
    /// This class handles all the communication with the Twitter API.
    /// </summary>
    public sealed class TwitterApiManager
    {
        private static readonly TwitterApiManager _instance = new TwitterApiManager();
        
        private TwitterApiManager() { }

        public static TwitterApiManager GetTwitterApiManager()
        {
            return _instance;
        }


    }
}

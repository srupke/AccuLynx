namespace AccuLynx.Twitter.Managers
{
    /// <summary>
    /// This class handles all the communication with the Twitter Dal Layer.
    /// </summary>
    public sealed class TwitterDalManager
    {
        private static readonly TwitterDalManager _instance = new TwitterDalManager();

        private TwitterDalManager() { }

        public static TwitterDalManager GetTwitterDalManager()
        {
            return _instance;
        }


    }
}

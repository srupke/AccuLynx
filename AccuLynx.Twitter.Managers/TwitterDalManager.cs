using System;
using System.Collections.Generic;
using System.Linq;
using AccuLynx.Twitter.Dal;
using AccuLynx.Twitter.Models;

namespace AccuLynx.Twitter.Managers
{
    /// <summary>
    /// This class handles all the communication with the Twitter Dal Layer.
    /// </summary>
    public sealed class TwitterDalManager
    {
        private static readonly TwitterDalManager _instance = new TwitterDalManager();
        private static TwitterContext db = new TwitterContext();


        private TwitterDalManager() { }

        public static TwitterDalManager GetTwitterDalManager()
        {
            return _instance;
        }

        public TwitterAnalysisModel AddTwitterAnalysis(TwitterAnalysisModel analysis)
        {
            db.Analysis.Add(analysis);
            db.SaveChanges();

            return analysis;
        }

        public List<TwitterAnalysisModel> GetTwitterAnalysisList()
        {
            return db.Analysis.ToList();
        }

        public TwitterAnalysisModel GetTwitterAnalysis(int id)
        {
            var result = db.Analysis.First(a => a.AnalysisId == id);

            db.Entry(result).Collection(p => p.Phrases).Load();

            foreach (var phrase in result.Phrases)
            {
                db.Entry(phrase).Collection(d => d.Details).Load();
            }

            return result;
        }
    }
}

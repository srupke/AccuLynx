using AccuLynx.Twitter.Managers;
using AccuLynx.Twitter.Models;
using AccuLynx.Twitter.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
        public ActionResult AddTwitterAnalysis(AnalysisModel analysis)
        {
            var twitterAnalysis = new TwitterAnalysisModel();

            twitterAnalysis.Description = string.Format("\"{0}\" vs \"{1}\"", analysis.WordOrPhrase1, analysis.WordOrPhrase2);
            twitterAnalysis.AnalyzedOn = DateTime.Now;

            var apiManager = TwitterApiManager.GetTwitterApiManager();

            var phrase1 = apiManager.SearchForPhrase(analysis.WordOrPhrase1, 1);
            twitterAnalysis.Phrases.Add(phrase1);

            var phase2 = apiManager.SearchForPhrase(analysis.WordOrPhrase2, 2);
            twitterAnalysis.Phrases.Add(phase2);

            var manager = TwitterDalManager.GetTwitterDalManager();

            manager.AddTwitterAnalysis(twitterAnalysis);

            return Json(new { });
        }

        public ActionResult GetTwitterAnalysisList()
        {
            var manager = TwitterDalManager.GetTwitterDalManager();

            return Json(new { analysisList = manager.GetTwitterAnalysisList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
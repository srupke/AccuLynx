using AccuLynx.Twitter.Managers;
using AccuLynx.Twitter.Models;
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
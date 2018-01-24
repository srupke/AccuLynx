using AccuLynx.Twitter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccuLynx.Twitter.Web.Controllers
{
    public class HomeController : Controller
    {
        private TwitterContext db = new TwitterContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTwitterAnalysis(TwitterAnalysis analysis)
        {
            db.Analysis.Add(analysis);
            db.SaveChanges();

            return Json(new { });
        }

        public ActionResult GetTwitterAnalysisList()
        {
            return Json(new { analysisList = db.Analysis.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
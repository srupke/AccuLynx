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
            var list = db.Analysis.ToList();

           return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SdlTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string test_string)
        {
            ViewBag.test = test_string;
            return View();
        }
    }
}
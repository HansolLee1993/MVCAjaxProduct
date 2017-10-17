using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAjax.Controllers
{
    public class SimpleAjaxController : Controller
    {
        // GET: SimpleAjax
        //
        // GET: /SimpleAjax/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PureJavaScriptAjax()
        {
            return View();
        }

        public ActionResult SimpleJqueryAjax()
        {
            return View();
        }

        public string ServerTimeAsString()
        {
            System.Threading.Thread.Sleep(2000); // sleep for 2 seconds
            return "Time on the server is " + DateTime.Now.ToLongTimeString();
        }

        public PartialViewResult ServerTimeAsPartialView()
        {
            System.Threading.Thread.Sleep(2000); // sleep for 2 seconds
            ViewBag.ServerTime = "Time on the server is " + DateTime.Now.ToLongTimeString();
            return PartialView("_ServerTimePartial");
        }


    }
}
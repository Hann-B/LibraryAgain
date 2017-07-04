using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAgain.Controllers
{
    /// <summary>
    /// Home Page Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Library Home Page
        /// </summary>
        /// <remarks>
        /// Library home page
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

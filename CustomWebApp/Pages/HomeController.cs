using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CustomWebApp.Pages {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
        [HttpGet("about"), HttpGet("about-us"), HttpGet("company-info"), HttpGet("who-are-we")]
        public ActionResult About() {
            return View("About");
        }
        [HttpGet("privacy-policy"), HttpGet("privacy.html"), HttpGet("privacy.htm"), HttpGet("privacy.asp"), HttpGet("privacy.aspx"), HttpGet("privacy.php"), HttpGet("privacy-privacy.html"), HttpGet("privacy-privacy.htm"), HttpGet("privacy-privacy.asp"), HttpGet("privacy-privacy.aspx"), HttpGet("privacy-privacy.php")]
        public ActionResult Privacy() {
            return RedirectToPage("/Privacy");
        }
    }
}
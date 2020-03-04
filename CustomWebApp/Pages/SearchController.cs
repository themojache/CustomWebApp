using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomWebApp.Models;

namespace CustomWebApp.Pages {
    //[Route("search")]
    public class SearchController : Controller {
        public IActionResult Index() {
            return View("Search", new Results {
                Message = "You sucessfully searched for nothing!"
            });
        }
        [HttpGet("Search")]
        public IActionResult Index(string q) {
            if(q == null || String.IsNullOrEmpty(q)) return Index();
            return View("Search", new Results {
                Message = $"Searching for \"{q}\""
            });
        }
    }
}
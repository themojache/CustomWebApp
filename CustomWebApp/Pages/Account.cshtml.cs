using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomWebApp.Pages {
    public class AccountModel : PageModel {
        public string Username;
        public string Secret;
        public void OnGet() {
            Username = Request.Cookies["User"];
            Secret = (Request.Cookies["Secret"] == null) ? null : String.Join(", ", Convert.FromBase64String(Request.Cookies["Secret"]));
        }
    }
}
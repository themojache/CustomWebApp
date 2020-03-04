using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomWebApp.Pages {
    public class LoginModel : PageModel {
        public string Username;
        public void OnGet() {
            Username = Request.Cookies["User"];
        }
    }
}

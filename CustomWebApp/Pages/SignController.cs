using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CustomWebApp.Pages {
    public class SignController : Controller {
        public IActionResult Index() {
            return View();
        }

        public void Set(string key, string value, int? expireTime = null) {
            var option = new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddSeconds(expireTime ?? 180) }; //Couple Minute Expiration
            Response.Cookies.Append(key, value, option);
        }
        public void Set(string key, string value) => Set(key, value);
        public void Set(string key, string value, TimeSpan expireTime) => Set(key, value, Convert.ToInt32(expireTime.TotalSeconds));

        [HttpGet("sign/out")]
        public IActionResult Out() {
            Response.Cookies.Delete("User");
            Response.Cookies.Delete("Secret");
            return View("Out");
        }
        [HttpGet("sign/in"), HttpGet("sign/create")]
        public IActionResult In() {
            return RedirectToPage("/Login");
        }
        [HttpPost("sign/in")][ValidateAntiForgeryToken]
        public IActionResult In(string s_user, string s_pass, string s_remember) {
            var output = new Models.User();
            if(String.IsNullOrEmpty(s_user) || String.IsNullOrEmpty(s_pass)) {
                ViewData["Message"] = ViewBag.Message = "Invalid Sign In Attempt";
            } else {
                Models.User temp = output.Validate_Login(s_user, s_pass);
                ViewData["Message"] = ViewBag.Message = temp.LoginResult;

                if(temp.LoggedInStatus && !ViewBag.Message.ToLower().StartsWith("invalid")) { //remove the if here if you want to login (even though it will describe the login as invalid
                    Set("User", temp.Username, TimeSpan.FromHours(1)); //Should hash username with something to say the least, ideally it wouldn't be stored in a cookie but some other userHash could be
                    Set("Secret", Convert.ToBase64String(Guid.NewGuid().ToByteArray(), Base64FormattingOptions.None), TimeSpan.FromMinutes(45));
                }
            }
            return View(output);
        }
        [HttpGet("sign/reg"), HttpGet("sign/register")]
        public IActionResult Reg() {
            return RedirectToPage("/Register");
        }
        [HttpPost("sign/reg")][ValidateAntiForgeryToken]
        public IActionResult Reg(string r_user, string r_pass, string r_passC) {
            var output = new Models.User();
            var passwordsDontMatch = r_pass != r_passC;
            if(String.IsNullOrEmpty(r_user) || String.IsNullOrEmpty(r_pass) || passwordsDontMatch) {
                ViewData["Message"] = ViewBag.Message = ("Invalid Registration Attempt" + (passwordsDontMatch ? " (Passwords did not match)" : String.Empty));
            } else {
                ViewData["Message"] = ViewBag.Message = $"Registration was {(output.Register(r_user, r_pass).isValid ? "successful" : "unsuccessful")}";
            }
            return View("Reg", output);
        }
    }
}
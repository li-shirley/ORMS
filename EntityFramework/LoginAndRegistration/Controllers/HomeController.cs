using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginAndRegistration.Controllers
{
    public class HomeController : Controller
    {

        private LoginAndRegistrationContext db;
        public HomeController(LoginAndRegistrationContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                return View("Success");
            }
            return View("Index");
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                return View("Success");
            }
            return View("Login");
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "is already in use. Please try another or login.");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FirstName", newUser.FirstName);
            
            return RedirectToAction("Success");
        }

        [HttpPost("/sign-in")]
        public IActionResult SignIn(LoginUser loginUser)
        {
            // if (ModelState.IsValid == false)
            // {
            //     return View("Login");
            // }
            if (ModelState.IsValid)
            {
                User dbUser = db.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

                if (dbUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Credentials");
                    return View("Login");
                } 

                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult comparePwResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (comparePwResult == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Credentials");
                    return View("Login");
                }

                HttpContext.Session.SetInt32("UserId", dbUser.UserId);
                HttpContext.Session.SetString("FirstName", dbUser.FirstName);

                return RedirectToAction("Success");
            }
            return View("Login");
        }

        [HttpGet("/success")]
        public IActionResult Success()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return View("Index");
            }
            return View("Success");
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

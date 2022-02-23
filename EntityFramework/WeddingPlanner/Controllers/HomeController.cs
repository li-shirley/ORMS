using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext db;
        public HomeController(WeddingPlannerContext context)
        {
            db = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                return View("Dashboard", "Wedding");
            }
            return View("Index");
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
            
            return RedirectToAction("Dashboard", "Wedding");
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                User dbUser = db.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

                if (dbUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Credentials");
                    return View("Index");
                } 

                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult comparePwResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (comparePwResult == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Credentials");
                    return View("Index");
                }

                HttpContext.Session.SetInt32("UserId", dbUser.UserId);

                return RedirectToAction("Dashboard", "Wedding");
            }
            return View("Index");
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

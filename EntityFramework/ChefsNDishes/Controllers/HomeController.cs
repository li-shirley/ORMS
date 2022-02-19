using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private ChefsNDishesContext db;
        public HomeController(ChefsNDishesContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> chefs = db.Chefs.Include(c => c.Dishes).ToList();
            return View("Index", chefs);
        }

        [HttpGet("/new")]
        public IActionResult NewChef()
        {
            return View("NewChef");
        }

        [HttpPost("/create")]
        public IActionResult CreateChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                if (db.Chefs.Any(c => c.FirstName == newChef.FirstName && c.LastName == newChef.LastName))
                {
                    ModelState.AddModelError("FirstName", "Chef already exists in our roster.");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("NewChef");
            }
            db.Chefs.Add(newChef);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/dishes")]
        public IActionResult Dishes()
        {
            List<Dish> dishes = db.Dishes.Include(c => c.Cook).ToList();
            return View("Dishes", dishes);
        }

        [HttpGet("/dishes/new")]
        public IActionResult NewDish()
        {
            ViewBag.Chefs = db.Chefs.ToList();
            return View("NewDish");
        }

        [HttpPost("/dishes/create")]
        public IActionResult CreateDish(Dish newDish)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Chefs = db.Chefs.ToList();
                return View("NewDish");
            }
            db.Dishes.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("Dishes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

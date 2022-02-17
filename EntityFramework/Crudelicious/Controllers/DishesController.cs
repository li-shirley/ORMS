using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crudelicious.Models;

namespace Crudelicious.Controllers
{
    public class DishesController : Controller
    {

        private CrudeliciousContext db;
        public DishesController(CrudeliciousContext context)
        {
            db = context;
        }

        [HttpGet("/dishes/new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost("/dishes/create")]
        public IActionResult Create(Dish newDish)
        {
            if (ModelState.IsValid == false)
            {
                return View("New");
            }
            db.Dishes.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new {area = ""});
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

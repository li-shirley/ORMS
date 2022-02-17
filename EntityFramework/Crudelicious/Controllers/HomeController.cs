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
    public class HomeController : Controller
    {

        private CrudeliciousContext db;
        public HomeController(CrudeliciousContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> allDishes = db.Dishes
            .OrderByDescending(dish => dish.CreatedAt)
            .ToList();
            return View("Index", allDishes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

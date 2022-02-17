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

        [HttpGet("/dishes/{dishId}")]
        public IActionResult Details(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if (dish == null)
            {
                return RedirectToAction("Index", "Home", new {area = ""});
            }
            return View("Details", dish);
        }

        [HttpPost("/dishes/delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if (dish != null)
            {
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        [HttpGet("/dishes/edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if (dish == null)
            {
                return RedirectToAction("Index", "Home", new {area = ""});
            }
            return View("Edit", dish);
        } 

        [HttpPost("/dishes/update/{dishId}")]
        public IActionResult Update(Dish editedDish, int dishId)
        {
            if (ModelState.IsValid == false)
            {
                return View("Edit", editedDish);
            }
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish == null)
            {
                return RedirectToAction("Index", "Home", new {area = ""});
            }
            dish.ChefName = editedDish.ChefName;
            dish.Name = editedDish.Name;
            dish.Calories = editedDish.Calories;
            dish.Tastiness = editedDish.Tastiness;
            dish.Description = editedDish.Description;
            dish.UpdatedAt = DateTime.Now;
            db.Dishes.Update(dish);
            db.SaveChanges();
            return RedirectToAction("Details", dishId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

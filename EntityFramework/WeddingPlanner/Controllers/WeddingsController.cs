using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class WeddingsController : Controller
    {
        private WeddingPlannerContext db;
        public WeddingsController(WeddingPlannerContext context)
        {
            db = context;
        }

        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool loggedIn
        {
            get
            {
                return uid != null;
            }
        }

        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Weddings = db.Weddings
                .Include(w => w.Host)
                .Include(w => w.Attendees)
                .ToList();
            return View("Dashboard");
        }

        [HttpGet("/weddings/new")]
        public IActionResult New()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("New");
        }

        [HttpPost("/weddings/create")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            if (ModelState.IsValid == false)
            {
                return View("New");
            }
            newWedding.UserId = (int)uid;
            db.Weddings.Add(newWedding);
            db.SaveChanges();
            return RedirectToAction("Details", new { weddingId = newWedding.WeddingId });
        }

        [HttpPost("/weddings/{weddingId}/delete")]
        public IActionResult Delete(int weddingId)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            
            Wedding wedding = db.Weddings
                .FirstOrDefault(w => w.WeddingId == weddingId);
            if (wedding != null)
            {
                db.Weddings.Remove(wedding);
                db.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost("/weddings/{weddingId}/rsvp")]
        public IActionResult Rsvp(int weddingId)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            
            UserWeddingRSVP alreadyRSVP = db.UserWeddingRSVPs
                .FirstOrDefault(r => r.WeddingId == weddingId && (int)uid == r.UserId);

            if (alreadyRSVP == null)
            {
                UserWeddingRSVP rsvp = new UserWeddingRSVP()
                {
                    WeddingId = weddingId,
                    UserId = (int)uid
                };

                db.UserWeddingRSVPs.Add(rsvp);
            }

            else
            {
                db.UserWeddingRSVPs.Remove(alreadyRSVP);
            }
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/weddings/{weddingId}/details")]
        public IActionResult Details(int weddingId)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Wedding wedding = db.Weddings
                .Include(w => w.Host)
                .Include(w => w.Attendees)
                .ThenInclude(wa => wa.User)
                .FirstOrDefault(w => w.WeddingId == weddingId);
            
            if (wedding == null)
            {
                return RedirectToAction("Dashboard");
            }
            return View("Details", wedding);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

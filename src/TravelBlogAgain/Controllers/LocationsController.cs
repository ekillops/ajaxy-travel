using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBlogAgain.Models;



namespace TravelBlogAgain.Controllers
{
    public class LocationsController : Controller
    {
        private TravelBlogAgainContext db = new TravelBlogAgainContext();
        public IActionResult Index()
        {
            return View(db.Locations
                .Include(locations => locations.Experiences)
                .ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

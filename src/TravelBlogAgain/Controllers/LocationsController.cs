using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBlogAgain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TravelBlogAgain.Controllers
{

    public class LocationsController : Controller
    {
        private readonly TravelBlogAgainContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LocationsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogAgainContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Locations
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
            _db.Locations.Add(location);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            return View(thisLocation);
        }
        [HttpPost]
        public IActionResult Edit(Location location)
        {
            _db.Entry(location).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            return View(thisLocation);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            _db.Locations.Remove(thisLocation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

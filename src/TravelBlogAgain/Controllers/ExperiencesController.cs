using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlogAgain.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlogAgain.Controllers
{
    public class ExperiencesController : Controller
    {

        private TravelBlogAgainContext db = new TravelBlogAgainContext();
        public IActionResult Details(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExpId == id);
            var persons = db.Experience_Person
                .Include(exp_per => exp_per.Person)
                .Where(exp_per => exp_per.ExperienceId == id)
                .ToList();
            ViewBag.Persons = persons;
            return View(thisExperience);
        }
    }
}

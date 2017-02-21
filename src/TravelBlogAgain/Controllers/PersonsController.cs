using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlogAgain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TravelBlogAgain.Controllers
{
    public class PersonsController : Controller
    {
        private readonly TravelBlogAgainContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PersonsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogAgainContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Details(int id)
        {
            var thisPerson = _db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            var experiences = _db.Experience_Person
                .Include(exp_per => exp_per.Experience)
                .Where(e_p => e_p.PersonId == id)
                .ToList();
            ViewBag.Experiences = experiences;
            return View(thisPerson);
        }

        public IActionResult Create()
        {
            ViewBag.ExperiencesList = _db.Experiences;
            return View();
        }
      
        [HttpPost]
        public IActionResult Create(Person person, int[] experience_person)
        {
            _db.Persons.Add(person);
            _db.SaveChanges();
            for (int i = 0; i<experience_person.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(experience_person[i], person.PersonId);
                _db.Experience_Person.Add(newJoin);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Locations");
        }

        public IActionResult Edit(int id)
        {
            var thisPerson = _db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            var allExperiences = _db.Experiences.ToList();
            List<Experience> expList = new List<Experience> { };

            var thisPersonJoins = _db.Experience_Person
                .Include(join => join.Experience)
                .Where(exp_per => exp_per.PersonId == id)
                .ToList();

            foreach (var join in thisPersonJoins)
            {
                var exp = _db.Experiences.FirstOrDefault(ex => ex.ExpId == join.ExperienceId);
                expList.Add(exp);
            }
            

            var unlinkedExp = _db.Experiences.ToList().Except(expList).ToList();

            ViewBag.joinList = thisPersonJoins;
            ViewBag.unlinkList = unlinkedExp;
                       
            return View(thisPerson);

        }

        [HttpPost]
        public IActionResult Edit(Person person, int[] linked_experience_person, int[] unlinked_experience)
        {
            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
            for (int i = 0; i < linked_experience_person.Length; i++)
            {
                var thisExpPer = _db.Experience_Person.FirstOrDefault(expper => expper.Experience_PersonId == linked_experience_person[i]);
                _db.Experience_Person.Remove(thisExpPer);
            }
            for (int i = 0; i < unlinked_experience.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(unlinked_experience[i], person.PersonId);
                _db.Experience_Person.Add(newJoin);
            }
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = person.PersonId });
        }
        public IActionResult Delete(int id)
        {
            var thisPerson = _db.Persons
                .FirstOrDefault(p => p.PersonId == id);
            return View(thisPerson);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPerson = _db.Persons.FirstOrDefault(ps => ps.PersonId == id);
            _db.Persons.Remove(thisPerson);
            _db.SaveChanges();
            return RedirectToAction("Index", "Locations");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlogAgain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            ViewBag.PersonList = db.Persons;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Experience experience, int[] experience_person)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            for (int i = 0; i < experience_person.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(experience.ExpId, experience_person[i] );
                db.Experience_Person.Add(newJoin);
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Locations");
        }

        public IActionResult Edit(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExpId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");

            var allPersons = db.Persons.ToList();
            List<Person> personList = new List<Person> { };

            var thisJoins = db.Experience_Person
                .Include(join => join.Person)
                .Where(exp_per => exp_per.ExperienceId == id)
                .ToList();

            foreach (var join in thisJoins)
            {
                var person = db.Persons.FirstOrDefault(p => p.PersonId == join.PersonId);
                personList.Add(person);
            }


            var unlinkedPersons = db.Persons.ToList().Except(personList).ToList();

            ViewBag.joinList = thisJoins;
            ViewBag.unlinkList = unlinkedPersons;
            return View(thisExperience);

        }

        [HttpPost]
        public IActionResult Edit(Experience experience, int[] linked_person, int[] unlinked_person)
        {
            db.Entry(experience).State = EntityState.Modified;
            db.SaveChanges();

            for (int i = 0; i < linked_person.Length; i++)
            {
                var thisExpPer = db.Experience_Person.FirstOrDefault(expper => expper.Experience_PersonId == linked_person[i]);
                db.Experience_Person.Remove(thisExpPer);
            }

            for (int i = 0; i < unlinked_person.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(experience.ExpId, unlinked_person[i]);
                db.Experience_Person.Add(newJoin);
            }
            db.SaveChanges();

            return RedirectToAction("Details", new { id = experience.ExpId });
        }
        public IActionResult Delete(int id)
        {
            var thisExperience = db.Experiences
                .Include(exp => exp.Location)
                .FirstOrDefault(exps => exps.ExpId == id);
            return View(thisExperience);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisExp = db.Experiences.FirstOrDefault(exps => exps.ExpId == id);
            db.Experiences.Remove(thisExp);
            db.SaveChanges();
            return RedirectToAction("Index", "Locations");
        }
    }
}

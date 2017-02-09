using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlogAgain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlogAgain.Controllers
{
    public class PersonsController : Controller
    {
        private TravelBlogAgainContext db = new TravelBlogAgainContext();

        public IActionResult Details(int id)
        {
            var thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            var experiences = db.Experience_Person
                .Include(exp_per => exp_per.Experience)
                .Where(e_p => e_p.PersonId == id)
                .ToList();
            ViewBag.Experiences = experiences;
            return View(thisPerson);
        }

        public IActionResult Create()
        {
            ViewBag.ExperiencesList = db.Experiences;
            return View();
        }
      
        [HttpPost]
        public IActionResult Create(Person person, int[] experience_person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
            for (int i = 0; i<experience_person.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(experience_person[i], person.PersonId);
                db.Experience_Person.Add(newJoin);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Locations");
        }

        public IActionResult Edit(int id)
        {
            List<Experience> thisPersonExperiences = new List<Experience> { };

            List<Experience> unlinkedExperienceList = new List<Experience> { };

            var thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);

            var thisPersonJoins = db.Experience_Person
                .Where(exp_per => exp_per.PersonId == id);

            foreach (var join in thisPersonJoins)
            {
                thisPersonExperiences.Add(db.Experiences.Where(exp => exp.ExpId == join.ExperienceId))
            }
            ViewBag.linkedExperiences = db.Experiences
                .Where(exp => exp.ExpId == id);

            
            ViewBag.unlinkedExperiences = db.Experiences.ToList()
                .Except(thisPersonExperiences.Experience)
                .ToList(); 
                       
            return View(thisPerson);

        }

        [HttpPost]
        public IActionResult Edit(Person person, int[] linked_experience_person, int[] unlinked_experience_person)
        {
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
            for (int i = 0; i < linked_experience_person.Length; i++)
            {
                var thisExpPer = db.Experience_Person.FirstOrDefault(expper => expper.Experience_PersonId == linked_experience_person[i]);
                db.Experience_Person.Remove(thisExpPer);
            }
            for (int i = 0; i < unlinked_experience_person.Length; i++)
            {
                Experience_Person newJoin = new Experience_Person(unlinked_experience_person[i], person.PersonId);
                db.Experience_Person.Add(newJoin);
            }
            db.SaveChanges();
            return RedirectToAction("Details", new { id = person.PersonId });
        }
    }
}

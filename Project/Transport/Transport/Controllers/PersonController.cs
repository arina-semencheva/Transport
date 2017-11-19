using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transport.DAO.Person;
using Transport.Models;

namespace Transport.Controllers
{
    public class PersonController : Controller
    {
        PersonDAO _personDAO = new PersonDAO();

        // GET: Person
        public async Task<ActionResult> Index()
        {
            var persons = await _personDAO.GetPersons();
            return View(persons);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int personId)
        {
            var person = await _personDAO.GetPersonById(personId);
            return View(person);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PersonViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _personDAO.EditPerson(model);
            }
            else
                throw new Exception("Модель для изменения не определена");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int personId)
        {
            var person = await _personDAO.GetPersonById(personId);
            return View(person);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(PersonViewModel model)
        {
            if (ModelState.IsValid && model != null)

                await _personDAO.DeletePerson(model.PersonId);
            else
                throw new Exception("Входной параметр не определен");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PersonViewModel model)
        {
            if (ModelState.IsValid && model != null)
                await _personDAO.CreatePerson(model);
            else
                throw new Exception("Пустая модель");
            return RedirectToAction("Index");
        }

    }
}
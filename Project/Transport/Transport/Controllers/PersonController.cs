using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transport.DAO.Person;
using Transport.DataModel;
using Transport.Models;

namespace Transport.Controllers
{
    public class PersonController : Controller
    {
        PersonDAO _personDAO = new PersonDAO();
        TransportDBEntities _edm = new TransportDBEntities();

        // GET: Person
        public async Task<ActionResult> Index()
        {
            var persons = await _personDAO.GetPersons();
            return View(persons);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int personId)
        {
            var transports = _edm.Transports.Select(x => new TransportViewModel
            {
                TransportId = x.TransportId,
                TransportName = x.Name
            }).ToList();
            var tss = new SelectList(transports, "TransportId", "TransportName");
            var personTypes = _edm.PersonTypes.Select(x => new PersonTypeViewModel
            {
                PersonTypeId = x.PersonTypeId,
                PersonTypeName = x.Name
            });
            var pts = new SelectList(personTypes, "PersonTypeId", "PersonTypeName");
            ViewBag.Transports = tss;
            ViewBag.PersonTypes = pts;
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
            var transports = _edm.Transports.Select(x => new TransportViewModel
            {
                TransportId = x.TransportId,
                TransportName = x.Name
            }).ToList();
            var tss = new SelectList(transports, "TransportId", "TransportName");
            var personTypes = _edm.PersonTypes
                .Where(x => x.PersonTypeId != 3)
                .Select(x => new PersonTypeViewModel
                {
                    PersonTypeId = x.PersonTypeId,
                    PersonTypeName = x.Name
                });
            var pts = new SelectList(personTypes, "PersonTypeId", "PersonTypeName");
            ViewBag.Transports = tss;
            ViewBag.PersonTypes = pts;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transport.DAO;
using Transport.Models;

namespace Transport.Controllers
{
    public class TransportController : Controller
    {
        private TransportDAO _transport = new TransportDAO();

        public TransportController()
        {

        }

        // GET: Transport
        public async Task<ActionResult> Index()
        {
            var transports = await _transport.GetTransports();
            return View(transports);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int transportId)
        {
            var transport = await _transport.GetTransportById(transportId);
            return View(transport);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TransportViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _transport.EditTransport(model);
            }
            else
                throw new Exception("Модель для изменения не определена");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int transportId)
        {
            var transport = await _transport.GetTransportById(transportId);
            return View(transport);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(TransportViewModel model)
        {
            if (ModelState.IsValid && model != null)

                await _transport.DeleteTransport(model.TransportId);
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
        public async Task<ActionResult> Create(TransportViewModel model)
        {
            if (ModelState.IsValid && model != null)
                await _transport.CreateTransport(model);
            else
                throw new Exception("Пустая модель");
            return RedirectToAction("Index");
        }

    }
}
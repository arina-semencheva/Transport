using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transport.DAO;
using Transport.DataModel;
using Transport.Models;

namespace Transport.Controllers
{
    public class TransportController : Controller
    {
        private TransportDAO _transport = new TransportDAO();
        private TransportDBEntities _edm = new TransportDBEntities();
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
            var fuels = _edm.Fuels.Select(x => new FuleViewModel
            {
                FuelId = x.FueldId,
                Name = x.FuelName
            }).ToList();
            var fss = new SelectList(fuels, "FuelId", "Name");
            ViewBag.Fuels = fss;
            var transportTypes = _edm.TransportTypes.Select(x => new TransporttypeViewModel
            {
                TransportTypeId = x.TransportTypeId,
                TransportTypeName = x.TransportTypeName
            }).ToList();
            var tt = new SelectList(transportTypes, "TransportTypeId", "TransportTypeName");
            ViewBag.TransportTypes = tt;
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

                await _transport.DeleteTransport(model.TransportId.Value);
            else
                throw new Exception("Входной параметр не определен");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var fuels = _edm.Fuels.Select(x => new FuleViewModel
            {
                FuelId = x.FueldId,
                Name = x.FuelName
            }).ToList();
            var fss = new SelectList(fuels, "FuelId", "Name");
            var transportTypes = _edm.TransportTypes.Select(x => new TransporttypeViewModel
            {
                TransportTypeId = x.TransportTypeId,
                TransportTypeName = x.TransportTypeName
            })
            .ToList();
            var tt = new SelectList(transportTypes, "TransportTypeId", "TransportTypeName");
            ViewBag.Fuels = fss;
            ViewBag.TransportTypes = tt;
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
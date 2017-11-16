using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transport.DAO.RouteDAO;
using Transport.DataModel;
using Transport.Models;

namespace Transport.Controllers
{
    public class RouteController : Controller
    {

        //private IRouteDAO _routeDAO;
        private RouteDAO _routeDAO = new RouteDAO();

        public RouteController()
        {

        }

        //public RouteController(RouteDAO routeDAO)
        //{
        //    _routeDAO = routeDAO;
        //}

        public async Task<ActionResult> Index()
        {
            var routes = await _routeDAO.GetRoutes();
            return View(routes);
        }

        [HttpGet]
        public ActionResult Edit(int routeId)
        {
            return View(routeId);
        }

        [HttpPost]
        public async Task<ActionResult> EditRoute(RouteViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _routeDAO.EditRoute(model);
            }
            RedirectToAction("Index");
            return null;
        }

        [HttpGet]
        public ActionResult Delete(int routeId)
        {
            return View(routeId);
        }

        public async Task<ActionResult> DeleteRoute(int routeId)
        {
            if (routeId > 0)
                await _routeDAO.DeleteRoute(routeId);
            RedirectToAction("Index");
            return null;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RouteViewModel model)
        {
            if (ModelState.IsValid && model != null)
                await _routeDAO.CreateRoute(model);
            RedirectToAction("Index");
            return null;
        }

    }
}
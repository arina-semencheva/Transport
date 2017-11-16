﻿using System;
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

        private RouteDAO _routeDAO = new RouteDAO();

        public RouteController()
        {

        }

        public async Task<ActionResult> Index()
        {
            var routes = await _routeDAO.GetRoutes();
            return View(routes);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int routeId)
        {
            var route = await _routeDAO.GetRouteById(routeId);
            return View(route);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RouteViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _routeDAO.EditRoute(model);
            }
            else
                throw new Exception("Модель для изменения не определена");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int routeId)
        {
            var route = await _routeDAO.GetRouteById(routeId);
            return View(route);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RouteViewModel route)
        {
            if (ModelState.IsValid && route != null)

                await _routeDAO.DeleteRoute(route.RouteId);
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
        public async Task<ActionResult> Create(RouteViewModel model)
        {
            if (ModelState.IsValid && model != null)
                await _routeDAO.CreateRoute(model);
            else
                throw new Exception("Пустая модель");
            return RedirectToAction("Index");
        }

    }
}
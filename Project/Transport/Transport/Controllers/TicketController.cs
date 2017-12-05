using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.DAO.Ticket;
using Transport.DataModel;
using Transport.Models;

namespace Transport.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        TransportDBEntities _edm = new TransportDBEntities();
        TicketDAO _ticket = new TicketDAO();

        [Authorize(Roles = "Dispetcher,Aministration,Client")]
        public ActionResult Index(int? personId = default(int?))
        {          
            List<TicketViewModel> tickets = new List<TicketViewModel>();
            if (User.IsInRole("Client"))
            {
                var currentUser = User.Identity.Name;
                var user = _edm.AspNetUsers.FirstOrDefault(x => x.UserName == currentUser);
                var person = _edm.People.FirstOrDefault(x => x.UserId == user.Id);               
                tickets = _ticket.GetPersonTickets(person.PersonId);
            }
            else
            {
                tickets = _ticket.GetTickets().ToList();
            }
            return View(tickets);
        }

        [Authorize(Roles = "Dispetcher,Aministration,Client")]
        public ActionResult BookTicket(int routeId)
        {
            TicketViewModel model = new TicketViewModel();
            model.Route = new RouteViewModel();
            model.Route.RouteId = routeId;
            var currentUser = User.Identity.Name;
            var user = _edm.AspNetUsers.FirstOrDefault(x => x.UserName == currentUser);
            var person = _edm.People.FirstOrDefault(x => x.UserId == user.Id);
            model.Person = new PersonViewModel();
            model.Person.PersonId = person.PersonId;
            if (model != null)
            {
                _ticket.BookTicket(model);
            }
            var personRoutes = _edm.Tickets.Count(x => x.PersonId == model.Person.PersonId);
            ViewBag.Tickets = personRoutes;
            var availableTickets = _edm.Routes.Count(x => x.RouteId == model.Route.RouteId);
            if(availableTickets <= 0)
            {
                return View("NoAvailableTickets");
            }
            return RedirectToAction("Index", "Route");
        }

        [Authorize(Roles = "Dispetcher,Aministration,Client")]
        public ActionResult PurchaseTicket(int id)
        {
            _ticket.PurchaseTicket(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Dispetcher,Aministration,Client")]
        public ActionResult CancelBookedTicket(int id)
        {
            _ticket.CancelBookedTicked(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Dispetcher,Aministration,Client")]
        public ActionResult RemoveTicket(int id)
        {
            _ticket.RemoveTicket(id);
            return RedirectToAction("Index");
        }

    }
}
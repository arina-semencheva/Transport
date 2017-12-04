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
    public class TicketController : Controller
    {
        TransportDBEntities _edm = new TransportDBEntities();
        TicketDAO _ticket = new TicketDAO();

        public ActionResult Index(int? personId = default(int?))
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();
            if (personId.HasValue)
            {
                tickets = _ticket.GetPersonTickets(personId.Value);
            }
            else
            {
                tickets = _ticket.GetTickets().ToList();
            }
            return View(tickets);
        }

        public ActionResult BookTicket(TicketViewModel model)
        {
            if (model != null)
            {
                _ticket.BookTicket(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult PurchaseTicket(int id)
        {
            _ticket.PurchaseTicket(id);
            return RedirectToAction("Index");
        }

        public ActionResult CancelTicket(int id)
        {
            _ticket.RemoveTicket(id);
            return RedirectToAction("Index");
        }

    }
}
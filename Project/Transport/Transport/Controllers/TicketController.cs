using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.DAO.Ticket;
using Transport.DataModel;

namespace Transport.Controllers
{
    public class TicketController : Controller
    {
        TransportDBEntities _edm = new TransportDBEntities();
        TicketDAO _ticket = new TicketDAO();

        public ActionResult Index()
        {
            return View();
        }
    }
}
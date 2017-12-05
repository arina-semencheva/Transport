using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transport.DataModel;
using Transport.Models;

namespace Transport.DAO.Ticket
{
    public class TicketDAO
    {

        private TransportDBEntities _edm = new TransportDBEntities();

        public IEnumerable<TicketViewModel> GetTickets()
        {
            var tickets = _edm.Tickets.Select(x => new TicketViewModel
            {
                TicketId = x.TicketId,
                TicketState = x.TicketState,
                PurchaseDate = x.PurchaseDate,
                Person = new PersonViewModel
                {
                    PersonId = x.PersonId,
                    Name = x.Person.Name,
                    Surname = x.Person.Surname,
                    BirthDate = x.Person.BirthDate
                },
                Route = new RouteViewModel
                {
                    RouteId = x.RouteId,
                    FirstStop = x.Route.FirstStop,
                    LastSport = x.Route.LastStop,
                    Transport = new TransportViewModel
                    {
                        TransportId = x.Route.TransportId,
                        TransportName = x.Route.Transport.Name,
                        TransportType = new TransporttypeViewModel
                        {
                            TransportTypeId = x.Route.Transport.TransportTypeId,
                            TransportTypeName = x.Route.Transport.TransportType.TransportTypeName
                        }
                    }
                }
            })
            .ToList();
            return tickets;
        }

        public TicketViewModel GetTicketById(int id)
        {
            var ticket = GetTickets().FirstOrDefault(x => x.TicketId == id);
            return ticket;
        }

        public List<TicketViewModel> GetPersonTickets(int id)
        {
            var tickets = GetTickets().Where(x => x.Person.PersonId == id).ToList();
            return tickets;
        }

        //куплен - Purhased, забронирован - Booked, освобожден - отменен - Canceled
        public void BookTicket(TicketViewModel model)
        {
            DataModel.Ticket ticket = new DataModel.Ticket
            {
                PurchaseDate = DateTime.UtcNow.Date,
                PersonId = model.Person.PersonId,
                RouteId = model.Route.RouteId,
                TicketState = "Booked"
            };
            _edm.Tickets.Add(ticket);
            var routeEntity = _edm.Routes.FirstOrDefault(x => x.RouteId == model.Route.RouteId);
            int count = routeEntity.TicketCount.Value - 1;
            routeEntity.TicketCount = count;
            _edm.SaveChanges();
        }

        public void PurchaseTicket(int id)
        {
            var ticket = _edm.Tickets.FirstOrDefault(x => x.TicketId == id);
            if (ticket == null)
                throw new Exception("Билет не найден");
            ticket.TicketState = "Purchased";
            _edm.SaveChanges();
        }

        public void CancelBookedTicked(int id)
        {
            var ticket = _edm.Tickets.FirstOrDefault(x => x.TicketId == id);
            if (ticket == null)
                throw new Exception("Билет не найден");
            ticket.TicketState = "Canceled";
            var route = ticket.Route;
            var ticketsCount = route.TicketCount + 1;
            route.TicketCount = ticketsCount;
            _edm.SaveChanges();
        }

        public void RemoveTicket(int id)
        {
            var ticket = _edm.Tickets.FirstOrDefault(x => x.TicketId == id);
            if (ticket == null)
                throw new Exception("Билет не найден");
            var route = ticket.Route;
            var ticketsCount = route.TicketCount + 1;
            route.TicketCount = ticketsCount;
            _edm.Tickets.Remove(ticket);
            _edm.SaveChanges();
        }

    }
}
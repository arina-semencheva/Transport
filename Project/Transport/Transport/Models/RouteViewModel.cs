using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class RouteViewModel
    {
        public int RouteId { get; set; }
        public string FirstStop { get; set; }
        public string LastSport { get; set; }
        public TransportViewModel Transport { get; set; }
        public PersonViewModel Person { get; set; }
        public int? TicketCount { get; set; }
    }
}
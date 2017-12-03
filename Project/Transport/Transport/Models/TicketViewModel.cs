using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public PersonViewModel Person { get; set; }
        public DateTime PurchaseDate { get; set; }
        public RouteViewModel Route { get; set; }
        public string TicketState { get; set; }
    }
}
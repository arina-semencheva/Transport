using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class TransportViewModel
    {
        public int TransportId { get; set; }
        public int RouteId { get; set; }
        public int FuelId { get; set; }
        public string TransportName { get; set; }
        public string EngineNumber { get; set; }
        public int PersonId { get; set; }
        public string Fuel { get; set; }
        public TransporttypeViewModel TransportType { get; set; }
        public RouteViewModel Route { get; set; }
        public PersonTypeViewModel Person { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transport.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int RouteId { get; set; }
        public int PersonId { get; set; }
        public string TicketState { get; set; }
        public System.DateTime PurchaseDate { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Route Route { get; set; }
    }
}

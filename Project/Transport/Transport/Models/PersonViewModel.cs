using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public PersonTypeViewModel PersonType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int ExperienceWork { get; set; }
        public int TransportId { get; set; }
        public int PersonTypeId { get; set; }
        public string Person => $"{Name} {Surname}";
        public TransportViewModel Transport { get; set; }
    }
}
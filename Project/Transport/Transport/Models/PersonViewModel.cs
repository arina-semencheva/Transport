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
    }
}
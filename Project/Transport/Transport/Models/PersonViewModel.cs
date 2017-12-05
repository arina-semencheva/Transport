using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int ExperienceWork { get; set; }
        public int TransportId { get; set; }
        public int PersonTypeId { get; set; }
        public string Person => $"{Name} {Surname}";
        public TransportViewModel Transport { get; set; }
        public string UserId { get; set; }
    }
}
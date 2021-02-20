using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightTracker5.Models
{
    public class Pilot
    {
            [Key]//primary key
            public int PilotID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateofBirth { get; set; }
            public string Rank { get; set; }
            public Boolean PC { get; set; }
            public Boolean PI { get; set; }
            public Boolean SP { get; set; }
            public Boolean IP { get; set; }
            public string FAC { get; set; }
            public ICollection<Flight> Flight { get; set; }
        
    }
    public class PilotDto
    {
        public int PilotID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Rank { get; set; }
        public Boolean PC { get; set; }
        public Boolean PI { get; set; }
        public Boolean SP { get; set; }
        public Boolean IP { get; set; }
        public string FAC { get; set; }

    }
}
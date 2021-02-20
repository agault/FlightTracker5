using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace FlightTracker5.Models
{
    public class Flight
    {

        [Key] //primary key
        public int FlightID { get; set; }

        [ForeignKey("Pilot")] //FK
        public int PilotID { get; set; }
        public virtual Pilot Pilot { get; set; }//Not quite sure

        //Date
        public DateTime Date { get; set; }

        //Duty
        public string Duty { get; set; }

        //Seat
        public string Seat { get; set; }

        //Mission
        public string Mission { get; set; }

        //day
        public decimal Day { get; set; }

        //night
        public decimal Night { get; set; }

        //night goggles
        public decimal NightGoggles { get; set; }

        //night systems
        public decimal NightSystems { get; set; }

        //weather
        public decimal Weather { get; set; }

        //aircraft id
        public string AircraftId { get; set; }

    }

    public class FlightDto
    {
      
            public int FlightID { get; set; }

            public int PilotID { get; set; }

            //Date
            public DateTime Date { get; set; }

            //Duty
            public string Duty { get; set; }

            //Seat
            public string Seat { get; set; }

            //Mission
            public string Mission { get; set; }

            //day
            public decimal Day { get; set; }

            //night
            public decimal Night { get; set; }

            //night goggles
            public decimal NightGoggles { get; set; }

            //night systems
            public decimal NightSystems { get; set; }

            //weather
            public decimal Weather { get; set; }

            //aircraft id
            public string AircraftId { get; set; }

        
    }
}
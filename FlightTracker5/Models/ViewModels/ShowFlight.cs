using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightTracker5.Models.ViewModels
{
    public class ShowFlight
    {
        public class ShowFlights
        {
            public FlightDto flight { get; set; }
            //information about the pilots  flights
            public PilotDto pilot { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightTracker5.Models.ViewModels
{
    public class UpdateFlight
    {
        //Information about the flight
        public FlightDto flight { get; set; }
        //Needed for a dropdownlist which presents the player with a choice of teams to play for
        public IEnumerable<PilotDto> allpilots { get; set; }
    }
}
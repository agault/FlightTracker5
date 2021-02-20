using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

//Key stone class that maps to server
//This bad boy creates the tables
namespace FlightTracker5.Models
{

    public class FlightsDataContext : DbContext
    {
        public FlightsDataContext() : base("name=FlightsDataContext")
        {
        }
        //DbSet<##CLASS> of table then getter and setters
            public DbSet<Flight> Flight { get; set; }
            public DbSet<Pilot> Pilot { get; set; }
    }
}
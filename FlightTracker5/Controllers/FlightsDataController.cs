using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightTracker5.Models;
//Accesses controller using LINQ
namespace FlightTracker5.Controllers
{
    public class FlightsDataController : ApiController
    {
        private FlightsDataContext db = new FlightsDataContext();


        // GET: api/FlightsData
        // GET: api/FlightsData/GetFlights == LIST OF ALL Flights

        public IEnumerable<FlightDto> GetFlights()
        {//use method IEnumerable Flight DTO, Directly access flights using  db.Flight.ToList();
            List<Flight> Flights = db.Flight.ToList();
            //empty shell to transfer info, lotta stuff under the hood.
            //FlightDTo s safer to send info
            //Go through each flight and
            List<FlightDto> FlightDtos = new List<FlightDto> { };

            //This is the info that your API has access too
            foreach (var Flight in Flights)
            {
                FlightDto NewFlight = new FlightDto
                {
                    FlightID = Flight.FlightID,
                    Date = Flight.Date,
                    Duty = Flight.Duty,
                    Seat = Flight.Seat,
                    Mission = Flight.Mission,
                    Day = Flight.Day,
                    Night = Flight.Night,
                    NightGoggles = Flight.NightGoggles,
                    NightSystems = Flight.NightSystems,
                    Weather = Flight.Weather,
                    AircraftId = Flight.AircraftId

                };
                FlightDtos.Add(NewFlight);
            }
            //return the created DTOs
            //FlightDTOs Defined under Flight declaration in flight model.
            return FlightDtos;
        }

        // GET: api/FlightsData/FindFlight/1  == ReturnFLIGHTID1
        [ResponseType(typeof(FlightDto))]
        [HttpGet]
        public IHttpActionResult FindFlight(int id)
        {
            //Find the data
            Flight Flight = db.Flight.Find(id);
            //if not found, return 404 status code.
            if (Flight == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            //Choose ther info thats exposed to the api
            FlightDto FlightDto = new FlightDto
            {

                FlightID = Flight.FlightID,
                Date = Flight.Date,
                Duty = Flight.Duty,
                Seat = Flight.Seat,
                Mission = Flight.Mission,
                Day = Flight.Day,
                Night = Flight.Night,
                NightGoggles = Flight.NightGoggles,
                NightSystems = Flight.NightSystems,
                Weather = Flight.Weather,
                AircraftId = Flight.AircraftId

            };

            //pass along data as 200 status code OK response
            return Ok(FlightDto);
        }

        // POST: api/FlightsData/UpdateFlight/5
        // FORM DATA: Flight JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFlight(int id, [FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flight.FlightID)
            {
                return BadRequest();
            }

            db.Entry(flight).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FlightsData/AddFlight
        // FORM DATA: Flight JSON Object
        [ResponseType(typeof(Flight))]
        [HttpPost]
        public IHttpActionResult AddFlight([FromBody] Flight flight)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flight.Add(flight);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flight.FlightID }, flight);
        }
        // POST: api/FlightsData/DeleteFlight/6
        [HttpPost]
        public IHttpActionResult DeleteFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return NotFound();
            }
            db.Flight.Remove(flight);
            db.SaveChanges();

            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool FlightExists(int id)
        {
            return db.Flight.Count(e => e.FlightID == id) > 0;
        }
    }
}
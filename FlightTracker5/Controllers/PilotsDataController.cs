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

namespace FlightTracker5.Controllers
{
    public class PilotsDataController : ApiController
    {
        private FlightsDataContext db = new FlightsDataContext();


        // GET: api/PilotsData
        // GET: api/PilotsData/GetPilots == LIST OF ALL Pilots

        public IEnumerable<PilotDto> GetPilots()
        {//use method IEnumerable Pilot DTO, Directly access Pilots using  db.Pilot.ToList();
            List<Pilot> Pilots = db.Pilot.ToList();
            //empty shell to transfer info, lotta stuff under the hood.
            //PilotDTo s safer to send info
            //Go through eachPilot and
            List<PilotDto> PilotDtos = new List<PilotDto> { };

            //This is the info that your API has access too
            foreach (var Pilot in Pilots)
            {/*
         PilotID, FirstName,LastName,DateofBirth,Rank,PC,PI,SP,IP,FAC public ICollection<Flight> Flight*/
                PilotDto NewPilot = new PilotDto
                {
                    PilotID = Pilot.PilotID,
                    FirstName= Pilot.FirstName,
                    LastName=Pilot.LastName,
                    DateofBirth=Pilot.DateofBirth,
                    Rank=Pilot.Rank,
                    PC=Pilot.PC,
                    PI=Pilot.PI,
                    SP=Pilot.SP,
                    IP=Pilot.IP,
                    FAC=Pilot.FAC
                };
                PilotDtos.Add(NewPilot);
            }
            //return the created DTOs
            //PilotDTOs Defined under Pilott declaration in Pilot model.
            return PilotDtos;
        }

        // GET: api/PilotsData/FindPilot/1  == ReturnPilotID1
        [ResponseType(typeof(PilotDto))]
        [HttpGet]
        public IHttpActionResult FindPilot(int id)
        {
            //Find the data
            Pilot Pilot = db.Pilot.Find(id);
            //if not found, return 404 status code.
            if (Pilot == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            //Choose ther info thats exposed to the api
            PilotDto PilotDto = new PilotDto
            {

                PilotID = Pilot.PilotID,
                FirstName = Pilot.FirstName,
                LastName = Pilot.LastName,
                DateofBirth = Pilot.DateofBirth,
                Rank = Pilot.Rank,
                PC = Pilot.PC,
                PI = Pilot.PI,
                SP = Pilot.SP,
                IP = Pilot.IP,
                FAC = Pilot.FAC

            };

            //pass along data as 200 status code OK response
            return Ok(PilotDto);
        }

        // POST: api/PilotsData/UpdatePilot/5
        // FORM DATA: Pilot JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePilot(int id, [FromBody] Pilot pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pilot.PilotID)
            {
                return BadRequest();
            }

            db.Entry(pilot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PilotExists(id))
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

        // POST: api/PilotsData/AddPilot
        // FORM DATA: Pilot JSON Object
        [ResponseType(typeof(Pilot))]
        [HttpPost]
        public IHttpActionResult AddPilot([FromBody] Pilot pilot)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pilot.Add(pilot);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pilot.PilotID }, pilot);
        }
        // POST: api/PilotsData/DeletePilot/5
        [HttpPost]
        public IHttpActionResult DeletePilot(int id)
        {
            Pilot pilot = db.Pilot.Find(id);
            if (pilot == null)
            {
                return NotFound();
            }
            db.Pilot.Remove(pilot);
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
        private bool PilotExists(int id)
        {
            return db.Pilot.Count(e => e.PilotID == id) > 0;
        }
    }
}
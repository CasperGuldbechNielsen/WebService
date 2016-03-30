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
using WebService;

namespace WebService.Controllers
{
    public class SingleRoomsController : ApiController
    {
        private ViewContext db = new ViewContext();

        // GET: api/SingleRooms
        public IQueryable<SingleRoom> GetSingleRooms()
        {
            return db.SingleRooms;
        }

        // GET: api/SingleRooms/5
        [ResponseType(typeof(SingleRoom))]
        public IHttpActionResult GetSingleRoom(int id)
        {
            SingleRoom singleRoom = db.SingleRooms.Find(id);
            if (singleRoom == null)
            {
                return NotFound();
            }

            return Ok(singleRoom);
        }

        // PUT: api/SingleRooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSingleRoom(int id, SingleRoom singleRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != singleRoom.Hotel_Number)
            {
                return BadRequest();
            }

            db.Entry(singleRoom).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SingleRoomExists(id))
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

        // POST: api/SingleRooms
        [ResponseType(typeof(SingleRoom))]
        public IHttpActionResult PostSingleRoom(SingleRoom singleRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SingleRooms.Add(singleRoom);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SingleRoomExists(singleRoom.Hotel_Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = singleRoom.Hotel_Number }, singleRoom);
        }

        // DELETE: api/SingleRooms/5
        [ResponseType(typeof(SingleRoom))]
        public IHttpActionResult DeleteSingleRoom(int id)
        {
            SingleRoom singleRoom = db.SingleRooms.Find(id);
            if (singleRoom == null)
            {
                return NotFound();
            }

            db.SingleRooms.Remove(singleRoom);
            db.SaveChanges();

            return Ok(singleRoom);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SingleRoomExists(int id)
        {
            return db.SingleRooms.Count(e => e.Hotel_Number == id) > 0;
        }
    }
}
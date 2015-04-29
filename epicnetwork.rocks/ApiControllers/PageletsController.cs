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
using epicnetwork.rocks.Models;

namespace epicnetwork.rocks.ApiControllers
{
    public class PageletsController : ApiController
    {
        private MerchantContext db = new MerchantContext();

        // GET: api/Pagelets
        public IQueryable<Pagelet> GetPagelets()
        {
            return db.Pagelets;
        }

        // GET: api/Pagelets/5
        [ResponseType(typeof(Pagelet))]
        public IHttpActionResult GetPagelet(string id)
        {
            Pagelet pagelet = db.Pagelets.Find(id);
            if (pagelet == null)
            {
                return NotFound();
            }

            return Ok(pagelet);
        }

        // PUT: api/Pagelets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPagelet(string id, Pagelet pagelet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pagelet.id)
            {
                return BadRequest();
            }

            db.Entry(pagelet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageletExists(id))
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

        // POST: api/Pagelets
        [ResponseType(typeof(Pagelet))]
        public IHttpActionResult PostPagelet(Pagelet pagelet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pagelets.Add(pagelet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PageletExists(pagelet.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pagelet.id }, pagelet);
        }

        // DELETE: api/Pagelets/5
        [ResponseType(typeof(Pagelet))]
        public IHttpActionResult DeletePagelet(string id)
        {
            Pagelet pagelet = db.Pagelets.Find(id);
            if (pagelet == null)
            {
                return NotFound();
            }

            db.Pagelets.Remove(pagelet);
            db.SaveChanges();

            return Ok(pagelet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageletExists(string id)
        {
            return db.Pagelets.Count(e => e.id == id) > 0;
        }
    }
}
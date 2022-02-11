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
using HTTP_5212_Passion_Project_RX_v1.Models;

namespace HTTP_5212_Passion_Project_RX_v1.Controllers
{
    public class DrugDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DrugData/Listdrugs
        [Route("api/DrugData/Listdrugs")]
        [HttpGet]
        public IQueryable<Drug> ListDrugs()
        {
            return db.Drugs;
        }


        // GET: /api/drugdata/finddrug/2
        [Route("api/DrugData/finddrug/{drugId}")]
        [ResponseType(typeof(Drug))]
        [HttpGet]
        public IHttpActionResult FindDrug(int drugID)
        {
            Drug drug = db.Drugs.Find(drugID);
            if (drug == null)
            {
                return NotFound();
            }

            return Ok(drug);
        }

        // PUT: api/DrugData/updateDrug/5
        [Route("api/DrugData/updateDrug/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDrug(int id, Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drug.DrugID)
            {
                return BadRequest();
            }

            db.Entry(drug).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugExists(id))
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

        // POST: api/DrugData/addDrug
       // [Route("api/DrugData/addDrug")]
        [ResponseType(typeof(Drug))]
        [HttpPost]
        public IHttpActionResult AddDrug(Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drugs.Add(drug);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drug.DrugID }, drug);
        }

        // DELETE: api/DrugData/deleteDrug/5
        [Route("api/DrugData/deleteDrug/{id}")]
        [ResponseType(typeof(Drug))]
        [HttpPost]
        public IHttpActionResult DeleteDrug(int id)
        {
            Drug drug = db.Drugs.Find(id);
            if (drug == null)
            {
                return NotFound();
            }

            db.Drugs.Remove(drug);
            db.SaveChanges();

            return Ok(drug);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrugExists(int id)
        {
            return db.Drugs.Count(e => e.DrugID == id) > 0;
        }
    }
}
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

        /// <summary>
        /// Returns all the available drugs in the system 
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all drugs in the database
        /// <example>
        /// GET: api/DrugData/Listdrugs
        /// </example>
        /// </returns>
        [Route("api/DrugData/Listdrugs")]
        [HttpGet]
        public IQueryable<Drug> ListDrugs()
        {
            return db.Drugs;
        }

        /// <summary>
        /// Returns drug details of a drug matching the given id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A drug in the system matching up the drugID which is Primary Key
        /// </returns>
        /// <param name="drugID">Primary key of the drug</param>
        /// <example>
        /// GET: /api/drugdata/finddrug/2
        /// </example>   
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

        /// <summary>
        /// Updates a particular drug in the system with POST Data Input
        /// </summary>
        /// <param name="id">Represents the drugID Primary Key</param>
        /// <param name="drug">JSON Form Data of a Drug including id of drug to be updated</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response) or
        /// HEADER: 400 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DrugData/updateDrug/5
        /// Drug Json Object
        /// </example>
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

        /// <summary>
        /// Add a new drug to the system
        /// </summary>
        /// <param name="drug">JSON form data of a new drug (no id)</param> 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Drug Id, Drug Data
        /// or
        /// HEADER: 404 (Bad request)
        /// </returns>
        /// <example>
        /// POST: api/DrugData/addDrug
        /// FORM DATA: Drug json object
        /// </example>
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

        /// <summary>
        /// Deletes a Drug from the system matching to the given Drug Id
        /// </summary>
        /// <param name="id">The primary key of the Drug</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///  DELETE: api/DrugData/deleteDrug/5
        /// </example>
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
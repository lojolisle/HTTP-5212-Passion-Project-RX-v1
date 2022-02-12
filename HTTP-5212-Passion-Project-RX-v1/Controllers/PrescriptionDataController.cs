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
using System.Diagnostics;
using System.Web.Routing;

namespace HTTP_5212_Passion_Project_RX_v1.Controllers
{
    public class PrescriptionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // using DTO instead of regular function
        // GET: api/PrescriptionData/ListPrescriptions
        /*[HttpGet]
        public IQueryable<Prescription> ListPrescriptions()
        {
          
            return db.Prescriptions;
        }*/


        /// <summary>
        /// Returns all the available Prescription given by a doctor (doctor name) for a patient (patient Name) in the system 
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Prescriptions in the database
        /// <example>
        /// GET: api/PrescriptionData/ListPrescriptions
        /// </example>
        /// </returns>
        [HttpGet]
        public IEnumerable<PrescriptionDto> ListPrescriptions()
        {
            List<Prescription> Prescriptions = db.Prescriptions.ToList();
            List<PrescriptionDto> PrescriptionDtos = new List<PrescriptionDto>();

            Prescriptions.ForEach(a => PrescriptionDtos.Add(new PrescriptionDto()
            {
                PrescriptionID = a.PrescriptionID,
                DoctorName = a.DoctorName,
                PatientName = a.PatientName
            }));
         
            return PrescriptionDtos;
        }


        /// <summary>
        /// Returns Prescription details matching the given precription id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A prescription in the system matching up the prescriptionID which is Primary Key
        /// </returns>
        /// <param name="drugID">Primary key of the Prescription</param>
        /// <example>
        /// GET: api/PrescriptionData/FindPrescription/1
        /// </example>   
        [Route("api/prescriptiondata/findprescription/{prescriptionID}")]
        [ResponseType(typeof(Prescription))]
        [HttpGet]
        public IHttpActionResult FindPrescription(int PrescriptionID)
        {
            Debug.WriteLine("I reached FIND id : " + PrescriptionID);
           
            Prescription prescription = db.Prescriptions.Find(PrescriptionID);
            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }

        /// <summary>
        /// Updates a particular Prescription in the system with POST Data Input
        /// </summary>
        /// <param name="id">Represents the PrescriptionID Primary Key</param>
        /// <param name="drug">JSON Form Data of a Prescription including id of Prescription to be updated</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response) or
        /// HEADER: 400 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/PrescriptionData/UpdatePrescription/5
        /// Prescription Json Object
        /// </example>
        [Route("api/prescriptiondata/updateprescription/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePrescription(int id, Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.PrescriptionID)
            {
                return BadRequest();
            }

            db.Entry(prescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
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
        /// Add a new Prescription to the system
        /// </summary>
        /// <param name="drug">JSON form data of a new Prescription (no id)</param> 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Prescription Id, Prescription Data
        /// or
        /// HEADER: 404 (Bad request)
        /// </returns>
        /// <example>
        /// POST: api/PrescriptionData/AddPrescription
        /// FORM DATA: Prescription json object
        /// </example>
        // 
        //[Route("api/prescriptiondata/addprescription")]
        [ResponseType(typeof(Prescription))]
        [HttpPost]
        public IHttpActionResult AddPrescription(Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prescriptions.Add(prescription);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = prescription.PrescriptionID }, prescription);
        }


        /// <summary>
        /// Deletes a Prescription from the system matching to the given Prescription Id
        /// </summary>
        /// <param name="id">The primary key of the Prescription</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///  DELETE: api/PrescriptionData/DeletePrescription/5
        /// </example> 
        //[Route("api/prescriptiondata/deleteprescription/{id}")]
        [ResponseType(typeof(Prescription))]
        [HttpPost]
        public IHttpActionResult DeletePrescription(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return NotFound();
            }

            db.Prescriptions.Remove(prescription);
            db.SaveChanges();

            return Ok(prescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionExists(int id)
        {
            return db.Prescriptions.Count(e => e.PrescriptionID == id) > 0;
        }
    }
}
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


        // GET: api/PrescriptionData/ListPrescriptions
        /*[HttpGet]
        public IQueryable<Prescription> ListPrescriptions()
        {
          
            return db.Prescriptions;
        }*/


        // GET: api/PrescriptionData/ListPrescriptions
        [HttpGet]
        public IEnumerable<PrescriptionDto> ListPrescriptions()
        {
            List<Prescription> Prescriptions = db.Prescriptions.ToList();
            List<PrescriptionDto> PrescriptionDtos = new List<PrescriptionDto>();

            Prescriptions.ForEach(a => PrescriptionDtos.Add(new PrescriptionDto()
            {
                PrescriptionID = a.PrescriptionID,
                DoctorName = a.DoctorName
            }));
         
            return PrescriptionDtos;
        }
       


        // GET: api/PrescriptionData/FindPrescription/1
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


        // POST: api/PrescriptionData/UpdatePrescription/5
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

        // POST: api/PrescriptionData/AddPrescription
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

        // POST: api/PrescriptionData/DeletePrescription/5
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
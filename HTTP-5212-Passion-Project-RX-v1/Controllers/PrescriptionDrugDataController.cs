using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HTTP_5212_Passion_Project_RX_v1.Models;

namespace HTTP_5212_Passion_Project_RX_v1.Controllers
{
    public class PrescriptionDrugDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PrescriptionDrugData/ListPrescriptionsDrugs
        /*public IQueryable<PrescriptionDrug> GetPrescriptionsDrugs()
        {
            return db.PrescriptionsDrugs;
        }
        */

        // GET: api/PrescriptionDrugData/ListPrescriptionsDrugs
        [Route("api/PrescriptionDrugData/ListPrescriptionsDrugs")]
        [HttpGet]
         public IEnumerable<PrescriptionDrugDto> ListPrescriptionsDrugs()
         {
             List<PrescriptionDrug> PrescriptionsDrugs = db.PrescriptionsDrugs.ToList();
             List<PrescriptionDrugDto> PrescriptionsDrugsDtos = new List<PrescriptionDrugDto>();

            PrescriptionsDrugs.ForEach(a => PrescriptionsDrugsDtos.Add(new PrescriptionDrugDto()
             {
                 ID = a.ID,
                 DrugId = a.DrugId,
                 PrescriptionID = a.PrescriptionID, 
                 DoctorName = a.Prescription.DoctorName,
                 PatientName = a.Prescription.PatientName,
                 DrugName = a.Drug.DrugName,
                 Quantity = a.Quantity,
                 Dosage = a.Drug.Dosage
            }));

             return PrescriptionsDrugsDtos;
         }


        // GET: api/PrescriptionDrugData/FindPrescriptionsDrugs/5
        [ResponseType(typeof(PrescriptionDrug))]
        [Route("api/PrescriptionDrugData/FindPrescriptionsDrugs/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> FindPrescriptionsDrugs(int id)
        {
            PrescriptionDrug prescriptionDrug = await db.PrescriptionsDrugs.FindAsync(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            return Ok(prescriptionDrug);
        }

        // PUT: api/PrescriptionDrugData/UpdatePrescriptionsDrugs/5
        [ResponseType(typeof(void))]
        [Route("api/PrescriptionDrugData/UpdatePrescriptionsDrugs/5")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePrescriptionsDrugs(int id, PrescriptionDrug prescriptionDrug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescriptionDrug.ID)
            {
                return BadRequest();
            }

            db.Entry(prescriptionDrug).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionDrugExists(id))
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

        // POST:  api/PrescriptionDrugData/AddPrescriptionsDrugs
        [ResponseType(typeof(PrescriptionDrug))]
        [HttpPost]
        public async Task<IHttpActionResult> AddPrescriptionsDrugs(PrescriptionDrug prescriptionDrug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PrescriptionsDrugs.Add(prescriptionDrug);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prescriptionDrug.ID }, prescriptionDrug);
        }

        // DELETE: api/PrescriptionDrugData/DeletePrescriptionsDrugs/1
        [ResponseType(typeof(PrescriptionDrug))]
        [Route("api/PrescriptionDrugData/DeletePrescriptionsDrugs/1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeletePrescriptionsDrugs(int id)
        {
            PrescriptionDrug prescriptionDrug = await db.PrescriptionsDrugs.FindAsync(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            db.PrescriptionsDrugs.Remove(prescriptionDrug);
            await db.SaveChangesAsync();

            return Ok(prescriptionDrug);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionDrugExists(int id)
        {
            return db.PrescriptionsDrugs.Count(e => e.ID == id) > 0;
        }
    }
}
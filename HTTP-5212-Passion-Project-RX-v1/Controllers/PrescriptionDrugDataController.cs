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


        /// <summary>
        /// Returns all the available details of drug (name, dosage etc) and prescription( doctor and patient name) along with
        /// information like drug quantity, how many repeats and sig ( directions of taking a drug) in the system 
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all prescription related drug details in the database
        /// <example>
        ///  GET: api/PrescriptionDrugData/ListAllPrescriptionsDrugs
        /// </example>
        /// </returns>
        [Route("api/PrescriptionDrugData/ListAllPrescriptionsDrugs")]
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
         public IHttpActionResult ListAllPrescriptionsDrugs()
         {
             List<PrescriptionDrug> PrescriptionsDrugs = db.PrescriptionsDrugs.ToList();
             List<PrescriptionDrugDto> PrescriptionsDrugsDtos = new List<PrescriptionDrugDto>();

            PrescriptionsDrugs.ForEach(a => PrescriptionsDrugsDtos.Add(new PrescriptionDrugDto()
             {
                 ID = a.ID, 
                 PrescriptionID = a.PrescriptionID,
                 DoctorName = a.Prescription.DoctorName,
                 PatientName = a.Prescription.PatientName,
                 DrugName = a.Drug.DrugName,
                 Quantity = a.Quantity,
                 Dosage = a.Drug.Dosage,
                 Sig = a.Sig
            }));

            return Ok(PrescriptionsDrugsDtos);
         }


        /// <summary>
        /// Returns the full details in one prescription matching with prescription ID 
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: a prescription with drugs belonging to that prescription and other related details
        /// <example>
        ///  GET: api/PrescriptionDrugData/FindFullPrescription/{pid}
        /// </example>
        /// </returns>
        [Route("api/PrescriptionDrugData/FindFullPrescriptionDetails/{pid}")]
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult FindFullPrescriptionDetails(int PID)
        {
            List<PrescriptionDrug> PrescriptionsDrugs = db.PrescriptionsDrugs.Where(a=>a.PrescriptionID==PID).ToList();
            List<PrescriptionDrugDto> PrescriptionsDrugsDtos = new List<PrescriptionDrugDto>();

            PrescriptionsDrugs.ForEach(a => PrescriptionsDrugsDtos.Add(new PrescriptionDrugDto()
            {
                ID = a.ID,
                PrescriptionID = a.PrescriptionID,
                DoctorName = a.Prescription.DoctorName,
                PatientName = a.Prescription.PatientName,
                DrugName = a.Drug.DrugName,
                Quantity = a.Quantity,
                Dosage = a.Drug.Dosage,
                Sig = a.Sig
            }));

            return Ok(PrescriptionsDrugsDtos);
        }


        /// <summary>
        /// Returns all prescriptions that contains the drug matching drug id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: All prescriptions in the system matching up the drug ID which is Primary Key
        /// </returns>
        /// <param name="drugID">Primary key of the Drug</param>
        /// <example>
        /// GET: api/PrescriptionDrugData/FindAllPrescriptionsForOneDrug/5
        /// </example>   

        [Route("api/PrescriptionDrugData/FindAllPrescriptionsForOneDrug/{drugID}")]
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult FindAllPrescriptionsForOneDrug(int drugID)
        {

            List<PrescriptionDrug> PrescriptionsDrugs = db.PrescriptionsDrugs.Where(d => d.DrugId == drugID).ToList();
            List<PrescriptionDrugDto> PrescriptionsDrugsDtos = new List<PrescriptionDrugDto>();

            PrescriptionsDrugs.ForEach(d => PrescriptionsDrugsDtos.Add(new PrescriptionDrugDto()
            {
                ID = d.ID,
                PrescriptionID = d.PrescriptionID,
                DoctorName = d.Prescription.DoctorName,
                PatientName = d.Prescription.PatientName,
                DrugName = d.Drug.DrugName,
                Quantity = d.Quantity,
                Dosage = d.Drug.Dosage,
                Sig = d.Sig
            }));

            return Ok(PrescriptionsDrugsDtos);
        }

        /// <summary>
        /// Find a prescription from a patients name
        /// SQL QUERY 
        /// select * from prescriptionDrugs pd
        /// left join Prescriptions p ON pd.prescriptionid = p.PrescriptionID
        /// where p.PatientName LIKE 'GE%';
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ResponseType(typeof(PrescriptionDrugDto))]
      /*  public IHttpActionResult  FindPrescriptionOfPatient(int prescriptionId)
        {
            // ????? Not sure how to implement the following SQL Query
            /// select * from prescriptionDrugs pd
            /// left join Prescriptions p ON pd.prescriptionid = p.PrescriptionID
            /// where p.PatientName LIKE 'GE%';

            /*
            PrescriptionDrug selectedPrescriptionDrug = db.PrescriptionsDrugs.Where(
                pd => pd.Prescription.PrescriptionID == prescriptionId)
            
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            PrescriptionsDrugs.ForEach(a => PrescriptionsDrugsDtos.Add(new PrescriptionDrugDto()
            {
                ID = a.ID,
                PrescriptionID = a.PrescriptionID,
                DoctorName = a.Prescription.DoctorName,
                PatientName = a.Prescription.PatientName,
                DrugName = a.Drug.DrugName,
                Quantity = a.Quantity,
                Dosage = a.Drug.Dosage,
                Sig = a.Sig
            }));

            return Ok(PrescriptionsDrugsDtos);
        }

    */
        /// <summary>
        /// Updates a particular PrescriptionDrug details in the system with POST Data Input
        /// </summary>
        /// <param name="id">Represents the PrescriptionID Primary Key</param>
        /// <param name="drug">JSON Form Data of a Prescription including id of Prescription to be updated</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response) or
        /// HEADER: 400 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/PrescriptionDrugData/UpdatePrescriptionsDrugs/5
        /// Prescription Json Object
        /// </example> 
        [ResponseType(typeof(void))]
        [Route("api/PrescriptionDrugData/UpdatePrescriptionsDrugs/5")]
        //[HttpPost] 
        
       
        public IHttpActionResult UpdatePrescriptionsDrugs(int id, PrescriptionDrug prescriptionDrug)
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
                db.SaveChanges();
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

        /// <summary>
        /// Add a new PrescriptionDrug to the system
        /// </summary>
        /// <param name="drug">JSON form data of a new PrescriptionDrug details</param> 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Prescription Id, Drug Id, Prescription/Drug Data
        /// or
        /// HEADER: 404 (Bad request)
        /// </returns>
        /// <example>
        ///POST:  api/PrescriptionDrugData/AddPrescriptionsDrugs
        /// FORM DATA: Prescription json object
        /// </example>
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

        /// <summary>
        /// Deletes a PrescriptionDrug from the system matching to the given Prescription Id
        /// </summary>
        /// <param name="id">The primary key of the Prescription</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// DELETE: api/PrescriptionDrugData/DeletePrescriptionsDrugs/1
        /// </example> 
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
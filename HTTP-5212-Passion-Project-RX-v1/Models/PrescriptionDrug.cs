using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP_5212_Passion_Project_RX_v1.Models
{
    public class PrescriptionDrug
    {
        // This table is used as an explicit bridging table between Drug and Prescription table
        // because we have more columns besides the two foreign keys.
        // it is used to store details of drugs in a particular prescription like qty, repeat etc
        
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }

        public int Repeat { get; set; }

        //Sig represents direction on how to take medications
        public string Sig { get; set; }

        // each PrescriptionDrugs row belongs to one Prescribtion
        [ForeignKey("Prescription")]
        public int PrescriptionID { get; set; }
        public virtual Prescription Prescription { get; set; }

        // each PrescriptionDrug row belong to one Drug
        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public virtual Drug Drug { get; set; }
    }

    public class PrescriptionDrugDto
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int PrescriptionID { get; set; }
    
        public string DoctorName { get; set; }

        public string PatientName { get; set; }
        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public string Dosage { get; set; }
    }
}
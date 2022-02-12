using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP_5212_Passion_Project_RX_v1.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }

    public class PrescriptionDto
    {
        public int PrescriptionID { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP_5212_Passion_Project_RX_v1.Models
{
    public class Drug
    {
        [Key]
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }

        // Formulatioin specifies physical form of drug eg: capsules, tablets
        public string Formulation { get; set; }
    }
}
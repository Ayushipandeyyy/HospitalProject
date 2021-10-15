using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Insurer
    {
        public int InsurerId { get; set; }
        [Required]
        [Display(Name ="Insurer Name")]
        public string InsurerName { get; set; }
        public Nullable<int> RegistrationNo { get; set; }
        public string HeadOffice { get; set; }
    }
}
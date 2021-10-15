using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Hos_pin_Result
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public Nullable<int> InsurerId { get; set; }

        public string InsurerName { get; set; }
    }
}
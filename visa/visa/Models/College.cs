using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class College
    {
        public int id { get; set; }
        [Display(Name = "College name")]
        [Required]
        public string CollegeName { get; set; }
        public string CountryCode { get; set; }
        public string ccountryname { get; set; }
    }
}
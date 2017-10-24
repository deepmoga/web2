using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class PreForm
    {
        public int id { get; set; }
        [Required]
        public int SerialNo { get; set; }
        [Required (ErrorMessage ="EnterName")]
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Religion { get; set; }
        [Required]
        public string Address { get; set; }
        [Required (ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]

        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public string Dateofbirth { get; set; }
        public string BirthCertificate { get; set; }
        public string Passport { get; set; }
        public string NationalId { get; set; }
        public string Ielts { get; set; }
        public string Sat { get; set; }
        public string Tofel { get; set; }
        public string Gre { get; set; }
        [Display(Name = "Preffered Country")]
        public string PrefCountry { get; set; }
        [Display(Name = "Preffered College If You Have")]
        public string PrefCollege { get; set; }
        [Display (Name ="Preffered Subject")]
        public string PrefSubject { get; set; }
        public string Sponsorship { get; set; }
        [Display(Name = "Sponsorship Typ [if yes]")]
        public string SponsorshipType { get; set; }
        public string RefferedName { get; set; }
        public string Comments { get; set; }
        

    }
    public enum Relegion
    {
        Hindu,
        Sikh,
        Muslim
    }
    public enum BirthC
    {
        Yes,No
    }
    public enum scolar
    { yes,no}


}
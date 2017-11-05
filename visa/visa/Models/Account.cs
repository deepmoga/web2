using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class Account
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        [Required]
        public string Role { get; set; }
        public string UserName { get; set; }
       
        public string Password { get; set; }
    }
    public enum Role
    { Admin,Receptionist,User,Editor }
}
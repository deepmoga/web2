using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class InvoiceBalance
    {
        public int id { get; set; }
        public string Date { get; set; }
        public string Invoice { get; set; }
        public string Sid { get; set; }
        
        public string Balance { get; set; }
       
        public string Status { get; set; }

    }
}
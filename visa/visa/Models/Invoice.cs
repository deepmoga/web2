using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class Invoice
    {
        public int id { get; set; }
        public string Date { get; set; }
        public string InvoiceNo { get; set; }
        public string Sid { get; set; }
        public string Particular { get; set; }
        public string Total { get; set; }
        public string Gst { get; set; }
        public string FinalTotal { get; set; }
        public string Paid { get; set; }
        public string Mode { get; set; }
        public string Note { get; set; }
      

    }
}
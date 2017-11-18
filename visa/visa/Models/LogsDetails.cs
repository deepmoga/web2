using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class LogsDetails
    {
        public int id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
       
        public string sid { get; set; }

    }
}
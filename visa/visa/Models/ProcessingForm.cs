using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace visa.Models
{
    public class ProcessingForm
    {
        public int id { get; set; }
        public string OfferLetterFee { get; set; }
        public string AppliedDate { get; set; }
        public string RecivedDate { get; set; }
       
        public string ProcessingFee { get; set; }
        public string ProcessAlertDate { get; set; }
        public string CollegeFee { get; set; }
        public string CollegeAlertDate { get; set; }
        public string GICFee { get; set; }
        public string GICAlertDate { get; set; }
        public string EmedicalFee { get; set; }
        public string AppointmentDate { get; set; }
        public string EmbassyFee { get; set; }
        public string EmbassyAlertDate { get; set; }
        public string TrackingId { get; set; }
        public string Studentid { get; set; }
    }
}
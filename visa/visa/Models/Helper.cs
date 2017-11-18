using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace visa.Models
{
    public class Helper
    {
        public string _FileName;
        public string uploadfile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    string _path = (HttpContext.Current.Server.MapPath("/UploadedFiles/" + _FileName));

                    file.SaveAs(_path);
                }

                return _FileName;
            }
            catch (Exception e)
            {
                return "False";
            }
        }
        public string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Template/ConfirmEmail.txt")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }
        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            dbcontext db = new dbcontext();
            var office = db.OfficeDetails.ToList();

            using (MailMessage mailMessage = new MailMessage(office[0].Email, recepientEmail))
            {
                
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(office[0].Email,office[0].Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }
    }
    public enum NotificationEnumeration
    {
        Success,
        Error,
        Warning
    }
  
}
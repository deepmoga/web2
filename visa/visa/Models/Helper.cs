using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
    public enum NotificationEnumeration
    {
        Success,
        Error,
        Warning
    }
}
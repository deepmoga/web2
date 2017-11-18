using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using visa.Models;
using System.Web.Script.Serialization;
using System.Dynamic;

namespace visa.Controllers
{
    public class LogsDetailsController : BaseController
    {
        private dbcontext db = new dbcontext();

        // GET: LogsDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.LogsDetails.ToListAsync());
        }

        // GET: LogsDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogsDetails logsDetails = await db.LogsDetails.FindAsync(id);
            if (logsDetails == null)
            {
                return HttpNotFound();
            }
            return View(logsDetails);
        }

        // GET: LogsDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogsDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Date,Time,Title,Message,Sender")] LogsDetails logsDetails,int title,string message,int student,Helper help)
        {
            if (ModelState.IsValid)
            {
                logsDetails.Date = DateTime.Now.ToShortDateString();
                logsDetails.Time = DateTime.Now.ToLongTimeString();
                logsDetails.Title = title.ToString();
                logsDetails.sid = student.ToString();
                logsDetails.Message = message;

                db.LogsDetails.Add(logsDetails);
                await db.SaveChangesAsync();
               
                var emailtitle = db.EmailTemplates.FirstOrDefault(x => x.id == title);
               

                var semail = db.PreForms.FirstOrDefault(x => x.id == student);
                if (semail.Email != null)
                {
                    if (semail.ConfirmStatus == "Confirmed")
                    {
                        string body = help.PopulateBody("", emailtitle.Title, "", logsDetails.Message);
                        help.SendHtmlFormattedEmail(semail.Email, emailtitle.Title, body);
                        this.SetNotification("Email Send Successfully", NotificationEnumeration.Success);
                    }
                    else
                    {
                        this.SetNotification("Email Sending Failed . Because Email Not Verify", NotificationEnumeration.Error);
                    }
                }
                else
                {
                    this.SetNotification("Email Sending Failed . Because There Is No Email Registered", NotificationEnumeration.Error);
                }

                return RedirectToAction("Index");
            }
            
            return View(logsDetails);
        }

        // GET: LogsDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogsDetails logsDetails = await db.LogsDetails.FindAsync(id);
            if (logsDetails == null)
            {
                return HttpNotFound();
            }
            return View(logsDetails);
        }

        // POST: LogsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Date,Time,Title,Message,Sender")] LogsDetails logsDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logsDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logsDetails);
        }

        // GET: LogsDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogsDetails logsDetails = await db.LogsDetails.FindAsync(id);
            if (logsDetails == null)
            {
                return HttpNotFound();
            }
            return View(logsDetails);
        }

        // POST: LogsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LogsDetails logsDetails = await db.LogsDetails.FindAsync(id);
            db.LogsDetails.Remove(logsDetails);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetCityList(string stateID)
        {
            List<EmailTemplate> lstcity = new List<EmailTemplate>();
            int idds = Convert.ToInt32(stateID);
            lstcity = (db.EmailTemplates.Where(x => x.id == idds)).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TimeLine()
        {
            List<LogsDetails> aa = new List<LogsDetails>();
            dynamic model = new ExpandoObject();
            var data = (from t in db.LogsDetails
                        join sc in db.PreForms on t.sid equals sc.id.ToString()
                        join st in db.EmailTemplates on t.Title equals st.id.ToString()
                       
                        select new { StudentName = sc.StudentName, Title = st.Title, Message = t.Message, id = t.id, Date = t.Date, Time = t.Time, Sender = t.Sender }).ToList();

            foreach (var item in data) //retrieve each item and assign to model
            {
                aa.Add(new LogsDetails()
                {

                    id = item.id,
                    sid = item.StudentName,
                    Title = item.Title,
                    Message = item.Message,
                    Time = item.Time,
                    Sender = item.Sender,
                    Date = item.Date
                });
            }



            return View(aa);
        }
    }
}

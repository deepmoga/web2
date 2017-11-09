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
using System.Net.Mail;

namespace visa.Controllers
{
    public class ProcessingFormsController :BaseController
    {
        private dbcontext db = new dbcontext();
        public static string student;
        // GET: ProcessingForms
        public async Task<ActionResult> Index(string searchdata)
        {
            if (searchdata != null)
            {

                return View(await db.ProcessingForms.Where(x => x.Studentid == searchdata).ToListAsync());
            }
            else
            {
                return View(await db.ProcessingForms.ToListAsync());
            }
        }

        // GET: ProcessingForms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessingForm processingForm = await db.ProcessingForms.FindAsync(id);
            if (processingForm == null)
            {
                return HttpNotFound();
            }
            return View(processingForm);
        }

        // GET: ProcessingForms/Create
        public ActionResult Create(int? id)
        {

             // ViewBag.message = "Yes";
                return View();
           
           
        }

        // POST: ProcessingForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,OfferLetterFee,AppliedDate,RecivedDate,ProcessingFee,ProcessAlertDate,CollegeFee,CollegeAlertDate,GICFee,GICAlertDate,EmedicalFee,AppointmentDate,EmbassyFee,EmbassyAlertDate,TrackingId,Studentid")] ProcessingForm processingForm,string student)
        {
            if (ModelState.IsValid)
            {
                var pf= db.ProcessingForms.FirstOrDefault(x => x.Studentid == student);
                if (pf != null)
                {

                    ViewBag.message = "Yes";
                    this.SetNotification("You Already Filled A Form. You Can Only Edit This Form . Go to View page And Edit", NotificationEnumeration.Warning);
                    return View(pf);
                }
                else
                {
                    processingForm.Studentid = student; ;
                    db.ProcessingForms.Add(processingForm);
                    await db.SaveChangesAsync();
                    this.SetNotification("Your File Created Successfully", NotificationEnumeration.Warning);
                    return RedirectToAction("Create");
                }
            }

            return View(processingForm);
        }

        // GET: ProcessingForms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessingForm processingForm = await db.ProcessingForms.FindAsync(id);
            student = processingForm.Studentid;
            if (processingForm == null)
            {
                return HttpNotFound();
            }
            return View(processingForm);
        }

        // POST: ProcessingForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OfferLetterFee,AppliedDate,RecivedDate,ProcessingFee,ProcessAlertDate,CollegeFee,CollegeAlertDate,GICFee,GICAlertDate,EmedicalFee,AppointmentDate,EmbassyFee,EmbassyAlertDate,TrackingId,Studentid")] ProcessingForm processingForm)
        {
            if (ModelState.IsValid)
            {
                processingForm.Studentid = student;
                db.Entry(processingForm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                this.SetNotification("Your Fees Form Edit Sucessfully.", NotificationEnumeration.Success);
               
                return RedirectToAction("Index");
            }
            return View(processingForm);
        }

        // GET: ProcessingForms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessingForm processingForm = await db.ProcessingForms.FindAsync(id);
            if (processingForm == null)
            {
                return HttpNotFound();
            }
            return View(processingForm);
        }

        // POST: ProcessingForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessingForm processingForm = await db.ProcessingForms.FindAsync(id);
            db.ProcessingForms.Remove(processingForm);
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
        public ActionResult StuProfile(int? id)
        {

          //  this.SetNotification("Your support ticket was created successfully.", NotificationEnumeration.Success);

            return View();
        }
        public ActionResult Forms(int? id)
        {
            
            return RedirectToAction("Create", "ProcessingForms", new { id = id });

        }
        public ActionResult Docs(int? id)
        {
            TempData["pid"] = id;
            if (Session["pid"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return RedirectToAction("Index", "Docs", new { id = id });

        }
    
    }
}

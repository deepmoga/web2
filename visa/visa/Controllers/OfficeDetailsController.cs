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

namespace visa.Controllers
{
    public class OfficeDetailsController :BaseController
    {
        private dbcontext db = new dbcontext();

        // GET: OfficeDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.OfficeDetails.ToListAsync());
        }

        // GET: OfficeDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeDetail officeDetail = await db.OfficeDetails.FindAsync(id);
            if (officeDetail == null)
            {
                return HttpNotFound();
            }
            return View(officeDetail);
        }

        // GET: OfficeDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CompanyName,Address,Phone,GSTNo,Email,Password")] OfficeDetail officeDetail)
        {
            if (ModelState.IsValid)
            {
                db.OfficeDetails.Add(officeDetail);
                await db.SaveChangesAsync();
                this.SetNotification("Office Detail Added SucessFully", NotificationEnumeration.Success);
                return RedirectToAction("Index");
            }

            return View(officeDetail);
        }

        // GET: OfficeDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeDetail officeDetail = await db.OfficeDetails.FindAsync(id);
            if (officeDetail == null)
            {
                return HttpNotFound();
            }
            return View(officeDetail);
        }

        // POST: OfficeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CompanyName,Address,Phone,GSTNo,Email,Password")] OfficeDetail officeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                this.SetNotification("Office Detail Edit SucessFully", NotificationEnumeration.Success);
                return RedirectToAction("Index");
            }
            return View(officeDetail);
        }

        // GET: OfficeDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeDetail officeDetail = await db.OfficeDetails.FindAsync(id);
            if (officeDetail == null)
            {
                return HttpNotFound();
            }
            return View(officeDetail);
        }

        // POST: OfficeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OfficeDetail officeDetail = await db.OfficeDetails.FindAsync(id);
            db.OfficeDetails.Remove(officeDetail);
            this.SetNotification("Office Detail Deleted SucessFully", NotificationEnumeration.Error);
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
    }
}

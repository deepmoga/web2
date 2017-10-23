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
    public class PreFormsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: PreForms
        public async Task<ActionResult> Index()
        {
            return View(await db.PreForms.ToListAsync());
        }

        // GET: PreForms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreForm preForm = await db.PreForms.FindAsync(id);
            if (preForm == null)
            {
                return HttpNotFound();
            }
            return View(preForm);
        }

        // GET: PreForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,SerialNo,StudentName,FatherName,MotherName,Religion,Address,ContactNo,Nationality,Dateofbirth,BirthCertificate,Passport,NationalId,Ielts,Sat,Tofel,Gre,PrefCountry,PrefCollege,PrefSubject,Sponsorship,SponsorshipType,RefferedName,Comments")] PreForm preForm)
        {
            if (ModelState.IsValid)
            {
                db.PreForms.Add(preForm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(preForm);
        }

        // GET: PreForms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreForm preForm = await db.PreForms.FindAsync(id);
            if (preForm == null)
            {
                return HttpNotFound();
            }
            return View(preForm);
        }

        // POST: PreForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,SerialNo,StudentName,FatherName,MotherName,Religion,Address,ContactNo,Nationality,Dateofbirth,BirthCertificate,Passport,NationalId,Ielts,Sat,Tofel,Gre,PrefCountry,PrefCollege,PrefSubject,Sponsorship,SponsorshipType,RefferedName,Comments")] PreForm preForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preForm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(preForm);
        }

        // GET: PreForms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreForm preForm = await db.PreForms.FindAsync(id);
            if (preForm == null)
            {
                return HttpNotFound();
            }
            return View(preForm);
        }

        // POST: PreForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PreForm preForm = await db.PreForms.FindAsync(id);
            db.PreForms.Remove(preForm);
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

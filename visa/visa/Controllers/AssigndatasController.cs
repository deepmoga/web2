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

namespace visa.Controllers
{
    public class AssigndatasController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Assigndatas
        public async Task<ActionResult> Index()
        {

            return View(await db.Assigndatas.ToListAsync());
        }

        // GET: Assigndatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigndata assigndata = await db.Assigndatas.FindAsync(id);
            if (assigndata == null)
            {
                return HttpNotFound();
            }
            return View(assigndata);
        }

        // GET: Assigndatas/Create
        public ActionResult Create()
        {
            ViewBag.StateList = db.PreForms.ToList();
            return View();
        }
        public ActionResult GetCityList(string stateID)
        {
            List<College> lstcity = new List<College>();

            lstcity = (db.Colleges.Where(x => x.CountryCode == stateID)).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Assigndatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Serialid,Studentid,StudentName,Country,College,Course,Date")] Assigndata assigndata)
        {
            if (ModelState.IsValid)
            {
                db.Assigndatas.Add(assigndata);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(assigndata);
        }

        // GET: Assigndatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigndata assigndata = await db.Assigndatas.FindAsync(id);
            if (assigndata == null)
            {
                return HttpNotFound();
            }
            return View(assigndata);
        }

        // POST: Assigndatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Serialid,Studentid,StudentName,Country,College,Course,Date")] Assigndata assigndata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assigndata).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(assigndata);
        }

        // GET: Assigndatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigndata assigndata = await db.Assigndatas.FindAsync(id);
            if (assigndata == null)
            {
                return HttpNotFound();
            }
            return View(assigndata);
        }

        // POST: Assigndatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assigndata assigndata = await db.Assigndatas.FindAsync(id);
            db.Assigndatas.Remove(assigndata);
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

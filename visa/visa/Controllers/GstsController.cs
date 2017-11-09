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
    public class GstsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Gsts
        public async Task<ActionResult> Index()
        {
            return View(await db.Gsts.ToListAsync());
        }

        // GET: Gsts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gst gst = await db.Gsts.FindAsync(id);
            if (gst == null)
            {
                return HttpNotFound();
            }
            return View(gst);
        }

        // GET: Gsts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gsts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,GstNo,CenterGst,StateGst,TotalGst")] Gst gst)
        {
            if (ModelState.IsValid)
            {
                db.Gsts.Add(gst);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gst);
        }

        // GET: Gsts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gst gst = await db.Gsts.FindAsync(id);
            if (gst == null)
            {
                return HttpNotFound();
            }
            return View(gst);
        }

        // POST: Gsts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,GstNo,CenterGst,StateGst,TotalGst")] Gst gst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gst).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gst);
        }

        // GET: Gsts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gst gst = await db.Gsts.FindAsync(id);
            if (gst == null)
            {
                return HttpNotFound();
            }
            return View(gst);
        }

        // POST: Gsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gst gst = await db.Gsts.FindAsync(id);
            db.Gsts.Remove(gst);
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

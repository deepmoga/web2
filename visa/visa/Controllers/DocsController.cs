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
    public class DocsController :BaseController
    {
        private dbcontext db = new dbcontext();
        public static string img,code;
        // GET: Docs
        public async Task<ActionResult> Index(string searchdata)
        {
            if (searchdata != null)
            {
               
                return View(await db.Docs.Where(x =>x.StudentId==searchdata).ToListAsync());
            }
            else
            {
                return View(await db.Docs.ToListAsync());
            }
          
        }

        // GET: Docs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docs docs = await db.Docs.FindAsync(id);
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // GET: Docs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Docs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,DocName,Documents,StudentId")] Docs docs, IEnumerable<HttpPostedFileBase> file, Helper help, string student)
        {
            if (ModelState.IsValid)
            {
                foreach (var f in file)
                {
                    docs.Documents = help.uploadfile(f);
                    docs.StudentId = student;
                    db.Docs.Add(docs);
                    await db.SaveChangesAsync();
                }
                this.SetNotification("Your Document Insert Successfully", NotificationEnumeration.Success);
                return RedirectToAction("Index");
            }

            return View(docs);
        }

        // GET: Docs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docs docs = await db.Docs.FindAsync(id);
            img = docs.Documents;
            code = docs.StudentId;
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // POST: Docs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,DocName,Documents,StudentId")] Docs docs,HttpPostedFileBase file,Helper help)
        {
            if (ModelState.IsValid)
            {
                docs.Documents = file != null ? help.uploadfile(file) : img;
                docs.StudentId = code;
                db.Entry(docs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                this.SetNotification("Your Document Edit Successfully", NotificationEnumeration.Success);
                return RedirectToAction("Create");
            }
            return View(docs);
        }

        // GET: Docs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docs docs = await db.Docs.FindAsync(id);
            if (docs == null)
            {
                return HttpNotFound();
            }
            return View(docs);
        }

        // POST: Docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Docs docs = await db.Docs.FindAsync(id);
            db.Docs.Remove(docs);
            await db.SaveChangesAsync();
            this.SetNotification("Your Document Deleted Successfully", NotificationEnumeration.Success);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.SetNotification("Error Will Be Occur", NotificationEnumeration.Error);
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Pop(string stateID)
        {
            List<Docs> lstcity = new List<Docs>();
            int id = Convert.ToInt32(stateID);
            lstcity = (db.Docs.Where(x => x.StudentId == stateID)).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

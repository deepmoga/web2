﻿using System;
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
    public class CollegesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string cname, ccode;
        // GET: Colleges
        public async Task<ActionResult> Index(int? id)
        {
            string idds = id.ToString();
            return View(await db.Colleges.Where(x=>x.CountryCode==idds).ToListAsync());
        }

        // GET: Colleges/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = await db.Colleges.FindAsync(id);
            
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // GET: Colleges/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Colleges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CollegeName,CountryCode")] College college,int? id)
        {
            if (ModelState.IsValid)
            {
                var a = db.Countries.Where(x => x.id == id).ToList();
                college.CountryCode = a[0].id.ToString();
                college.ccountryname = a[0].CountryName;
                db.Colleges.Add(college);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Colleges", new {id=id });
            }

            return View(college);
        }

        // GET: Colleges/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = await db.Colleges.FindAsync(id);
            cname = college.ccountryname;
            ccode = college.CountryCode;
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // POST: Colleges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CollegeName,CountryCode")] College college)
        {
            if (ModelState.IsValid)
            {
                college.ccountryname = cname;
                college.CountryCode = ccode;
                db.Entry(college).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(college);
        }

        // GET: Colleges/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = await db.Colleges.FindAsync(id);
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // POST: Colleges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            College college = await db.Colleges.FindAsync(id);
            db.Colleges.Remove(college);
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
        public ActionResult Course(string id)
        {
            TempData["name"] = id.ToString();
            return RedirectToAction("Index", "Courses", new { id = id });
        }
    }
}

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
using System.Web.Script.Serialization;
using System.Dynamic;

namespace visa.Controllers
{
    public class AssigndatasController : Controller
    {
        private dbcontext db = new dbcontext();
        Assigndata ad = new Assigndata();
        // GET: Assigndatas
        public ActionResult Index()
        {
            List<Assigndata> aa = new List<Assigndata>();
               dynamic model = new ExpandoObject();
               var data = (from t in db.Assigndatas
                    join sc in db.PreForms on t.StudentName equals sc.id.ToString()
                    join st in db.Countries on t.Country equals st.id.ToString()
                    join d in db.Colleges on t.College equals d.id.ToString()
                    join cc in db.Courses on t.Course equals cc.id.ToString()
                    select new { StudentName=sc.StudentName, Country=st.CountryName, College=d.CollegeName,id=t.id,serial=sc.SerialNo,Course=cc.CourseName,date=t.Date}).ToList();

            foreach (var item in data) //retrieve each item and assign to model
            {
                aa.Add(new Assigndata()
                {
                   
                    id=item.id,
                    StudentName = item.StudentName,
                    Country = item.Country,
                    College = item.College,
                    Serialid=item.serial.ToString(),
                    Course=item.Course,
                    Date=item.date
                });
            }


            return View(aa);
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
        public ActionResult Course(string stateID)
        {
            List<Course> lstcity = new List<Course>();

            lstcity = (db.Courses.Where(x => x.collegecode == stateID)).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: Assigndatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Serialid,Studentid,StudentName,Country,College,Course,Date")] Assigndata assigndata,string Student,string country,string collegeid,string courseid)
        {
            if (ModelState.IsValid)
            {
                assigndata.StudentName = Student;
                assigndata.Country = country;
                assigndata.College = collegeid;
                assigndata.Course = courseid;
                
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

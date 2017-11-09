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
using System.Net.Mail;
using System.IO;

namespace visa.Controllers
{
    public class PreFormsController : Controller
    {
        private dbcontext db = new dbcontext();
       public static PreForm usersEntities = new PreForm();
        public static PreForm pf = new PreForm();
        public static string student;
        Guid activationCode = Guid.NewGuid();
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
        public async Task<ActionResult> Create([Bind(Include = "id,SerialNo,StudentName,FatherName,MotherName,Religion,Address,ContactNo,Email,Nationality,Dateofbirth,BirthCertificate,Passport,NationalId,Ielts,Sat,Tofel,Gre,PrefCountry,PrefCollege,PrefSubject,Sponsorship,SponsorshipType,RefferedName,Comments")] PreForm preForm)
        {
            if (ModelState.IsValid)
            {
               
                var a = db.PreForms.FirstOrDefault(x => x.Email == preForm.Email);
                if (a == null)
                {
                    preForm.Status = "Pending";
                    db.PreForms.Add(preForm);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Email Already In Used";
                    return RedirectToAction("Create");
                }
               
                
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
        public async Task<ActionResult> Edit([Bind(Include = "id,SerialNo,StudentName,FatherName,MotherName,Religion,Address,ContactNo,Email,Nationality,Dateofbirth,BirthCertificate,Passport,NationalId,Ielts,Sat,Tofel,Gre,PrefCountry,PrefCollege,PrefSubject,Sponsorship,SponsorshipType,RefferedName,Comments")] PreForm preForm)
        {
            if (ModelState.IsValid)
            {
                preForm.ActivationCode = activationCode.ToString();
                db.Entry(preForm).State = EntityState.Modified;
                await db.SaveChangesAsync();

                // Send Confirm Email Address
                SendActivationEmail(preForm.Email);
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
        public ActionResult Pop(string stateID)
        {
            List<PreForm> lstcity = new List<PreForm>();
            int id = Convert.ToInt32(stateID);
            lstcity = (db.PreForms.Where(x => x.id == id)).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Check(string val1)
        {
            List<PreForm> lstcity = new List<PreForm>();
            lstcity = (db.PreForms.Where(x => x.Email == val1)).ToList();
            if (lstcity.Count>0)
            {
                ViewBag.message = "Email Already In Used";
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult profile(int? id)
        {
            TempData["pid"] = id;
            ViewBag.pid = id;
            Session["pid"] = id;
            return RedirectToAction("StuProfile", "ProcessingForms", new { id = Session["pid"] });
            
        }
        private void SendActivationEmail(string Email)
        {
            string body;
            using (var sr = new StreamReader(Server.MapPath("../../App_Data/Template/") + "ConfirmEmail.txt"))
            {
                body = sr.ReadToEnd();
            }
            // Guid activationCode = Guid.NewGuid();
            using (MailMessage mm = new MailMessage("offsolut@gmail.com", Email))
            {
                mm.Subject = "Account Activation";
               // Username: { 1}
                string username = HttpUtility.UrlEncode("abc");
                string password = HttpUtility.UrlEncode("Xyz");
                
                string emailSubject = @"Welcome Email";

                string messageBody = string.Format(body, username, username, password);
                //body += " < br /><br />Please click the following link to activate your account";
                //body += "<br /><a href = '" + string.Format("{0}://{1}/PreForms/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                //body += "<br /><br />Thanks";
                //mm.Body = body;
                body = messageBody;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("offsolut@gmail.com", "passiong");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                
            }
            
    }
        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());

                string code = activationCode.ToString();
                usersEntities = db.PreForms.Where(p => p.ActivationCode == code).FirstOrDefault();
                if (usersEntities != null)
                {
                    usersEntities.ConfirmStatus = "Condirmed";
                    db.Entry(usersEntities).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Activation successful.";
                }
            }
            else
            {
                ViewBag.Message = "Invalid Activation code.";
            }

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers; // mail servisi kullanabilmek için ekledik
using System.Web.Mvc;
using MVC5KurumsalWebSitesi.Models.Context;
using MVC5KurumsalWebSitesi.Models.Model.Context;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class MailSettingsController : Controller
    {
        private KurumsalContext db = new KurumsalContext();
      
        // GET: MailSettings
        public ActionResult Index()
        {
            return View(db.MailSettings.ToList());
        }

        // GET: MailSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailSetting mailSetting = db.MailSettings.Find(id);
            if (mailSetting == null)
            {
                return HttpNotFound();
            }
            return View(mailSetting);
        }

        // GET: MailSettings/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MailId,Smtp,Port,SendMail,MailPassword,EnableSsl")] MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                db.MailSettings.Add(mailSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailSetting);
        }

        // GET: MailSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailSetting mailSetting = db.MailSettings.Find(id);
            if (mailSetting == null)
            {
                return HttpNotFound();
            }
            return View(mailSetting);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MailId,Smtp,Port,SendMail,MailPassword,EnableSsl")] MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailSetting);
        }

        // GET: MailSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailSetting mailSetting = db.MailSettings.Find(id);
            if (mailSetting == null)
            {
                return HttpNotFound();
            }
            return View(mailSetting);
        }

        // POST: MailSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailSetting mailSetting = db.MailSettings.Find(id);
            db.MailSettings.Remove(mailSetting);
            db.SaveChanges();
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
       
        public ActionResult TestMail()
        {
            var mailsetting = db.MailSettings.FirstOrDefault();
            return View(mailsetting);
        }

        [HttpPost]
        public ActionResult TestMail(string mail)
        {
            var testmail = db.MailSettings.SingleOrDefault();
            WebMail.SmtpServer =testmail.Smtp;
            WebMail.EnableSsl =(bool)testmail.EnableSsl;
            WebMail.UserName =testmail.SendMail;
            WebMail.Password =testmail.MailPassword.ToString();
            WebMail.SmtpPort =int.Parse(testmail.Port);
            WebMail.Send(mail, "Test Maili", "Bu bir test mailidir");
            return RedirectToAction("Index");
        }
    }
}

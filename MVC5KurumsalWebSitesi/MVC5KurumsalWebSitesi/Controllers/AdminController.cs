
using MVC5KurumsalWebSitesi.Models.Context;
using MVC5KurumsalWebSitesi.Models.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers; // Crypto sınıfını kullanabilmek için ekledik
using System.Web.Mvc;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        KurumsalContext context = new KurumsalContext();
       
        [Route("yonetimpaneli")]
        [Route("Admin/Index")]
        public ActionResult Index()
        {
            ViewBag.blogcount = context.Blogs.Count();
            ViewBag.categoricount = context.Categories.Count();
            ViewBag.servicecount = context.Services.Count();
            ViewBag.commentcount = context.Comments.Count();
            /* Yorumu durumu false olanların sayısını alıyoruz */
            ViewBag.yorumonay = context.Comments.Where(x => x.Status == false).Count();
            var categoriList = context.Categories.ToList();
            return View(categoriList);
        }

        [Route("yonetimpaneli/admingiris")]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {

            //adminLogin.AdminPassword == Crypto.Hash(admin.AdminPassword, "MD5") && adminLogin.AdminMail == admin.AdminMail

            var sifre = Crypto.Hash(admin.AdminPassword, "MD5");
            var adminLogin = context.Admins.FirstOrDefault(x => x.AdminMail == admin.AdminMail && x.AdminPassword == sifre);         
            
                if (adminLogin!=null)
                {
                    Session["AdminId"] = adminLogin.AdminId;
                    Session["AdminMail"] = adminLogin.AdminMail;
                    Session["yetki"] = adminLogin.AdminRole;
                    TempData["mesaj"] = "ekleme";
                    return RedirectToAction("Index", "Admin");
                }
            ViewBag.uyari = "Kullanıcı adı veya şifre yanlış!!!";
            return View(admin);
        }

        public ActionResult LogOut()
        {
            Session["AdminId"] = null;
            Session["AdminMail"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult Adminler()
        {
            return View(context.Admins.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            if(ModelState.IsValid)
            {
                /* Şifreyi crypto ederek ekliyoruz mD5 olarak şifreliyoruz */
                admin.AdminPassword = Crypto.Hash(admin.AdminPassword, "MD5");
                context.Admins.Add(admin);
                context.SaveChanges();
                TempData["mesaj"] = "insert";
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var updateAdmin = context.Admins.Where(x => x.AdminId == id).SingleOrDefault();
            return View(updateAdmin);
        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin)
        {
            var updateAdmin = context.Admins.Where(x => x.AdminId == id).SingleOrDefault();
            if(ModelState.IsValid)
            {
                updateAdmin.AdminName = admin.AdminName;
                updateAdmin.AdminSurname = admin.AdminSurname;
                updateAdmin.AdminPassword = Crypto.Hash(admin.AdminPassword, "MD5");
                updateAdmin.AdminMail = admin.AdminMail;
                updateAdmin.AdminRole = admin.AdminRole;
                context.SaveChanges();
                TempData["mesaj"] = "update";
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }

        public ActionResult Delete(int id)
        {
            var deleteAdmin = context.Admins.Where(x => x.AdminId == id).SingleOrDefault();
            return View(deleteAdmin);
        }
        [HttpPost]
        public ActionResult Delete(int id,Admin admin)
        {
            var deleteAdmin = context.Admins.Where(x => x.AdminId == id).SingleOrDefault();
            context.Admins.Remove(deleteAdmin);
            context.SaveChanges();
            TempData["mesaj"] = "delete";
            return RedirectToAction("Adminler");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ForgetPassword(string email)
        {

            var sendmail = context.MailSettings.SingleOrDefault();
            var adminmail = context.Admins.Where(x => x.AdminMail == email).SingleOrDefault();
            if (adminmail!=null)
            {
                Guid guid = Guid.NewGuid();
                string NewPassword =guid.ToString().Substring(0, 5);        
                adminmail.AdminPassword = Crypto.Hash(NewPassword.ToString(), "MD5");
                context.SaveChanges();
                WebMail.SmtpServer = sendmail.Smtp;
                WebMail.EnableSsl =(bool)sendmail.EnableSsl;
                WebMail.UserName = sendmail.SendMail;
                WebMail.Password = sendmail.MailPassword;
                WebMail.SmtpPort =int.Parse(sendmail.Port);
                WebMail.Send(email,"Admin panel giriş şifreniz","Şifreniz: "+ NewPassword.ToString());
                ViewBag.uyari = "Şifreniz Eposta adresinize gönderilmiştir";
            }
            else
            {
                ViewBag.uyari = "Hata oluştu tekrar deneyiniz!!!";
            }
            return View();
        }
    }
}
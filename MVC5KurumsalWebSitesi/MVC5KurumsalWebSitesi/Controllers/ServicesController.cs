using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO; // FileInfo kullanabilmek için ekledik
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers; // WebImage kullanabilmek için ekledik
using System.Web.Mvc;
using MVC5KurumsalWebSitesi.Models.Context;
using MVC5KurumsalWebSitesi.Models.Model.Context;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class ServicesController : Controller
    {
        KurumsalContext db = new KurumsalContext();
       
        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        /* bu ifadeyi ekledik ckeditor ile yazıyı kalın yapınca zararlı olabilecek bir değer algılandı diyor Request.Form algılandı diyor bunu engellemek için bu ifadeyi kullandık */
        [ValidateInput(false)]
        public ActionResult Create(Service service, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {              
                /* resim url null kontrolü yapıyoruz */
                if (ImageUrl != null)
                {
                    /* resim daha önce yüklenmiş mi kontrol ediyoruz*/
                    WebImage img = new WebImage(ImageUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                    string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(200, 100);
                    img.Save("~/Images/ServiceImage/" + logoName);
                    service.ImageUrl = "/Images/ServiceImage/" + logoName;
                }


                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.uyari = "Güncellenecek veri bulunamadı!!!";
            }
            var updateService = db.Services.Find(id);
            if (updateService == null)
            {
                return HttpNotFound();
            }
            return View(updateService);
        }

        [HttpPost]
        /* bu ifadeyi ekledik ckeditor ile yazıyı kalın yapınca zararlı olabilecek bir değer algılandı diyor Request.Form algılandı diyor bunu engellemek için bu ifadeyi kullandık */
        [ValidateInput(false)]
        public ActionResult Edit(Service service, HttpPostedFileBase ImageUrl, int? id)
        {
            if (ModelState.IsValid)
            {

                var updateService = db.Services.Where(x => x.ServiceId == id).SingleOrDefault();
                /* resim url null kontrolü yapıyoruz */
                if (ImageUrl != null)
                {
                    /* resim daha önce yüklenmiş mi kontrol ediyoruz*/
                    if (System.IO.File.Exists(Server.MapPath(updateService.ImageUrl)))
                    {
                        /* resim daha önce eklenmiş ise silip yeni resmi ekliyoruz */
                        System.IO.File.Delete(Server.MapPath(updateService.ImageUrl));
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/ServiceImage/" + logoName);
                        updateService.ImageUrl = "/Images/ServiceImage/" + logoName;
                    }
                    else
                    {
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/ServiceImage/" + logoName);
                        updateService.ImageUrl = "/Images/ServiceImage/" + logoName;
                    }
                    updateService.Baslik = service.Baslik;
                    updateService.Aciklama = service.Aciklama;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }               
            }
            return View(service);
        }

            // GET: Services/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Service service = db.Services.Find(id);
                if (service == null)
                {
                    return HttpNotFound();
                }
                return View(service);
            }

            // POST: Services/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Service service = db.Services.Find(id);
                db.Services.Remove(service);
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
    }
}

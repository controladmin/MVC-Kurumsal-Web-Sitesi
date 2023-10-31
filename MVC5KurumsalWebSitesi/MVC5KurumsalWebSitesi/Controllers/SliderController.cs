using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC5KurumsalWebSitesi.Models.Context;
using MVC5KurumsalWebSitesi.Models.Model.Context;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class SliderController : Controller
    {
        private KurumsalContext db = new KurumsalContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "SliderID,Baslik,Aciklama,ImageUrl")] Slider slider,HttpPostedFileBase ImageUrl)
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
                    img.Save("~/Images/SliderImage/" + logoName);
                    slider.ImageUrl = "/Images/SliderImage/" + logoName;
                }

                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "SliderID,Baslik,Aciklama,ImageUrl")] Slider slider,HttpPostedFileBase ImageUrl,int id)
        {
            if (ModelState.IsValid)
            {
                var updateSlider = db.Sliders.Where(x =>x.SliderID == id).SingleOrDefault();
                if (ImageUrl != null)
                {
                    /* resim daha önce yüklenmiş mi kontrol ediyoruz*/
                    if (System.IO.File.Exists(Server.MapPath(updateSlider.ImageUrl)))
                    {
                        /* resim daha önce eklenmiş ise silip yeni resmi ekliyoruz */
                        System.IO.File.Delete(Server.MapPath(updateSlider.ImageUrl));
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/SliderImage/" + logoName);
                        updateSlider.ImageUrl = "/Images/SliderImage/" + logoName;
                    }
                    else
                    {
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/SliderImage/" + logoName);
                        updateSlider.ImageUrl = "/Images/SliderImage/" + logoName;
                    }
                    updateSlider.Baslik = slider.Baslik;
                    updateSlider.Aciklama = slider.Aciklama;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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

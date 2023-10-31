using MVC5KurumsalWebSitesi.Models.Context; // KurumsalContext kullanabilmek için ekledik
using MVC5KurumsalWebSitesi.Models.Model.Context; // About sınıfını kullanabilmek için ekledik
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: Hakkimizda
        KurumsalContext context = new KurumsalContext();
        public ActionResult Index()
        {
            var aboutList = context.Abouts.ToList();
            return View(aboutList);
        }
        public ActionResult Edit(int id)
        {
            var updateAbout = context.Abouts.Where(x => x.AboutId == id).SingleOrDefault();
            return View(updateAbout);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* bu ifadeyi ekledik ckeditor ile yazıyı kalın yapınca zararlı olabilecek bir değer algılandı diyor Request.Form algılandı diyor bunu engellemek için bu ifadeyi kullandık */
        [ValidateInput(false)]
        public ActionResult Edit(int id,About about)
        {
            if(ModelState.IsValid)
            {
                var updateAbout = context.Abouts.Where(x => x.AboutId == id).SingleOrDefault();
                updateAbout.Description = about.Description;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(about);
        }

        public ActionResult Delete(int id)
        {
            var deleteAbout = context.Abouts.Where(x => x.AboutId == id).SingleOrDefault();
            return View(deleteAbout);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* bu ifadeyi ekledik ckeditor ile yazıyı kalın yapınca zararlı olabilecek bir değer algılandı diyor Request.Form algılandı diyor bunu engellemek için bu ifadeyi kullandık */
        [ValidateInput(false)]
        public ActionResult Delete(int id,About about)
        {
            var deleteAbout = context.Abouts.Where(x => x.AboutId == id).SingleOrDefault();
            context.Abouts.Remove(deleteAbout);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
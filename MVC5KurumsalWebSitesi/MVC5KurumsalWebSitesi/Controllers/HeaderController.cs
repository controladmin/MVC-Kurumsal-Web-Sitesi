using MVC5KurumsalWebSitesi.Models.Context; //    KurumsalContext kullanabilmek için ekledik
using MVC5KurumsalWebSitesi.Models.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers; // WebImage sınıfını kullanabilmek için ekledik

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class HeaderController : Controller
    {
        KurumsalContext context = new KurumsalContext();
        // GET: Header
        public ActionResult Index()
        {
            var headerList = context.Headers.ToList();
            return View(headerList);
        }

        // GET: Header/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Header/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Header/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Header/Edit/5
        public ActionResult Edit(int id)
        {
            var updateHeader = context.Headers.Where(x => x.HeaderId == id).SingleOrDefault();
            return View(updateHeader);
        }

        // POST: Header/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* bu ifadeyi ekledik ckeditor ile yazıyı kalın yapınca zararlı olabilecek bir değer algılandı diyor Request.Form algılandı diyor bunu engellemek için bu ifadeyi kullandık */
        [ValidateInput(false)]
        public ActionResult Edit(int id, Header header, HttpPostedFileBase imageurl)
        {

            if (ModelState.IsValid)
            {
                var updateHeader = context.Headers.Where(x => x.HeaderId == id).SingleOrDefault();

                /* resim url null kontrolü yapıyoruz */
                if (imageurl != null)
                {
                    /* resim daha önce yüklenmiş mi kontrol ediyoruz*/
                    if(System.IO.File.Exists(Server.MapPath(updateHeader.LogoUrl)))
                    {
                        /* resim daha önce eklenmiş ise silip yeni resmi ekliyoruz */
                        System.IO.File.Delete(Server.MapPath(updateHeader.LogoUrl));
                        WebImage img = new WebImage(imageurl.InputStream);
                        FileInfo imgInfo = new FileInfo(imageurl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/HeaderImage/" + logoName);
                        updateHeader.LogoUrl = "/Images/HeaderImage/" + logoName;
                    }
                    else
                    {
                        WebImage img = new WebImage(imageurl.InputStream);
                        FileInfo imgInfo = new FileInfo(imageurl.FileName);

                        string logoName = Guid.NewGuid().ToString()+imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/HeaderImage/" + logoName);
                        updateHeader.LogoUrl = "/Images/HeaderImage/" + logoName;
                    }
                    updateHeader.Title = header.Title;
                    updateHeader.Keywords = header.Keywords;
                    updateHeader.Description = header.Description;
                    updateHeader.Unvan = header.Unvan;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(header);
        }

        // GET: Header/Delete/5
        public ActionResult Delete(int id)
        {
            var deleteHeader = context.Headers.Where(x => x.HeaderId == id).SingleOrDefault();
            return View(deleteHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Header header)
        {
            var deleteHeader = context.Headers.Where(x => x.HeaderId == id).SingleOrDefault();
            context.Headers.Remove(deleteHeader);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

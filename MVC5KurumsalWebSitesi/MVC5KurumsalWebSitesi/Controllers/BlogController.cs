using MVC5KurumsalWebSitesi.Models.Context;
using MVC5KurumsalWebSitesi.Models.Model.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private KurumsalContext context = new KurumsalContext();
        public ActionResult Index()
        {
            /* aşağıdaki işlemi yapmaz isek blog listesi açılınca sayfa bir nesnenin öğesine ayarlanamadı hatası verir
               bunu önlemek için Blog tablosuna include diyerek kategori tablosunu ekliyoruz LazyLoadingEnabled=false yapıyoruz
             */
            context.Configuration.LazyLoadingEnabled = false;
            var blogList = context.Blogs.Include("Categori").ToList();
            return View(blogList);
        }
        public ActionResult Create()
        {
            /* Kategori bilgilerini dropdown almak için ekledik */
            ViewBag.categoriid = new SelectList(context.Categories, "CategoriId", "CategoriName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog,HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)              
            {
                /* Kategori bilgilerini dropdown almak için ekledik */
                ViewBag.categoriid = new SelectList(context.Categories, "CategoriId", "CategoriName");
                if (ImageUrl != null)
                {
                    WebImage img = new WebImage(ImageUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                    string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(200, 100);
                    img.Save("~/Images/BlogImage/" + logoName);
                    blog.ImageUrl = "/Images/BlogImage/" + logoName;                 
                }
                context.Blogs.Add(blog);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        public ActionResult Edit(int id)
        {
            /* Kategori bilgilerini dropdown almak için ekledik */
            ViewBag.categoriid = new SelectList(context.Categories, "CategoriId", "CategoriName");
            if (id==null)
            {
                return HttpNotFound();
            }

            var updateBlog = context.Blogs.Where(x => x.BlogId == id).SingleOrDefault();
            if(updateBlog==null)
            {
                return HttpNotFound();
            }
                return View(updateBlog);
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog,HttpPostedFileBase ImageUrl,int id)
        {
            if (ModelState.IsValid)
            {

                /* Kategori bilgilerini dropdown almak için ekledik */
                ViewBag.categoriid = new SelectList(context.Categories, "CategoriId", "CategoriName");
                var updateBlog = context.Blogs.Where(x => x.BlogId == id).SingleOrDefault();

                /* resim url null kontrolü yapıyoruz */
                if (ImageUrl != null)
                {
                    /* resim daha önce yüklenmiş mi kontrol ediyoruz*/
                    if (System.IO.File.Exists(Server.MapPath(updateBlog.ImageUrl)))
                    {
                        /* resim daha önce eklenmiş ise silip yeni resmi ekliyoruz */
                        System.IO.File.Delete(Server.MapPath(updateBlog.ImageUrl));
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/BlogImage/" + logoName);
                        updateBlog.ImageUrl = "/Images/BlogImage/" + logoName;
                    }
                    else
                    {
                        WebImage img = new WebImage(ImageUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(ImageUrl.FileName);

                        string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(200, 100);
                        img.Save("~/Images/BlogImage/" + logoName);
                        updateBlog.ImageUrl = "/Images/BlogImage/" + logoName;
                    }
                    updateBlog.Baslik = blog.Baslik;
                    updateBlog.Icerik = blog.Icerik;
                    updateBlog.CategoriId = blog.CategoriId;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(blog);
        }

        public ActionResult Delete(int id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }
            var deleteBlog = context.Blogs.Where(x => x.BlogId == id).SingleOrDefault();
            if(deleteBlog==null)
            {
                return HttpNotFound();
            }
            return View(deleteBlog);
        }
    
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(int id,Blog blog)
        {
            if(id==null)
            {
                HttpNotFound();
            }
            var deleteBlog = context.Blogs.Where(x => x.BlogId == id).SingleOrDefault();
            if(deleteBlog==null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(deleteBlog.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath(deleteBlog.ImageUrl));
            }
            context.Blogs.Remove(deleteBlog);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
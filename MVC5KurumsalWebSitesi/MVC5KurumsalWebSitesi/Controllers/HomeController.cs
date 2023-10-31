using MVC5KurumsalWebSitesi.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers; // WebMail kullanabilmek için ekledik
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVC5KurumsalWebSitesi.Models.Model.Context;

namespace MVC5KurumsalWebSitesi.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalContext context = new KurumsalContext();
        [Route("")]
        [Route("Anasayfa")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            ViewBag.hizmet = context.Services.ToList().OrderByDescending(x => x.ServiceId);
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            return View(context.Abouts.SingleOrDefault());
        }

        public ActionResult Contact()
        {

            return View();
        }

        [Route("Service")]
        public ActionResult Service()
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            return View(context.Services.ToList().OrderByDescending(x=>x.ServiceId));
        }

        public ActionResult SliderPartial()
        {
            /* Home index içindeki slider kodlarını bu partial view içerisine aldık home index sayfasında partial oalrak çağırdık */
            return View(context.Sliders.ToList().OrderByDescending(x=>x.SliderID));
        }
        public ActionResult ServicePartial()
        {
            
            return View(context.Services.ToList().OrderByDescending(x=>x.ServiceId));
        }

        public ActionResult SliderDetay(int id)
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            var sliderDetay = context.Sliders.Where(x => x.SliderID == id).SingleOrDefault();
            return View(sliderDetay);
        }

        public ActionResult FooterPartial()
        {
            /* _SiteLayout içindeki Footer kodları alanı bu partial içine taşındı ve _SiteLayout içinde partial olarak çağrıldı */
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            ViewBag.iletisim = context.Contacts.SingleOrDefault();
            ViewBag.blog = context.Blogs.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.hizmet = context.Services.ToList().OrderByDescending(x => x.ServiceId);
            return PartialView();
        }
        public ActionResult BlogCategoriPartial()
        {
           
            return PartialView(context.Categories.Include("Blogs").ToList().OrderBy(x=>x.CategoriName));
        }

        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            return View(context.Contacts.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null,string email=null,string konu=null,string mesaj=null)
        {

            ViewBag.header = context.Headers.SingleOrDefault();
            if (adsoyad!=null && email!=null && konu!=null && mesaj!=null)
            {
                var sendmail = context.MailSettings.SingleOrDefault();
                WebMail.SmtpServer =sendmail.Smtp;
                WebMail.EnableSsl = (bool)sendmail.EnableSsl;
                WebMail.UserName =sendmail.SendMail;
                WebMail.Password = sendmail.MailPassword;
                WebMail.SmtpPort =int.Parse(sendmail.Port);
                WebMail.Send(email, konu, email + "<br/>"+adsoyad+ "<br/>" + mesaj);
                ViewBag.uyari = "Mesajınız başarı ile gönderilmiştir";
            }
          else
            {
                ViewBag.uyari = "Hata oluştu tekrar deneyiniz!!!";
            }
            return View();
        }

        [Route("BlogPost")]
        public ActionResult Blog(int sayfa=1)
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            /* Burada kategori tablosu ile ilişki olduğu için onuda includa dahil etmemiz gerek hata vermemesi için */
            return View(context.Blogs.Include("Categori").OrderByDescending(x => x.BlogId).ToPagedList(sayfa, 5));
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            var blogDetay = context.Blogs.Include("Categori").Include("Comments").Where(x => x.BlogId == id).SingleOrDefault();
            return View(blogDetay);
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult CategoriBlog(int id,int sayfa=1)
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            var blogCategori = context.Blogs.Include("Categori").OrderByDescending(y=>y.BlogId).Where(x => x.Categori.CategoriId == id).ToPagedList(sayfa, 5);
            return View(blogCategori);
        }

        public ActionResult BlogKayitPartial()
        {
            /* Bu viewbag _SiteLayout içinde yazdık kimlik title değerini databaseden alıyor */
            ViewBag.header = context.Headers.SingleOrDefault();
            return PartialView(context.Blogs.ToList().OrderByDescending(x=>x.BlogId));
        }

        public JsonResult YorumYap(string adsoyad,string mail,string icerik,int blogid)
        {
            if(icerik==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            context.Comments.Add(new Comment
            {
                AdSoyad = adsoyad,
                Email = mail,
                Icerik = icerik,
                BlogId = blogid,
                Status=false
            });
            context.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ErrorPage403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult ErrorPage404()
        {

            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();

        }
    }
}
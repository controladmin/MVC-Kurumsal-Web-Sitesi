using MVC5KurumsalWebSitesi.Models.Model.Context; // Tabloları görebilmek için ekledik
using System;
using System.Collections.Generic;
using System.Data.Entity; // DbContext sınıfını kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Context
{
    public class KurumsalContext:DbContext
    {

        /* KurumsalDB Web.config altında connection stringe verdiğimiz DB ismi yazılıyor */
        public KurumsalContext():base("KurumsalDB")
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Categori> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
    }
}
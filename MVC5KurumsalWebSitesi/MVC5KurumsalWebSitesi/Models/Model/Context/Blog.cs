using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; //  [Table("Blog")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        [Required, StringLength(150, ErrorMessage = "Başlık alanı en fazla 150 karakter olmalıdır!!!")]
        public string Baslik { get; set; }
        [StringLength(1000, ErrorMessage = "İçerik alanı en fazla 1000 karakter olmalıdır!!!")]
        public string Icerik { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoriId { get; set; }
        public Categori Categori { get; set; }
        public ICollection<Comment> Comments {get;set;}
    }
}
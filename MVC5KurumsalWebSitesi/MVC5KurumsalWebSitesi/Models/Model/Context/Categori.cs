using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //   [Key] ifadesini kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("Categori")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Categori")]
    public class Categori
    {
        [Key]
        public int CategoriId{get;set;}
        [Required,StringLength(50,ErrorMessage ="Kategori adı en fazla 50 karakter olmalıdır!!!")]
        public string CategoriName { get; set; }
        [StringLength(1000,ErrorMessage ="Açıklama alanı en fazla 1000 karakter olmalıdır!!!")]
        public string CategoriDescription { get; set; }

        /* Bu şekilde 2 türde yazabiliriz */
        //public List<Blog> Blogs { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
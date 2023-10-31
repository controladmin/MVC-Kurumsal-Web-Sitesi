using System;
using System.Collections.Generic;
using System.ComponentModel; // [DisplayName("Site Başlığı")] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("Header"] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Header")]
    public class Header
    {
        [Key]
        public int HeaderId { get; set; }
        [Required,StringLength(100,ErrorMessage ="Başlık alanı en fazla 100 karakter olmalıdır!!!")]
        [DisplayName("Site Başlık")]
        public string Title { get; set; }
        [Required, StringLength(200, ErrorMessage = "Başlık alanı en fazla 200 karakter olmalıdır!!!")]
        [DisplayName("Anahtar Kelimeler")]
        public string Keywords { get; set; }
        [Required, StringLength(1000, ErrorMessage = "Başlık alanı en fazla 1000 karakter olmalıdır!!!")]
        [DisplayName("Site Açıklama")]
        public string Description { get; set; }
        [DisplayName("Site Logosu")]
        public string LogoUrl { get; set; }
        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }
    }
}
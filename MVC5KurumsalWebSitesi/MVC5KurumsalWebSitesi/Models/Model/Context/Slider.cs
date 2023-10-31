using System;
using System.Collections.Generic;
using System.ComponentModel; // [DisplayName("Slider Başlık")] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; //  [Table("Slider")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderID { get; set; }
        [DisplayName("Slider Başlık"),StringLength(50,ErrorMessage ="Başlık en fazla 50 karakter olmalıdır!!!")]
        public string Baslik { get; set; }
        [DisplayName("Slider Açıklama"), StringLength(500, ErrorMessage = "Başlık en fazla 500 karakter olmalıdır!!!")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim")]
        public string ImageUrl { get; set; }
    }
}
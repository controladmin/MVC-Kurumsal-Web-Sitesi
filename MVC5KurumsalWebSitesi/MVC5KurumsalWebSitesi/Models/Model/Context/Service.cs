using System;
using System.Collections.Generic;
using System.ComponentModel; //  [DisplayName("Hizmet Başlık")] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations; //  [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("Service")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required,StringLength(150,ErrorMessage ="Başlık alanı en fazla 150 karakter olmalıdır!!!")]
        [DisplayName("Hizmet Başlık")]
        public string Baslik { get; set; }
        [StringLength(1000,ErrorMessage ="Açıklama alanı en fazla 1000 karakter olmalıdır!!!")]
        [DisplayName("Hizmet Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Hizmet Resim")]
        public string ImageUrl { get; set; }
    }
}
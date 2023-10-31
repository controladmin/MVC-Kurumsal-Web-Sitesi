using System;
using System.Collections.Generic;
using System.ComponentModel; // [DisplayName] ifadesini kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations; // [Key] ifadesini kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("About"] ifadesini kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("About")]
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        [Required,StringLength(1000,ErrorMessage ="Açıklama alanı en fazla 1000 karakter olmalıdır!!!")]
        [DisplayName("Hakkımızda Açıklama")]
        public string Description { get; set; }
    }
}
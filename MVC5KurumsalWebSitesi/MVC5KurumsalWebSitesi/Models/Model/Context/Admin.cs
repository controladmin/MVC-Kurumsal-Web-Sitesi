using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // [Key] ifadesini kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; //  [Table("Admin")] ifadesini kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required,StringLength(30,ErrorMessage ="Ad alanı en fazla 30 karakter olmalıdır!!!")]
        public string AdminName { get; set; }
        [Required, StringLength(30, ErrorMessage = "Soyad alanı en fazla 30 karakter olmalıdır!!!")]
        public string AdminSurname { get; set; }
        [Required, StringLength(50, ErrorMessage = "Mail alanı en fazla 50 karakter olmalıdır!!!")]
        public string AdminMail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminRole { get; set; }
    }
}
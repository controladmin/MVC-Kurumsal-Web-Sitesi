using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("Contact")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [StringLength(500,ErrorMessage ="Adres alanı boş geçilemez!!!")]
        public string Adres { get; set; }
        [StringLength(50,ErrorMessage ="Mail alanı en fazla 50 karakter olmalıdır!!!")]
        public string Mail { get; set; }
        [StringLength(20,ErrorMessage ="Telefon alanı en fazla 50 karakter olmalıdır!!!")]
        public string Telefon { get; set; }
        public string Whatsapp { get; set; }
        public string Fax { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instegram { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; //  [Table("MailSetting")] kullanabilmek için ekledik
using System.Linq;
using System.Web;

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("MailSetting")]
    public class MailSetting
    {
        [Key]
        public int MailId { get; set; }
        [Required(ErrorMessage ="Smpt alanı boş geçilemez!!!")]
        public string Smtp { get; set; }
        [Required(ErrorMessage ="Port alanı boş geçilemez!!!")]
        public string Port { get; set; }
        [Required,EmailAddress(ErrorMessage ="Mail alanı boş geçilemez!!!")]
        [DisplayName("Gonderici Mail")]
        public string SendMail { get; set; }
        [Required(ErrorMessage ="Şifre alanı boş geçilemez!!!")]
        [DisplayName("Mail Sifresi")]
        public string MailPassword { get; set; }

        public bool? EnableSsl { get; set; }
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // [Key] kullanabilmek için ekledik
using System.ComponentModel.DataAnnotations.Schema; // [Table("Comment")] kullanabilmek için ekledik
using System.ComponentModel; //    [DisplayName("Yorumunuz")] kullanabilmek için ekledik

namespace MVC5KurumsalWebSitesi.Models.Model.Context
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required,StringLength(50,ErrorMessage ="En fazla 50 karakter girebilirsiniz!!!")]
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        [DisplayName("Yorumunuz")]
        public string Icerik { get; set; }
        public bool Status { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
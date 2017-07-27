using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ugurcan.Models.Tablolar
{
    [Table("About")]
    public class About : BaseEntity
    {
        [Display(Name = "About statement Name")]
        [MinLength(30, ErrorMessage = "Minumum 30 Karakter Girmelisiniz!")]
        [UIHint("tinymce_full_compressed"), AllowHtml]
        public string Aciklama { get; set; }


        [Display(Name = "About Image")]
        [MaxLength(3000, ErrorMessage = "Maximum 3000 karakter girebilirsiniz")]
        public string ResimUrl { get; set; }
    }
}
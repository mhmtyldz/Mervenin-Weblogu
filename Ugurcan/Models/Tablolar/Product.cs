using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ugurcan.Models.Tablolar
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Display(Name = "Blog Başlık")]
        [MaxLength(80, ErrorMessage = "Max. Uzunluk 80!")]
        [Required(ErrorMessage = "Lütfen Başlığı Giriniz !")]
        public string Baslik { get; set; }

        [Display(Name = "Kısa Açıklama")]
        public string KisaAciklama { get; set; }

        [Display(Name = "Blog Açıklama")]
        [UIHint("tinymce_full_compressed"), AllowHtml]
        [Required(ErrorMessage="Lütfen Açıklamayı Giriniz!")]
        public string Aciklama { get; set; }


        public int Okunma { get; set; }


        public int BegeniSayisi { get; set; }

    

        public int CategoryID { get; set; }

        [Display(Name = "Blog Resmi")]
        [MaxLength(300, ErrorMessage = "max. 300!")]
        public string ResimURL { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Yorum> Yorum { get; set; }
    }
}
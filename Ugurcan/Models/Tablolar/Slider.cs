using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ugurcan.Models.Tablolar
{
    [Table("Slider")]
    public class Slider:BaseEntity
    {
        [Display(Name = "Title")]
        [MinLength(3, ErrorMessage = "Min 3 Character!"), MaxLength(120, ErrorMessage = "Title Max. 120 Character")]
        public string Baslik { get; set; }


        [Display(Name = "Declaration")]
        [MinLength(3, ErrorMessage = "Declaration Min. 3 Character!"), MaxLength(400, ErrorMessage = "Declaration Max. 400 Character!")]
        public string Aciklama { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Required Image! ")]
        public string ResimURL { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ugurcan.Models.Tablolar
{
    [Table("Message")]
    public class Message : BaseEntity
    {
        [Display(Name = "Name Surname")]
        [MaxLength(100, ErrorMessage = "Max. 100 character for Name and Surname!")]
        [Required(ErrorMessage = "Required Name and Surname")]
        public string AdSoyad { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required Email")]
       
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(20, ErrorMessage = "Max 20 Character for Phone Number!")]
        [MinLength(7, ErrorMessage = "Min 7 character for Phone Number!")]
        [Required(ErrorMessage = "Required Phone Number")]
        public string TelNo { get; set; }

        [Display(Name = "Subject")]
        [MaxLength(200, ErrorMessage = "Max 200 character for subject!")]
        [MinLength(2, ErrorMessage = "Min 2 character for subject!")]
        public string Konu { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Required Message!")]
        [MaxLength(1500, ErrorMessage = "Max 1500 character for Message!")]
        [MinLength(2, ErrorMessage = "Min 2 character for Message!")]
        public string Mesaj { get; set; }
    }
}
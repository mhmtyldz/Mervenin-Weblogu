using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ugurcan.Models.Tablolar
{
    [Table("Users")]
    public class Users:BaseEntity
    {
        private bool Onay = false;

        [MaxLength(70, ErrorMessage = " Please Max. 70 character! ")]
        [Required(ErrorMessage="Please write your Name and Surname! ")]
        [Display(Name = "Name and Surname")]
        public string AdSoyad { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please ")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen Şifre Kısmını Doldurunuz")]
        [MaxLength(16, ErrorMessage = "Lütfen 16 karakterden fazla değer girmeyiniz ! ")]
        public string Sifre { get; set; }

        public bool OnayliMi { get { return Onay; } set { Onay = value; } }
    }
}
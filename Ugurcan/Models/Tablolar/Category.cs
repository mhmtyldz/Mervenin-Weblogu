using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ugurcan.Models.Tablolar
{
    [Table("Category")]
    public class Category:BaseEntity
    {
        public Category()
        {

        }

        [MinLength(2, ErrorMessage = "Min. length 2"), MaxLength(150, ErrorMessage = "Max. length 70")]
        [Required]
        public string KategoriAdi { get; set; }

        public virtual List<Product> Product { get; set; }
    }
}
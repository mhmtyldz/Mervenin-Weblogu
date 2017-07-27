using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ugurcan.Models.Tablolar;

namespace Ugurcan.Models.DataContext
{
    public class MagdiContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Resimler> Resimler { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
    }
}
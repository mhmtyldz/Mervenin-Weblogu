using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ugurcan.Models.DataContext;
using Ugurcan.Models.Tablolar;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace Ugurcan.Controllers
{
    public class HomeController : Controller
    {
        MagdiContext db = new MagdiContext();

        public ActionResult Anasayfa()
        {
            return View();
        }



        public ActionResult Homepage()
        {
            return View();
        }

        public PartialViewResult _SliderPartial()
        {
            return PartialView(db.Slider.OrderByDescending(x => x.EklenmeTarihi).Take(6).ToList());
        }

        public PartialViewResult _SonEklenenUrunlerAnasayfa()
        {

            return PartialView(db.Product.OrderByDescending(x => x.EklenmeTarihi).Take(4).ToList());
        }


        public PartialViewResult _PopulerUrunler()
        {
            return PartialView(db.Product.OrderByDescending(x => x.Okunma).Take(4).ToList());
        }

        public PartialViewResult _FavoriUrunler()
        {

            return PartialView(db.Product.OrderByDescending(x => x.EklenmeTarihi).Take(12).ToList());
        }

        public PartialViewResult _Urunler()
        {
            return PartialView(db.Product.OrderByDescending(x => x.EklenmeTarihi).Take(12).ToList());
        }




        public PartialViewResult _KategoriPartial()
        {
            return PartialView(db.Category.OrderByDescending(x => x.EklenmeTarihi).ToList());
        }

        public PartialViewResult _Kategori2Partial()
        {
            return PartialView(db.Category.OrderByDescending(x => x.EklenmeTarihi).ToList());
        }

        public PartialViewResult _Kategori3Partial()
        {
            return PartialView(db.Category.OrderByDescending(x => x.EklenmeTarihi).ToList());
        }


        public PartialViewResult _AboutAnasayfa()
        {
            return PartialView(db.About.OrderByDescending(x => x.EklenmeTarihi).ToList().Take(1));
        }

        //public ActionResult Product(int Sayfa = 1)
        //{
        //    return View(db.Product.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        //}


        public ActionResult ProductDetail(int id)
        {
            Product dbUrun = db.Product.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dbUrun == null)
            {
                return RedirectToAction("HttpNotFound", "Home");
            }
            dbUrun.Okunma++;
            db.SaveChanges();
            return View(dbUrun);
        }

        public ActionResult CategoryProduct(int id, int Sayfa = 1)
        {
            return View(db.Product.Where(x => x.CategoryID == id).OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }





    }
}
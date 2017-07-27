using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ugurcan.Helper.CustomHelper;
using Ugurcan.Models.DataContext;
using Ugurcan.Models.Tablolar;
using PagedList;
using PagedList.Mvc;
using System.Net;
using Ugurcan.Helper.JsonMessage;
using System.IO;

namespace Ugurcan.Controllers
{
    public class AdminController : Controller
    {
        MagdiContext db = new MagdiContext();

        [LoginFilter]
        public ActionResult Anasayfa()
        {
            return View();
        }

        #region User
        [LoginFilter]
        [HttpGet]
        public ActionResult UserIndex()
        {
            return View(db.Users.OrderByDescending(x => x.EklenmeTarihi).ToList());
        }

        [LoginFilter]
        public ActionResult UserAccept(int id)
        {
            Users gelenUser = db.Users.Find(id);
            if (gelenUser.OnayliMi == true)
            {
                gelenUser.OnayliMi = false;
                db.SaveChanges();
                TempData["Bilgi"] = "Onay işlemi Tamamlandı.Artık Kullanıcı Onaylı Değil!";
                return RedirectToAction("UserIndex", "Admin");
            }

            else if (gelenUser.OnayliMi == false)
            {
                gelenUser.OnayliMi = true;
                db.SaveChanges();
                TempData["Bilgi"] = "Onay işlemi Tamamlandı!Artık Kullanıcı Onaylı!";
                return RedirectToAction("UserIndex", "Admin");
            }

            return View();
        }

        [LoginFilter]
        public JsonResult UserDelete(int ID)
        {
            Users dbUser = db.Users.Find(ID);
            if (dbUser == null)
            {
                return Json(new ResultJson { Success = false, Message = "Böyle Bir Kullanıcı Bulunamadı!" });
            }
            db.Users.Remove(dbUser);
            db.SaveChanges();
            return Json(new ResultJson { Success = true, Message = "Kullanıcı Silindi!" });
        }
        #endregion


        #region Category
        [LoginFilter]
        public ActionResult CategoryIndex(int Sayfa = 1)
        {

            return View(db.Category.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }

        [HttpGet]
        [LoginFilter]
        public ActionResult CategoryCreate()
        {
            return View();
        }


        [HttpPost]
        [LoginFilter]
        public ActionResult CategoryCreate(Category categories)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(categories);
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");

            }
            return View(categories);

        }


        [HttpGet]
        [LoginFilter]
        public ActionResult CategoryUpdate(int id)
        {
            Category dbCategory = db.Category.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (dbCategory == null)
            {
                return HttpNotFound();

            }
            return View(dbCategory);

        }



        [HttpPost]
        [LoginFilter]
        public ActionResult CategoryUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                Category dbCategory = db.Category.Find(category.ID);
                dbCategory.KategoriAdi = category.KategoriAdi;
                db.SaveChanges();
                return RedirectToAction("CategoryIndex", "Admin");
            }

            else
            {
                return View(category);
            }

        }


        [LoginFilter]
        public JsonResult CategoryDelete(int ID)
        {
            Category dbKategori = db.Category.Find(ID);
            if (dbKategori == null)
            {
                return Json(new ResultJson { Success = false, Message = "We Don't have this Category!" });
            }
            db.Category.Remove(dbKategori);
            db.SaveChanges();
            return Json(new ResultJson { Success = true, Message = "Kategori Silindi!" });
        }



        #endregion


        #region About
        [LoginFilter]
        public ActionResult AboutCreate()
        {
            return View();
        }


        [LoginFilter]
        [HttpPost]
        public ActionResult AboutCreate(About about, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (about.ResimUrl != null)
                {
                    if (ResimURL.ContentLength > 0)
                    {
                        string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                        string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string ResimYolu = "/External/About/" + Dosya + Uzanti;

                        ResimURL.SaveAs(Server.MapPath(ResimYolu));
                        about.ResimUrl = ResimYolu;
                    }

                    try
                    {
                        db.About.Add(about);
                        db.SaveChanges();
                        TempData["Bilgi"] = "Başarılı!! About Oluşturuldu!";
                        return RedirectToAction("AboutIndex", "Admin");
                    }
                    catch (Exception)
                    {

                        return View(about);
                    }

                }

            }
            TempData["BilgiDanger"] = "Eklenmedi!! Lütfen Kurallara Uyarak Ekleyiniz!";
            return RedirectToAction("AboutIndex", "Admin");
        }

        [LoginFilter]
        public ActionResult AboutIndex()
        {
            return View(db.About.OrderByDescending(x => x.EklenmeTarihi).ToList().Take(1));
        }

        [LoginFilter]
        public JsonResult AboutDelete(About about)
        {
            About dbAbout = db.About.Find(about.ID);
            if (dbAbout == null)
            {
                return Json(new ResultJson { Success = false, Message = "About not found ! " });

            }
            try
            {
                if (dbAbout.ResimUrl != null)
                {
                    string ResimURL = dbAbout.ResimUrl;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.About.Remove(dbAbout);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "About Silindi ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Birşeyler Yanlış Gitti ! " });
            }
        }

        #endregion


        #region Message

        [LoginFilter]
        public ActionResult MessageIndex(int Sayfa = 1)
        {
            return View(db.Message.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 15));
        }

        [LoginFilter]
        public ActionResult MessageDetail(int id)
        {
            Message dbMessage = db.Message.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dbMessage == null)
            {
                return HttpNotFound();
            }
            return View(dbMessage);
        }

        [LoginFilter]
        public JsonResult MessageDelete(int ID)
        {
            Message dbMessage = db.Message.Find(ID);
            if (dbMessage == null)
            {
                return Json(new ResultJson { Success = false, Message = "We Don't have this Message!" });
            }
            db.Message.Remove(dbMessage);
            db.SaveChanges();
            return Json(new ResultJson { Success = true, Message = "Message Deleted!" });
        }

        #endregion


        #region Product

        [LoginFilter]
        [HttpGet]
        public ActionResult BlogIndex(int Sayfa = 1)
        {
            return View(db.Product.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }


        [LoginFilter]
        public ActionResult BlogCreate()
        {
            SetKategoriListesi();
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult BlogCreate(Product product, int CategoryID, HttpPostedFileBase VitrinResmi)
        {
            if (ModelState.IsValid)
            {
                if (product != null)
                {
                    product.CategoryID = CategoryID;
                    if (VitrinResmi != null)
                    {
                        string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string TamYol = "/External/Product/" + DosyaAdi + Uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(TamYol));
                        product.ResimURL = TamYol;
                    }
                    db.Product.Add(product);
                    db.SaveChanges();


                    TempData["Bilgi"] = "Blog Oluşturma İşlemi Başarılı!";
                    return RedirectToAction("BlogIndex", "Admin");
                }
                TempData["BilgiDanger"] = "Blog Oluşturma İşlemi Başarısız!";
                return RedirectToAction("BlogIndex", "Admin");

            }
            TempData["BilgiDanger"] = "Blog Oluşturma İşlemi Başarılı,Bazı Bilgileri Yanlış Giriyorsunuz!";
            return RedirectToAction("BlogIndex", "Admin");
        }


        [LoginFilter]
        public void SetKategoriListesi(object kategori = null)
        {
            var KategoriList = db.Category.ToList();
            ViewBag.Kategori = KategoriList;

            db.SaveChanges();
        }


        [HttpGet]
        [LoginFilter]
        public ActionResult BlogUpdate(int id)
        {
            Product dbProduct = db.Product.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (dbProduct == null)
            {
                return HttpNotFound();
            }
            SetKategoriListesi();
            return View(dbProduct);
        }


        [LoginFilter]
        [HttpPost]
        public ActionResult BlogUpdate(Product urun, HttpPostedFileBase VitrinResmi)
        {
            Product gelenUrun = db.Product.Find(urun.ID);
            if (ModelState.IsValid)
            {
                gelenUrun.Aciklama = urun.Aciklama;
                gelenUrun.AktifMi = urun.AktifMi;
                gelenUrun.Baslik = urun.Baslik;
                gelenUrun.KisaAciklama = urun.KisaAciklama;
                gelenUrun.CategoryID = urun.CategoryID;
                if (VitrinResmi != null)
                {
                    string dosyaadi = gelenUrun.ResimURL;
                    string dosyaYolu = Server.MapPath(dosyaadi);
                    FileInfo dosya = new FileInfo(dosyaYolu);
                    if (dosya.Exists)
                    {
                        dosya.Delete();
                    }
                    string file_name = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYol = "/External/Product/" + file_name + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYol));
                    gelenUrun.ResimURL = TamYol;
                }
                db.SaveChanges();
                TempData["Bilgi"] = "Güncelleme İşleminiz Başarılı!";
                return RedirectToAction("BlogIndex", "Admin");
            }
            TempData["BilgiDanger"] = "Güncelleme İşleminiz Başarısız!!";
            return RedirectToAction("BlogIndex", "Admin");
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult BlogDetail(int id)
        {
            Product dbProduct = db.Product.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dbProduct == null)
            {
                return HttpNotFound();
            }
            return View(dbProduct);
        }





        [LoginFilter]
        public JsonResult BlogDelete(Product urun)
        {
            Product dbUrun = db.Product.Find(urun.ID);
            if (dbUrun == null)
            {
                return Json(new ResultJson { Success = false, Message = "Not Find Blog ! " });

            }
            try
            {
                if (dbUrun.ResimURL != null)
                {
                    string ResimURL = dbUrun.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Product.Remove(dbUrun);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Success! Blog is DELETED ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Something is going wrong! Blog is not DELETED! " });
            }
        }


        #endregion


        #region Slider
        [LoginFilter]
        public ActionResult SliderIndex(int Sayfa = 1)
        {
            return View(db.Slider.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }

        [LoginFilter]
        public ActionResult SliderCreate()
        {
            return View();
        }

        [LoginFilter]
        [HttpPost]
        public ActionResult SliderCreate(Slider slider, HttpPostedFileBase ResimURL)
        {

            if (slider.ResimURL != null)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Slider/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu));
                    slider.ResimURL = ResimYolu;
                }

                try
                {
                    db.Slider.Add(slider);
                    db.SaveChanges();
                    TempData["Bilgi"] = "Başarılı!! Slider Oluşturuldu!";
                    return RedirectToAction("SliderIndex", "Admin");
                }
                catch (Exception)
                {
                    TempData["BilgiDanger"] = "Hata! Slider Oluşturulamadı!";
                    return View(slider);

                }

            }
            TempData["BilgiDanger"] = "Hata! Slider Oluşturulamadı!";
            return View(slider);
        }


        [LoginFilter]
        public JsonResult SliderDelete(Slider slider)
        {
            Slider dbSlider = db.Slider.Find(slider.ID);
            if (dbSlider == null)
            {
                return Json(new ResultJson { Success = false, Message = "Not Found Slider ! " });

            }
            try
            {
                if (dbSlider.ResimURL != null)
                {
                    string ResimURL = dbSlider.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Slider.Remove(dbSlider);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Başarılı!! Slider Silindi ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Wrong!! Slider silinemedi ! " });
            }
        }
        #endregion


        #region Resimler
        [LoginFilter]
        public ActionResult ImageIndex(int Sayfa = 1)
        {
            return View(db.Resimler.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }

        [HttpGet]
        [LoginFilter]
        public ActionResult ImageCreate()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult ImageCreate(Resimler resimler, IEnumerable<HttpPostedFileBase> DetayResim)
        {
            if (resimler != null)
            {
                string cokluResims = System.IO.Path.GetExtension(Request.Files[1].FileName);

                if (cokluResims != "")
                {
                    foreach (var file in DetayResim)
                    {
                        if (file.ContentLength > 0)
                        {
                            string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string Uzanti = System.IO.Path.GetExtension(Request.Files[1].FileName);
                            string TamYol = "/External/Resimler/" + DosyaAdi + Uzanti;
                            resimler.ResimUrl = TamYol;
                            file.SaveAs(Server.MapPath(TamYol));
                            Random rastgele = new Random();
                            resimler.BegeniSayisi = rastgele.Next(3, 5);
                            db.Resimler.Add(resimler);
                            db.SaveChanges();
                        }

                    }

                    TempData["Bilgi"] = "Resim Ekleme İşleminiz Başarılı";
                    return RedirectToAction("ImageIndex", "Admin");
                }
                TempData["BilgiDanger"] = "Resim Seçmeyi Unuttunuz";
                return View(resimler);
            }
            TempData["BilgiDanger"] = "Lütfen Resim Seçiniz";
            return View(resimler);
        }

        [LoginFilter]
        public JsonResult ImageDelete(Resimler resim)
        {
            Resimler dbSlider = db.Resimler.Find(resim.ID);
            if (dbSlider == null)
            {
                return Json(new ResultJson { Success = false, Message = "Not Found Slider ! " });

            }
            try
            {
                if (dbSlider.ResimUrl != null)
                {
                    string ResimURL = dbSlider.ResimUrl;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Resimler.Remove(dbSlider);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Başarılı!! Resim Silindi ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Wrong!! Resim silinemedi ! " });
            }
        }
        #endregion


        #region Yorumlar


        [LoginFilter]
        public ActionResult YorumIndex(int Sayfa = 1)
        {
            return View(db.Yorum.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 20));
        }


        [LoginFilter]
        public ActionResult YorumOnay(int id)
        {
            Yorum gelenYorum = db.Yorum.Find(id);
            if (gelenYorum.OnayliMi == true)
            {
                gelenYorum.OnayliMi = false;
                db.SaveChanges();
                TempData["Bilgi"] = "Onay işlemi Tamamlandı.Artık Yorum Onaylı Değil!";
                return RedirectToAction("YorumIndex", "Admin");
            }

            else if (gelenYorum.OnayliMi == false)
            {
                gelenYorum.OnayliMi = true;
                db.SaveChanges();
                TempData["Bilgi"] = "Onay işlemi Tamamlandı!Artık Kullanıcı Onaylı!";
                return RedirectToAction("YorumIndex", "Admin");
            }

            return View();
        }


        [LoginFilter]
        public JsonResult YorumSil(int ID)
        {
            Yorum dbYorum = db.Yorum.Find(ID);
            if (dbYorum == null)
            {
                return Json(new ResultJson { Success = false, Message = "Böyle Bir Yorum Bulunamadı!" });
            }
            db.Yorum.Remove(dbYorum);
            db.SaveChanges();
            return Json(new ResultJson { Success = true, Message = "Yorum Silindi!" });
        }


        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ugurcan.Models.DataContext;
using Ugurcan.Models.Tablolar;

namespace Ugurcan.Controllers
{
    public class AccountController : Controller
    {
        MagdiContext db = new MagdiContext();


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users users)
        {
            var login = db.Users.Where(u => u.Email == users.Email && u.Sifre == users.Sifre && u.OnayliMi == true).SingleOrDefault();
            if (login != null)
            {
                if (login.Email == users.Email && login.Sifre == users.Sifre)
                {
                    Session["uyeid"] = login.ID;
                    Session["email"] = login.Email;
                    return RedirectToAction("Anasayfa", "Admin");
                }
                else
                {
                    TempData["Bilgi"] = "Yanlış Giriş Bilgileri.Lütfen Tekrar Deneyiniz!";
                    return View();
                }
            }
            else
            {
                TempData["Bilgi"] = "Giriş Bilgileri Yanlış yada Üyeliğiniz Onaylanmamış.Lütfen Tekrar deneyiniz yada üyeliğinizin onaylanmasını bekleyiniz!";
                return View();
            }

        }

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Abandon();
        //    return RedirectToAction("Login", "Account");

        //}


        #region Register
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Users users)
        {

            var login = db.Users.Where(u => u.Email == users.Email).SingleOrDefault();
            if (login != null)
            {
                TempData["Bilgi"] = "Bu mail Adresi Kullanılamaz Lütfen Başka Bir Mail Adresi giriniz!";
                return View(users);
            }
            else
            {
                if (users.AdSoyad.Length > 60 || users.Email.Length > 60 || users.Sifre.Length > 15 || users.Sifre.Length < 3 || users.AdSoyad.Length < 3)
                {
                    TempData["Bilgi2"] = "Bilgileri Yanlış yada eksik girdiniz.Tekrar Deneyiniz!";
                    return View(users);
                }

                else
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    TempData["Bilgi3"] = "Teşekkürler. Üyeliğiniz Sistemimiz Tarafından Tanımlandıktan Sonra Giriş Yapacaksınız!";
                    return RedirectToAction("Login", "Account");
                }
            }


        }
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Anasayfa", "Home");
        }
        #endregion
    }
}
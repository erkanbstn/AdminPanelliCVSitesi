using CV.Models.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CV.Controllers
{
    public class YonetimController : Controller
    {
        // GET: Yonetim

        public ActionResult YonetimGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YonetimGiris(CVAdmin u)
        {
            var uye = Baglanti.db.CVAdmin.FirstOrDefault(c => c.Kullanici == u.Kullanici && c.Sifre == u.Sifre);
            if (uye != null)
            {
                FormsAuthentication.SetAuthCookie(uye.Kullanici, false);
                Session["ID"] = uye.ID;
                return RedirectToAction("AnaKimlik", "Yonetim");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AnaKimlik()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Kimlik.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult KimlikGetir2(byte id)
        {
            var x = Baglanti.db.Kimlik.Find(id);
            return View("KimlikGetir2", x);
        }
        [HttpPost]
        public ActionResult DosyaYukle(HttpPostedFileBase dosya, byte id)
        {
            if (dosya != null && dosya.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/CV/assets/img/"), dosya.FileName);
                dosya.SaveAs(path);
                var x = Baglanti.db.Kimlik.Find(id);
                x.Resim = dosya.FileName;
                Baglanti.db.SaveChanges();
            }
            return RedirectToAction("AnaKimlik");
        }

        public ActionResult KimlikGuncelle2(Kimlik d)
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Kimlik.Find(d.ID);
                x.Aciklama = d.Aciklama;
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaKimlik");
            }
            return RedirectToAction("YonetimGiris");
        }
        public ActionResult KimlikGetir(byte id)
        {
            var x = Baglanti.db.Kimlik.Find(id);
            return View("KimlikGetir", x);
        }
        public ActionResult KimlikGuncelle(Kimlik d)
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Kimlik.Find(d.ID);
                x.Ad = d.Ad;
                x.Soyad = d.Soyad;
                x.Adres = d.Adres;
                x.Telefon = d.Telefon;
                x.Mail = d.Mail;
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaKimlik");
            }
            return RedirectToAction("YonetimGiris");
        }


        // PROJE BÖLÜMÜ     *************************************

        public ActionResult AnaProje()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Proje.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }


        public ActionResult ProjeSil(byte id)
        {
            var x = Baglanti.db.Proje.Find(id);
            Baglanti.db.Proje.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaProje");
        }

        [HttpGet]
        public ActionResult YeniProje()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }

        [HttpPost]
        public ActionResult YeniProje(HttpPostedFileBase dosya, Proje p)
        {
            if (dosya != null && dosya.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/CV/assets/img/"), dosya.FileName);
                dosya.SaveAs(path);
                p.Resim = dosya.FileName;
                Baglanti.db.Proje.Add(p);
                Baglanti.db.SaveChanges();
            }
            return RedirectToAction("AnaProje");
        }

        public ActionResult ProjeGetir(byte id)
        {
            var x = Baglanti.db.Proje.Find(id);
            return View("ProjeGetir", x);
        }
        [HttpPost]
        public ActionResult ProjeGuncelle(HttpPostedFileBase dosya, byte id)
        {
            if (dosya != null && dosya.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/CV/assets/img/"), dosya.FileName);
                dosya.SaveAs(path);
                var x = Baglanti.db.Proje.Find(id);
                x.Resim = dosya.FileName;
                Baglanti.db.SaveChanges();
            }
            return RedirectToAction("AnaProje");
        }









        // DENEYİM BÖLÜMÜ   *************************************

        public ActionResult AnaDeneyim()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Deneyim.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult DeneyimSil(byte id)
        {
            var x = Baglanti.db.Deneyim.Find(id);
            Baglanti.db.Deneyim.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaDeneyim");
        }

        [HttpGet]
        public ActionResult YeniDeneyim()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniDeneyim(Deneyim d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.Deneyim.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaDeneyim");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult DeneyimGetir(byte id)
        {
            var x = Baglanti.db.Deneyim.Find(id);
            return View("DeneyimGetir", x);
        }

        public ActionResult DeneyimGuncelle(Deneyim d)
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Deneyim.Find(d.ID);
                x.Baslik = d.Baslik;
                x.AltBaslik = d.AltBaslik;
                x.Aciklama = d.Aciklama;
                x.Tarih = d.Tarih;
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaDeneyim");
            }
            return RedirectToAction("YonetimGiris");
        }




        // EĞİTİM BÖLÜMÜ   *************************************

        public ActionResult AnaEgitim()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Egitim.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult EgitimSil(byte id)
        {
            var x = Baglanti.db.Egitim.Find(id);
            Baglanti.db.Egitim.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaEgitim");
        }

        [HttpGet]
        public ActionResult YeniEgitim()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniEgitim(Egitim d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.Egitim.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaEgitim");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult EgitimGetir(byte id)
        {
            var x = Baglanti.db.Egitim.Find(id);
            return View("EgitimGetir", x);
        }

        public ActionResult EgitimGuncelle(Egitim d)
        {
            var x = Baglanti.db.Egitim.Find(d.ID);
            x.Baslik = d.Baslik;
            x.AltBaslik1 = d.AltBaslik1;
            x.AltBaslik2 = d.AltBaslik2;
            x.Tarih = d.Tarih;
            x.Gno = d.Gno;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaEgitim");
        }




        // YETENEK BÖLÜMÜ   *************************************

        public ActionResult AnaYetenek()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Yetenek.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult YetenekSil(byte id)
        {
            var x = Baglanti.db.Yetenek.Find(id);
            Baglanti.db.Yetenek.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaYetenek");
        }

        [HttpGet]
        public ActionResult YeniYetenek()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniYetenek(Yetenek d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.Yetenek.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaYetenek");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult YetenekGetir(byte id)
        {
            var x = Baglanti.db.Yetenek.Find(id);
            return View("YetenekGetir", x);
        }

        public ActionResult YetenekGuncelle(Yetenek d)
        {
            var x = Baglanti.db.Yetenek.Find(d.ID);
            x.Yetenek1 = d.Yetenek1;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaYetenek");
        }







        // ADMİN BÖLÜMÜ   ************************************

        public ActionResult AnaAdmin()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.CVAdmin.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }


        public ActionResult AdminSil(byte id)
        {
            var x = Baglanti.db.CVAdmin.Find(id);
            Baglanti.db.CVAdmin.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaAdmin");
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniAdmin(CVAdmin d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.CVAdmin.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaAdmin");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult AdminGetir(byte id)
        {
            var x = Baglanti.db.CVAdmin.Find(id);
            return View("AdminGetir", x);
        }

        public ActionResult AdminGuncelle(CVAdmin d)
        {
            var x = Baglanti.db.CVAdmin.Find(d.ID);
            x.Kullanici = d.Kullanici;
            x.Sifre = d.Sifre;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaAdmin");
        }









        // HOBİ BÖLÜMÜ   *************************************

        public ActionResult AnaHobi()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Hobi.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");

        }

        public ActionResult HobiSil(byte id)
        {
            var x = Baglanti.db.Hobi.Find(id);
            Baglanti.db.Hobi.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaHobi");
        }

        [HttpGet]
        public ActionResult YeniHobi()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniHobi(Hobi d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.Hobi.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaHobi");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult HobiGetir(byte id)
        {
            var x = Baglanti.db.Hobi.Find(id);
            return View("HobiGetir", x);
        }

        public ActionResult HobiGuncelle(Hobi d)
        {
            var x = Baglanti.db.Hobi.Find(d.ID);
            x.Aciklama1 = d.Aciklama1;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaHobi");
        }






        // SERTİFİKA BÖLÜMÜ   *************************************

        public ActionResult AnaSertifika()
        {
            if (Session["ID"] != null)
            {
                var x = Baglanti.db.Sertifika.ToList();
                return View(x);
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult SertifikaSil(byte id)
        {
            var x = Baglanti.db.Sertifika.Find(id);
            Baglanti.db.Sertifika.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaSertifika");
        }

        [HttpGet]
        public ActionResult YeniSertifika()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("YonetimGiris");
        }
        [HttpPost]
        public ActionResult YeniSertifika(Sertifika d)
        {
            if (Session["ID"] != null)
            {
                Baglanti.db.Sertifika.Add(d);
                Baglanti.db.SaveChanges();
                return RedirectToAction("AnaSertifika");
            }
            return RedirectToAction("YonetimGiris");
        }

        public ActionResult SertifikaGetir(byte id)
        {
            var x = Baglanti.db.Sertifika.Find(id);
            return View("SertifikaGetir", x);
        }

        public ActionResult SertifikaGuncelle(Sertifika d)
        {
            var x = Baglanti.db.Sertifika.Find(d.ID);
            x.Aciklama = d.Aciklama;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaSertifika");
        }



    }
}
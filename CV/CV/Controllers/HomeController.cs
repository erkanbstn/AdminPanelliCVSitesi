using CV.Models.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult OzGecmis()
        {
            var deger = Baglanti.db.Kimlik.ToList();
            return View(deger);
        }
        public PartialViewResult Deneyim()
        {
            var deger = Baglanti.db.Deneyim.ToList();
            return PartialView(deger);
        }
        public PartialViewResult Egitim()
        {
            var deger = Baglanti.db.Egitim.ToList();
            return PartialView(deger);
        }
        public PartialViewResult Yetenek()
        {
            var deger = Baglanti.db.Yetenek.ToList();
            return PartialView(deger);
        }
        public PartialViewResult Hobi()
        {
            var deger = Baglanti.db.Hobi.ToList();
            return PartialView(deger);
        }

        public PartialViewResult Sertifika()
        {
            var deger = Baglanti.db.Sertifika.ToList();
            return PartialView(deger);
        }

        public PartialViewResult Proje()
        {
            var deger = Baglanti.db.Proje.ToList();
            return PartialView(deger);
        }
    }
}
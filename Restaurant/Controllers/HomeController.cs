using Restaurant.Models.Domin;
using Restaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        private DbHolstentorEntities db = null;

        public HomeController()
        {
            db = new DbHolstentorEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Ueberuns()
        {
            return View();
        }
        public ActionResult Kontakt()
        {
            return View();
        }
        public ActionResult Galerie()
        {
            return View();
        }
        public ActionResult Impressum()
        {
            return View();
        }
        public ActionResult Datenschutz()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Kategoriesuche(string id) // CategorySearch
        {
            string categoryid = "";
            string titlecategory = "";
            List<string> lstcategory = new List<string>();
            var qcategory = id.Trim().Split('@');
            foreach (var item in qcategory)
            {
                string name = item.Trim();
                lstcategory.Add(name);
            }
            categoryid = lstcategory.LastOrDefault();
            titlecategory = lstcategory.FirstOrDefault();
            int? codecategoryid = Convert.ToInt32(categoryid);
            var qcategoryselect = db.Tbl_Kategorie.Where(a => a.ID == codecategoryid).SingleOrDefault();
            if (qcategoryselect == null || qcategory == null)
                return RedirectToAction(nameof(Error));
            var qproducts = db.Tbl_Produkte.Where(a => a.CategoryID == qcategoryselect.ID && a.ActiveInactive == true && a.Tbl_Kategorie.ActivePassive == true).Include(a => a.Tbl_Kategorie).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Unterkategorie.Tbl_UnterkategorieGalerie);
            if (qproducts == null)
                return View();
            List<VmCategoryProductViewModel> lstcategoryproducts = new List<VmCategoryProductViewModel>();
            foreach (var item in qproducts)
            {
                VmCategoryProductViewModel vm = new VmCategoryProductViewModel();
                vm.IDProduct = item.ID;
                vm.TitleProduct = item.Title;
                vm.DescriptionProduct = item.Description;
                vm.PriceProduct1 = item.Price1;
                vm.PriceProduct2 = item.Price2;
                vm.PriceProduct3 = item.Price3;
                vm.DateProduct = item.Date;
                vm.IDCategory = item.CategoryID;
                vm.TitleCategory = item.Tbl_Kategorie.TitleCategory;
                vm.DescriptionCategory = item.Tbl_Kategorie.Description;
                vm.IDSubCategory = item.SubCategoryID;
                vm.TitleSubCategory = item.Tbl_Unterkategorie.TitleSubCategory;
                vm.DescriptionSubCategory = item.Tbl_Unterkategorie.Description;
                vm.SubtitleSubCategory1 = item.Tbl_Unterkategorie.Subtitle1;
                vm.SubtitleSubCategory2 = item.Tbl_Unterkategorie.Subtitle2;
                vm.SubtitleSubCategory3 = item.Tbl_Unterkategorie.Subtitle3;
                vm.Position = item.Position;
                var qgalerie = item.Tbl_Galerie.Where(a => a.ProductID == item.ID).SingleOrDefault();
                var qunterkategoriegallery = item.Tbl_Unterkategorie.Tbl_UnterkategorieGalerie.Where(a => a.SubCategoryID == item.SubCategoryID).SingleOrDefault();
                if (qgalerie != null)
                {
                    vm.ImageGallery = "..\\wwwroot\\assets\\img\\backgrounds\\gallery\\" + qgalerie.Image;
                    vm.NamePicGallery = qgalerie.NamePic;
                }
                //if (qgalerie == null)
                //{
                //    vm.ImageGallery = "..\\wwwroot\\assets\\img\\backgrounds\\gallery\\default.jpg";
                //}
                if (qunterkategoriegallery != null)
                {
                    vm.ImageGallerySubCategory = "..\\wwwroot\\assets\\img\\backgrounds\\subategorygallery\\" + qunterkategoriegallery.Image;
                    vm.NamePicGallerySubCategory = qunterkategoriegallery.NamePic;
                }
                //if (qunterkategoriegallery == null)
                //{
                //    vm.ImageGallerySubCategory = "..\\wwwroot\\assets\\img\\backgrounds\\subategorygallery\\default.jpg";
                //}
                lstcategoryproducts.Add(vm);
            }
            return View(lstcategoryproducts ?? null);
        }
        [HttpGet]
        public ActionResult Unterkategoriesuche(string id) // SubCategorySearch
        {
            string subcategoryid = "";
            string titlesubcategory = "";
            List<string> lstsubcategory = new List<string>();
            var qsubcategory = id.Trim().Split('@');
            foreach (var item in qsubcategory)
            {
                string name = item.Trim();
                lstsubcategory.Add(name);
            }
            subcategoryid = lstsubcategory.LastOrDefault();
            titlesubcategory = lstsubcategory.FirstOrDefault();
            int? codesubcategoryid = Convert.ToInt32(subcategoryid);
            var qsubcategoryselect = db.Tbl_Unterkategorie.Where(a => a.ID == codesubcategoryid).SingleOrDefault();
            var qcategoryselect = db.Tbl_Kategorie.Where(a => a.ID == qsubcategoryselect.CategoryID).SingleOrDefault();
            if (qsubcategoryselect == null || qsubcategory == null)
                return RedirectToAction(nameof(Error));
            var qproducts = db.Tbl_Produkte.Where(a => a.CategoryID == qcategoryselect.ID && a.SubCategoryID == qsubcategoryselect.ID && a.ActiveInactive == true && a.Tbl_Unterkategorie.ActivePassive == true).Include(a => a.Tbl_Kategorie).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Unterkategorie.Tbl_UnterkategorieGalerie);
            if (qproducts == null)
                return View();
            List<VmCategoryProductViewModel> lstsubcategoryproducts = new List<VmCategoryProductViewModel>();
            foreach (var item in qproducts)
            {
                VmCategoryProductViewModel vm = new VmCategoryProductViewModel();
                vm.IDProduct = item.ID;
                vm.TitleProduct = item.Title;
                vm.DescriptionProduct = item.Description;
                vm.PriceProduct1 = item.Price1;
                vm.PriceProduct2 = item.Price2;
                vm.PriceProduct3 = item.Price3;
                vm.DateProduct = item.Date;
                vm.IDCategory = item.CategoryID;
                vm.TitleCategory = item.Tbl_Kategorie.TitleCategory;
                vm.DescriptionCategory = item.Tbl_Kategorie.Description;
                vm.IDSubCategory = item.SubCategoryID;
                vm.TitleSubCategory = item.Tbl_Unterkategorie.TitleSubCategory;
                vm.DescriptionSubCategory = item.Tbl_Unterkategorie.Description;
                vm.SubtitleSubCategory1 = item.Tbl_Unterkategorie.Subtitle1;
                vm.SubtitleSubCategory2 = item.Tbl_Unterkategorie.Subtitle2;
                vm.SubtitleSubCategory3 = item.Tbl_Unterkategorie.Subtitle3;
                vm.Position = item.Position;
                var qgalerie = item.Tbl_Galerie.Where(a => a.ProductID == item.ID).SingleOrDefault();
                var qunterkategoriegallery = item.Tbl_Unterkategorie.Tbl_UnterkategorieGalerie.Where(a => a.SubCategoryID == item.SubCategoryID).SingleOrDefault();
                if (qgalerie != null)
                {
                    vm.ImageGallery = "..\\wwwroot\\assets\\img\\backgrounds\\gallery\\" + qgalerie.Image;
                    vm.NamePicGallery = qgalerie.NamePic;
                }
                //if (qgalerie == null)
                //{
                //    vm.ImageGallery = "..\\wwwroot\\assets\\img\\backgrounds\\gallery\\default.jpg";
                //}
                if (qunterkategoriegallery != null)
                {
                    vm.ImageGallerySubCategory = "..\\wwwroot\\assets\\img\\backgrounds\\subategorygallery\\" + qunterkategoriegallery.Image;
                    vm.NamePicGallerySubCategory = qunterkategoriegallery.NamePic;
                }
                //if (qunterkategoriegallery == null)
                //{
                //    vm.ImageGallerySubCategory = "..\\wwwroot\\assets\\img\\backgrounds\\subategorygallery\\default.jpg";
                //}
                lstsubcategoryproducts.Add(vm);
            }
            return View(lstsubcategoryproducts ?? null);
        }
        [HttpPost]
        public ActionResult MessageUser(string name, string email, string message) // MessageUser
        {
            try
            {
                MailMessage msg = new MailMessage();
                //msg.Body = "<b>" + "Gastbenutzer: " + name + "</b>" + "<br />" + "<b>" + "E-Mail-Adresse: " + email + "</b>" + "<br />" + "<b>" + "Nachricht: " + "<br />" + message.ToString().Replace("\n", "<br />") + "</b>";
                msg.Body = "<b>" + "Gastbenutzer: " + name + "</b>" + "<br />" + "<b>" + "E-Mail-Adresse: " + "<a href='mailto:'" + email + "'>" + email + "</a>" + "</b>" + "<br />" + "<b>" + "Nachricht: " + "<br />" + message.ToString().Replace("\n", "<br />") + "</b>";
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.From = new MailAddress("kontakt@restaurant-amholstentor.de", "Gastbenutzer", Encoding.UTF8);
                msg.Priority = MailPriority.Normal;
                msg.Sender = msg.From;
                msg.Subject = "Gastbenutzer: " + email;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.To.Add(new MailAddress("kontakt@restaurant-amholstentor.de", "kontakt@restaurant-amholstentor.de", Encoding.UTF8));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.ionos.de";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("kontakt@restaurant-amholstentor.de", "+H0123456789");

                smtp.Send(msg);
                return RedirectToAction(nameof(Kontakt));

            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Kontakt));

            }
        }
    }
}
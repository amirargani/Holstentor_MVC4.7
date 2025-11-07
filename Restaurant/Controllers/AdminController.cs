using Restaurant.Models.Domin;
using Restaurant.Models.Repository;
using Restaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class AdminController : Controller
    {
        private DbHolstentorEntities db = null;
        public readonly HomeRep _ivm = new HomeRep();
        public readonly AdminRep _adr = new AdminRep();

        public AdminController()
        {
            db = new DbHolstentorEntities();
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View();
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        // Startseite
        [HttpPost]
        public ActionResult Startseite(int? id = null) // Homepage
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qindex = db.Tbl_Index.SingleOrDefault(m => m.IDIndex == id);
                    if (qindex == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qindex);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult StartseiteConfirmed(int? id, Tbl_Index model) // HomepageConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qindex = db.Tbl_Index.SingleOrDefault(m => m.IDIndex == id);
                    if (id != qindex.IDIndex)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        var qupdate = db.Tbl_Index.Where(a => a.IDIndex == qindex.IDIndex).SingleOrDefault();
                        qupdate.Title = model.Title;
                        qupdate.TypedText1 = model.TypedText1;
                        qupdate.TypedText2 = model.TypedText2;
                        qupdate.TypedText3 = model.TypedText3;
                        qupdate.TypedText4 = model.TypedText4;
                        qupdate.TypedText5 = model.TypedText5;
                        db.Tbl_Index.Attach(qupdate);
                        db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StartseiteExists(qindex.IDIndex))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool StartseiteExists(int id)
        {
            return db.Tbl_Index.Any(e => e.IDIndex == id);
        }
        // Startseite
        // KopfzeileFusszeileUeber
        [HttpPost]
        public ActionResult KopfzeileFusszeileUeber(int? id = null) // HeaderFooterAbout
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qindex = db.Tbl_IndexET.SingleOrDefault(m => m.IDIndex == id);
                    if (qindex == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qindex);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }

        }
        [HttpPost]
        public ActionResult KopfzeileFusszeileUeberConfirmed(int? id, Tbl_IndexET model) // HeaderFooterAboutConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qindex = db.Tbl_IndexET.SingleOrDefault(m => m.IDIndex == id);
                    if (id != qindex.IDIndex)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        var qupdate = db.Tbl_IndexET.Where(a => a.IDIndex == qindex.IDIndex).SingleOrDefault();
                        qupdate.Street = model.Street;
                        qupdate.Number = model.Number;
                        qupdate.PostCode = model.PostCode;
                        qupdate.City = model.City;
                        qupdate.Country = model.Country;
                        qupdate.Description = model.Description;
                        //qupdate.Email = model.Email;
                        qupdate.EmbedLinkGoogleMap = model.EmbedLinkGoogleMap;
                        qupdate.NameSite = model.NameSite;
                        qupdate.PhoneNumber = model.PhoneNumber;
                        db.Tbl_IndexET.Attach(qupdate);
                        db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!KopfzeileFußzeileUeberExists(qindex.IDIndex))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool KopfzeileFußzeileUeberExists(int id)
        {
            return db.Tbl_IndexET.Any(e => e.IDIndex == id);
        }
        // KopfzeileFusszeileUeber
        // Kategorie
        public ActionResult Kategorie(int page = 1) // Category
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 15;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    var qcategory = db.Tbl_Kategorie.ToList();
                    ViewData["CategoryCount"] = qcategory.Count();
                    ViewData["CategoryTakeNumber"] = TakeNumber;
                    ViewData["CategoryPage"] = page;
                    return View(qcategory.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult KategorieInaktiv(int? id) // CategoryInactive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Kategorie");
                    var acategory = db.Tbl_Kategorie.Where(m => m.ID == id).SingleOrDefault();
                    if (acategory == null)
                        return RedirectToAction("Kategorie");
                    acategory.ActivePassive = false;
                    db.Tbl_Kategorie.Attach(acategory);
                    db.Entry(acategory).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Kategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult KategorieAktiv(int? id) // CategoryActive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Kategorie");
                    var acategory = db.Tbl_Kategorie.Where(m => m.ID == id).SingleOrDefault();
                    if (acategory == null)
                        return RedirectToAction("Kategorie");
                    acategory.ActivePassive = true;
                    db.Tbl_Kategorie.Attach(acategory);
                    db.Entry(acategory).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Kategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        public ActionResult KategorieErstellen() // CategoryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View();
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult KategorieErstellen(Tbl_Kategorie category) // CategoryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        db.Tbl_Kategorie.Add(category);
                        db.SaveChanges();
                        return RedirectToAction("Kategorie");
                    }
                    return View(category);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult KategorieBearbeiten(int? id) // CategoryEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Kategorie");
                    }
                    var qcategory = db.Tbl_Kategorie.SingleOrDefault(m => m.ID == id);
                    if (qcategory == null)
                    {
                        return RedirectToAction("Kategorie");
                    }
                    return View(qcategory);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult KategorieBearbeitenConfirmed(int id, Tbl_Kategorie category) // CategoryEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != category.ID)
                    {
                        return RedirectToAction("Kategorie");
                    }
                    try
                    {
                        var qupdate = db.Tbl_Kategorie.Where(a => a.ID == category.ID).SingleOrDefault();
                        qupdate.TitleCategory = category.TitleCategory;
                        qupdate.FontName = category.FontName;
                        qupdate.Description = category.Description;
                        qupdate.ActivePassive = category.ActivePassive;
                        db.Tbl_Kategorie.Attach(qupdate);
                        db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Kategorie");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!KategorieExists(category.ID))
                        {
                            return RedirectToAction("Kategorie");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool KategorieExists(int id)
        {
            return db.Tbl_Kategorie.Any(e => e.ID == id);
        }
        // Kategorie
        // Unterkategorie
        public ActionResult Unterkategorie(int page = 1) // SubCategory
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 25;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    var qsubcategory = db.Tbl_Unterkategorie.Include(a => a.Tbl_UnterkategorieGalerie).Include(a => a.Tbl_Kategorie).OrderBy(a => a.CategoryID).ToList();
                    ViewData["SubCategoryCount"] = qsubcategory.Count();
                    ViewData["SubCategoryTakeNumber"] = TakeNumber;
                    ViewData["SubCategoryPage"] = page;
                    return View(qsubcategory.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }

        }
        [HttpPost]
        public ActionResult UnterkategorieInaktiv(int? id) // SubCategoryInactive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Unterkategorie");
                    var asubcategory = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                    if (asubcategory == null)
                        return RedirectToAction("Unterkategorie");
                    asubcategory.ActivePassive = false;
                    db.Tbl_Unterkategorie.Attach(asubcategory);
                    db.Entry(asubcategory).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Unterkategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategorieAktiv(int? id) // SubCategoryActive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Unterkategorie");
                    var asubcategory = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                    if (asubcategory == null)
                        return RedirectToAction("Unterkategorie");
                    asubcategory.ActivePassive = true;
                    db.Tbl_Unterkategorie.Attach(asubcategory);
                    db.Entry(asubcategory).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Unterkategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        public ActionResult UnterkategorieErstellen() // SubCategoryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    ViewBag.CategoryID = new SelectList(db.Tbl_Kategorie, "ID", "TitleCategory");
                    return View();
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategorieErstellen(Tbl_Unterkategorie subcategory) // SubCategoryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        db.Tbl_Unterkategorie.Add(subcategory);
                        db.SaveChanges();
                        return RedirectToAction("Unterkategorie");
                    }
                    return View(subcategory);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategorieBearbeiten(int? id) // SubCategoryEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    var qsubcategory = db.Tbl_Unterkategorie.SingleOrDefault(m => m.ID == id);
                    if (qsubcategory == null)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    //ViewBag.CategoryID = new SelectList(db.Tbl_Kategorie, "ID", "TitleCategory");
                    ViewBag.CategoryID = new SelectList(db.Tbl_Kategorie.Where(m => m.ID == qsubcategory.CategoryID), "ID", "TitleCategory");
                    return View(qsubcategory);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategorieBearbeitenConfirmed(int id, Tbl_Unterkategorie subcategory)  // SubCategoryEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != subcategory.ID)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    try
                    {
                        var qproducts = db.Tbl_Produkte.Where(a => a.SubCategoryID == subcategory.ID).ToList();
                        var qupdate = db.Tbl_Unterkategorie.Where(a => a.ID == subcategory.ID).SingleOrDefault();
                        foreach (var item in qproducts)
                        {
                            if (item.SubCategoryID == subcategory.ID)
                            {
                                item.CategoryID = subcategory.CategoryID;
                                db.Tbl_Produkte.Attach(item);
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        qupdate.TitleSubCategory = subcategory.TitleSubCategory;
                        qupdate.CategoryID = subcategory.CategoryID;
                        qupdate.FontName = subcategory.FontName;
                        qupdate.Description = subcategory.Description;
                        qupdate.Subtitle1 = subcategory.Subtitle1;
                        qupdate.Subtitle2 = subcategory.Subtitle2;
                        qupdate.Subtitle3 = subcategory.Subtitle3;
                        qupdate.ActivePassive = subcategory.ActivePassive;
                        db.Tbl_Unterkategorie.Attach(qupdate);
                        db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Unterkategorie");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UnterkategorieExists(subcategory.ID))
                        {
                            return RedirectToAction("Unterkategorie");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool UnterkategorieExists(int id)
        {
            return db.Tbl_Unterkategorie.Any(e => e.ID == id);
        }
        // Unterkategorie
        // Galerie
        public ActionResult Galerie(int page = 1) // Album
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 25;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    var qalbum = db.Tbl_Album.OrderByDescending(a => a.Date).ToList();
                    ViewData["AlbumCount"] = qalbum.Count();
                    ViewData["AlbumTakeNumber"] = TakeNumber;
                    ViewData["AlbumPage"] = page;
                    return View(qalbum.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        public ActionResult GalerieErstellen() // AlbumCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View();
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult GalerieErstellen(Tbl_Album album, HttpPostedFileBase Image) // AlbumCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\album\\");
                                if (Image.ContentLength <= 5242880) // > 0 - 5 MB
                                {
                                    Random rnd = new Random();
                                    string FileName = "album-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                    Image.SaveAs(Path.Combine(uploads + FileName));
                                    Tbl_Album addalbum = new Tbl_Album();
                                    addalbum.Date = DateTime.Now;
                                    addalbum.Image = FileName;
                                    addalbum.NamePic = album.NamePic;
                                    db.Tbl_Album.Add(addalbum);
                                    db.SaveChanges();
                                    return RedirectToAction("Galerie");
                                }
                                else
                                    return View(album);
                            }
                        }
                        else
                            return View(album);
                    }
                    return View(album);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult GalerieBearbeiten(int? id) // AlbumEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Galerie");
                    }
                    var qalbum = db.Tbl_Album.SingleOrDefault(m => m.ID == id);
                    if (qalbum == null)
                    {
                        return RedirectToAction("Galerie");
                    }
                    return View(qalbum);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult GalerieBearbeitenConfirmed(int id, Tbl_Album album, HttpPostedFileBase Image) // AlbumEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != album.ID)
                    {
                        return RedirectToAction("Galerie");
                    }
                    try
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                var qupdate = db.Tbl_Album.Where(a => a.ID == album.ID).SingleOrDefault();
                                if (Image.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\album\\");
                                    var qalbumdelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\album\\" + qupdate.Image);
                                    if (System.IO.File.Exists(qalbumdelete))
                                    {
                                        System.IO.File.Delete(qalbumdelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "album-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                    Image.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.NamePic = album.NamePic;
                                    qupdate.Image = FileName;
                                    db.Tbl_Album.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var qupdate = db.Tbl_Album.Where(a => a.ID == album.ID).SingleOrDefault();
                            qupdate.NamePic = album.NamePic;
                            db.Tbl_Album.Attach(qupdate);
                            db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Galerie");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GalerieExists(album.ID))
                        {
                            return RedirectToAction("Galerie");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult GalerieLöschen(int? id) // AlbumDelete
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Galerie");
                    }
                    var qalbum = db.Tbl_Album.SingleOrDefault(m => m.ID == id);
                    if (qalbum == null)
                    {
                        return RedirectToAction("Galerie");
                    }
                    return View(qalbum);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult GalerieLöschenConfirmed(int id) // AlbumDeleteConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qalbum = db.Tbl_Album.SingleOrDefault(m => m.ID == id);
                    var qalbumdelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\album\\" + qalbum.Image);
                    if (System.IO.File.Exists(qalbumdelete))
                    {
                        System.IO.File.Delete(qalbumdelete);
                    }
                    db.Tbl_Album.Remove(qalbum);
                    db.SaveChanges();
                    return RedirectToAction("Galerie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool GalerieExists(int id)
        {
            return db.Tbl_Album.Any(e => e.ID == id);
        }
        // Galerie
        // Hochladen
        [HttpPost]
        public ActionResult VideoBearbeiten(int? id = null) // VideoEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qvideo = db.Tbl_Hochladen.SingleOrDefault(m => m.ID == id);
                    if (qvideo == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qvideo);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult VideoBearbeitenConfirmed(int id, Tbl_Hochladen upload, HttpPostedFileBase Video) // VideoEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != upload.ID)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        if (Video != null)
                        {
                            if (Video.ContentType == "video/mp4")
                            {
                                var qupdate = db.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefault();
                                if (Video.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\video\\");
                                    var qvideodelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\video\\" + qupdate.Video);
                                    if (System.IO.File.Exists(qvideodelete))
                                    {
                                        System.IO.File.Delete(qvideodelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "video-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Video.FileName.Split('.').Last();
                                    Video.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.Video = FileName;
                                    db.Tbl_Hochladen.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UploadExists(upload.ID))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoUeberunsBearbeiten(int? id = null) // Edit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qimage = db.Tbl_Hochladen.SingleOrDefault(m => m.ID == id);
                    if (qimage == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qimage);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoUeberunsBearbeitenConfirmed(int id, Tbl_Hochladen upload, HttpPostedFileBase ImageAbout) // EditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != upload.ID)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        if (ImageAbout != null)
                        {
                            if (ImageAbout.ContentType == "image/jpeg")
                            {
                                var qupdate = db.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefault();
                                if (ImageAbout.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\about\\");
                                    var qimagedelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\about\\" + qupdate.ImageAbout);
                                    if (System.IO.File.Exists(qimagedelete))
                                    {
                                        System.IO.File.Delete(qimagedelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "about-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + ImageAbout.FileName.Split('.').Last();
                                    ImageAbout.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.ImageAbout = FileName;
                                    db.Tbl_Hochladen.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UploadExists(upload.ID))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoGalerieBearbeiten(int? id = null) // Edit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qimage = db.Tbl_Hochladen.SingleOrDefault(m => m.ID == id);
                    if (qimage == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qimage);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoGalerieBearbeitenConfirmed(int id, Tbl_Hochladen upload, HttpPostedFileBase ImageGallery) // EditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != upload.ID)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        if (ImageGallery != null)
                        {
                            if (ImageGallery.ContentType == "image/jpeg")
                            {
                                var qupdate = db.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefault();
                                if (ImageGallery.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\gallery\\");
                                    var qimagedelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\gallery\\" + qupdate.ImageGallery);
                                    if (System.IO.File.Exists(qimagedelete))
                                    {
                                        System.IO.File.Delete(qimagedelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + ImageGallery.FileName.Split('.').Last();
                                    ImageGallery.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.ImageGallery = FileName;
                                    db.Tbl_Hochladen.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UploadExists(upload.ID))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoFusszeileBearbeiten(int? id = null) // Edit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var qimage = db.Tbl_Hochladen.SingleOrDefault(m => m.ID == id);
                    if (qimage == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(qimage);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult FotoFusszeileBearbeitenConfirmed(int id, Tbl_Hochladen upload, HttpPostedFileBase ImageFooter) // EditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != upload.ID)
                    {
                        return RedirectToAction("Index");
                    }
                    try
                    {
                        if (ImageFooter != null)
                        {
                            if (ImageFooter.ContentType == "image/jpeg")
                            {
                                var qupdate = db.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefault();
                                if (ImageFooter.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\footer\\");
                                    var qimagedelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\upload\\footer\\" + qupdate.ImageFooter);
                                    if (System.IO.File.Exists(qimagedelete))
                                    {
                                        System.IO.File.Delete(qimagedelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "footer-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + ImageFooter.FileName.Split('.').Last();
                                    ImageFooter.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.ImageFooter = FileName;
                                    db.Tbl_Hochladen.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UploadExists(upload.ID))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool UploadExists(int id)
        {
            return db.Tbl_Hochladen.Any(e => e.ID == id);
        }
        // Hochladen
        // Benutzer
        public ActionResult DetailsBenutzer(int page = 1) // DetailsUsers
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 25;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    ViewData["DetailsBenutzerCount"] = _adr.GetUsersSendAll().Count();
                    ViewData["DetailsBenutzerTakeNumber"] = TakeNumber;
                    ViewData["DetailsBenutzerPage"] = page;
                    return View(_adr.GetUsersSendAll().Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult DetailsBenutzerShow(string id) //  DetailsUserShow
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View(_adr.GetDetailsShow(id));
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult InaktivBenutzer(string id) // InactiveUser
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                        return RedirectToAction("DetailsBenutzer");
                    var applicationUser = db.Tbl_Users.Where(m => m.ID == id).SingleOrDefault();
                    if (applicationUser == null)
                        return RedirectToAction("DetailsBenutzer");
                    applicationUser.EmailConfirmed = false;
                    db.Tbl_Users.Attach(applicationUser);
                    db.Entry(applicationUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetailsBenutzer");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult AktivBenutzer(string id) // ActiveUser
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                        return RedirectToAction("DetailsBenutzer");
                    var applicationUser = db.Tbl_Users.Where(m => m.ID == id).SingleOrDefault();
                    if (applicationUser == null)
                        return RedirectToAction("DetailsBenutzer");
                    applicationUser.EmailConfirmed = true;
                    db.Tbl_Users.Attach(applicationUser);
                    db.Entry(applicationUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetailsBenutzer");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        // Benutzer
        // Nachrichten
        public ActionResult Nachrichten(int page = 1)
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 30;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    string admin = _ivm.GetUserRole(Session["EmailUser"].ToString());
                    var quserrecive = db.Tbl_Roles.Where(a => a.Name == admin).FirstOrDefault();
                    var quser = db.Tbl_Users.Where(a => a.RoleID == quserrecive.ID).First();
                    var qmessage = db.Tbl_Nachricht.Where(a => a.UserIdRecive == quser.RoleID).ToList();
                    ViewData["MessagesCount"] = qmessage.Count();
                    ViewData["MessagesTakeNumber"] = TakeNumber;
                    ViewData["MessagesPage"] = page;
                    return View(qmessage.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult NachrichtenAntworten(int? id)
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Nachrichten");
                    }
                    var qmessage = db.Tbl_Nachricht.Where(a => a.ID == id).FirstOrDefault();
                    if (qmessage == null)
                    {
                        return RedirectToAction("Nachrichten");
                    }
                    //return View(qmessage);
                    ViewData["ID"] = qmessage.ID;
                    ViewData["TextUser"] = qmessage.Text;
                    return View();
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult NachrichtenAntwortenConfirmed(int id, Tbl_Nachricht nachricht)
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    try
                    {
                        var qid = db.Tbl_Nachricht.Where(a => a.ID == id).First();
                        qid.Confirm = true;
                        qid.TextAdmin = nachricht.TextAdmin;
                        qid.DateUpdate = DateTime.Now;
                        db.Tbl_Nachricht.Attach(qid);
                        db.Entry(qid).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Nachrichten");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!NachrichtenExists(nachricht.ID))
                        {
                            return RedirectToAction("Nachrichten");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool NachrichtenExists(int id)
        {
            return db.Tbl_Nachricht.Any(e => e.ID == id);
        }
        // Nachrichten
        // PasswortAendern
        public ActionResult PasswortAendern() // ChangePassword
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View();
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult PasswortAendern(ChangePasswordViewModel model) // ChangePassword
        {
            if (Session["EmailUser"] != null)
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var quser = db.Tbl_Users.Where(a => a.UserName == username).FirstOrDefault();
                    if (quser.PasswordHash == Crypto.SHA256(model.OldPassword))
                    {
                        quser.PasswordHash = Crypto.SHA256(model.NewPassword);
                        db.Tbl_Users.Attach(quser);
                        db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        return RedirectToAction(nameof(AccountController.Ausloggen), "Account");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
        }
        // PasswortAendern
        //Produkte
        public ActionResult Produkte(int page = 1) //Products
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 500;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    var qproducts = db.Tbl_Produkte.Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Kategorie).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Users).ToList();
                    ViewData["ProductsCount"] = qproducts.Count();
                    ViewData["ProductsTakeNumber"] = TakeNumber;
                    ViewData["ProductsPage"] = page;
                    return View(qproducts.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult Produkte(int[] locationId, int page = 1) //ProductsUpdateTable
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int preference = 1;
                    foreach (int id in locationId)
                    {
                        var holidayLocation = db.Tbl_Produkte.Find(id);
                        holidayLocation.Position = preference;
                        db.SaveChanges();
                        preference += 1;
                    }
                    return RedirectToAction(nameof(AdminController.Produkte), "Admin");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktInaktiv(int? id) // ProductInactive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Produkte");
                    var aproducts = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    if (aproducts == null)
                        return RedirectToAction("Produkte");
                    aproducts.ActiveInactive = false;
                    db.Tbl_Produkte.Attach(aproducts);
                    db.Entry(aproducts).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Produkte");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktAktiv(int? id) // ProductActive
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id <= 0)
                        return RedirectToAction("Produkte");
                    var aproducts = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    if (aproducts == null)
                        return RedirectToAction("Produkte");
                    aproducts.ActiveInactive = true;
                    db.Tbl_Produkte.Attach(aproducts);
                    db.Entry(aproducts).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Produkte");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        public ActionResult ProduktKategorie() // ProductKategorie
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return View(_ivm.GetCategoryLink());
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktErstellen(int id = 0) // ProductsCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.CategoryID == id).FirstOrDefault();
                    if (qsubcategory == null)
                        return RedirectToAction("ProduktKategorie");
                    ViewBag.SubCategoryID = new SelectList(db.Tbl_Unterkategorie.Where(a => a.CategoryID == id), "ID", "TitleSubCategory");
                    return View();
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktErstellenConfirmed(Tbl_Produkte products) // ProductsCreateConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        var userid = db.Tbl_Users.Where(a => a.Email == username && a.EmailConfirmed == true).FirstOrDefault();
                        var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.ID == products.SubCategoryID).FirstOrDefault();
                        Tbl_Produkte pk = new Tbl_Produkte();
                        pk.Title = products.Title;
                        pk.CategoryID = qsubcategory.CategoryID;
                        pk.SubCategoryID = qsubcategory.ID;
                        pk.Price1 = products.Price1;
                        pk.Price2 = products.Price2;
                        pk.Price3 = products.Price3;
                        pk.Description = products.Description;
                        pk.ActiveInactive = true;
                        pk.Date = DateTime.Now;
                        pk.UserID = userid.ID;
                        db.Tbl_Produkte.Add(pk);
                        if (db.SaveChanges() > 0)
                            return RedirectToAction("Produkte");
                    }
                    return View(products);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProdukteBearbeiten(int? id) // ProductsEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Produkte");
                    }
                    var qproducts = db.Tbl_Produkte.SingleOrDefault(m => m.ID == id);
                    if (qproducts == null)
                    {
                        return RedirectToAction("Produkte");
                    }
                    ViewBag.CategoryID = new SelectList(db.Tbl_Kategorie.Where(a => a.ID == qproducts.CategoryID), "ID", "TitleCategory");
                    ViewBag.SubCategoryID = new SelectList(db.Tbl_Unterkategorie.Where(a => a.ID == qproducts.SubCategoryID), "ID", "TitleSubCategory");
                    //ViewBag.SubCategoryID = new SelectList(db.Tbl_Unterkategorie.Where(a => a.CategoryID == qproducts.CategoryID), "ID", "TitleSubCategory");
                    return View(qproducts);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProdukteBearbeitenConfirmed(int id, Tbl_Produkte products) // ProductsEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id != products.ID)
                    {
                        return RedirectToAction("Produkte");
                    }
                    try
                    {
                        var userid = db.Tbl_Users.Where(a => a.UserName == username && a.EmailConfirmed == true).FirstOrDefault();
                        var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.ID == products.SubCategoryID).FirstOrDefault();
                        var qupdate = db.Tbl_Produkte.Where(a => a.ID == products.ID).SingleOrDefault();
                        qupdate.Title = products.Title;
                        qupdate.CategoryID = qsubcategory.CategoryID;
                        qupdate.SubCategoryID = products.SubCategoryID;
                        qupdate.Price1 = products.Price1;
                        qupdate.Price2 = products.Price2;
                        qupdate.Price3 = products.Price3;
                        qupdate.Description = products.Description;
                        qupdate.ActiveInactive = products.ActiveInactive;
                        qupdate.UserID = userid.ID;
                        db.Tbl_Produkte.Attach(qupdate);
                        db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewData["CategoryID"] = new SelectList(db.Tbl_Kategorie, "ID", "TitleCategory", products.CategoryID);
                        return RedirectToAction("Produkte");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdukteExists(products.ID))
                        {
                            return RedirectToAction("Produkte");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool ProdukteExists(int id)
        {
            return db.Tbl_Produkte.Any(e => e.ID == id);
        }
        // Produkte
        // Produktgalerie
        public ActionResult Produktgalerie(int page = 1) // Gallery
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    int TakeNumber = 200;
                    int SkipNumber = (TakeNumber * page) - TakeNumber;
                    var qgallery = db.Tbl_Produkte.Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Kategorie).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Users).ToList();
                    ViewData["GalleryCount"] = qgallery.Count();
                    ViewData["GalleryTakeNumber"] = TakeNumber;
                    ViewData["GalleryPage"] = page;
                    return View(qgallery.Skip(SkipNumber).Take(TakeNumber) ?? null);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktgalerieErstellen(int? id) // GalleryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Produktgalerie");
                    }
                    var qproduct = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    if (qproduct == null)
                        return RedirectToAction("Produktgalerie");
                    ViewData["ProductID"] = qproduct.ID;
                    return View();
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktgalerieErstellenConfirmed(int id, HttpPostedFileBase Image) // GalleryCreateConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                if (Image.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\gallery\\");
                                    var qproduct = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                                    if (Image.ContentLength <= 5242880)
                                    {
                                        Random rnd = new Random();
                                        string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                        Image.SaveAs(Path.Combine(uploads + FileName));
                                        Tbl_Galerie addgallery = new Tbl_Galerie();
                                        addgallery.Date = DateTime.Now;
                                        addgallery.Image = FileName;
                                        addgallery.NamePic = qproduct.Title;
                                        addgallery.ProductID = qproduct.ID;
                                        db.Tbl_Galerie.Add(addgallery);
                                        db.SaveChanges();
                                        return RedirectToAction("Produktgalerie");
                                    }
                                }
                            }
                        }
                    }
                    return RedirectToAction("Produktgalerie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktgalerieBearbeiten(int? id) // GalleryEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Produktgalerie");
                    }
                    var qproduct = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    var qgallery = db.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefault();
                    if (qproduct == null)
                    {
                        return RedirectToAction("Produktgalerie");
                    }
                    return View(qgallery);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktgalerieBearbeitenConfirmed(int id, HttpPostedFileBase Image) // GalleryEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qproduct = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    var qgallery = db.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefault();
                    if (id != qgallery.ProductID)
                    {
                        return RedirectToAction("Produktgalerie");
                    }
                    try
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                var qupdate = db.Tbl_Galerie.Where(a => a.ID == qgallery.ID).SingleOrDefault();
                                if (Image.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\gallery\\");
                                    var qgallerydelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\gallery\\" + qupdate.Image);
                                    if (System.IO.File.Exists(qgallerydelete))
                                    {
                                        System.IO.File.Delete(qgallerydelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                    Image.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.NamePic = qproduct.Title;
                                    qupdate.Image = FileName;
                                    db.Tbl_Galerie.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var qupdate = db.Tbl_Galerie.Where(a => a.ID == qgallery.ID).SingleOrDefault();
                            qupdate.NamePic = qproduct.Title;
                            db.Tbl_Galerie.Attach(qupdate);
                            db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Produktgalerie");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProduktgalerieExists(id))
                        {
                            return RedirectToAction("Produktgalerie");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult ProduktgalerieLöschenConfirmed(int id) // GalleryDeleteConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qproduct = db.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                    var qgallery = db.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefault();
                    var qgallerydelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\gallery\\" + qgallery.Image);
                    if (System.IO.File.Exists(qgallerydelete))
                    {
                        System.IO.File.Delete(qgallerydelete);
                    }
                    db.Tbl_Galerie.Remove(qgallery);
                    db.SaveChanges();
                    return RedirectToAction("Produktgalerie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool ProduktgalerieExists(int id)
        {
            return db.Tbl_Galerie.Any(e => e.ProductID == id);
        }
        // Produktgalerie
        // Unterkategoriegalerie
        [HttpPost]
        public ActionResult UnterkategoriegalerieErstellen(int? id) // SubCategoryGalleryCreate
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                if (id == null)
                {
                    return RedirectToAction("Unterkategorie");
                }
                var qunterkategorie = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                if (qunterkategorie == null)
                    return RedirectToAction("Unterkategorie");
                ViewData["SubCategoryID"] = qunterkategorie.ID;
                return View();
            }
        }

        [HttpPost]
        public ActionResult UnterkategoriegalerieErstellenConfirmed(int id, HttpPostedFileBase Image) // SubCategoryGalleryCreateConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                if (Image.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\subategorygallery\\");
                                    var qunterkategorie = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                                    if (Image.ContentLength <= 5242880)
                                    {
                                        Random rnd = new Random();
                                        string FileName = "subategorygallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                        Image.SaveAs(Path.Combine(uploads + FileName));
                                        Tbl_UnterkategorieGalerie addgallery = new Tbl_UnterkategorieGalerie();
                                        addgallery.Date = DateTime.Now;
                                        addgallery.Image = FileName;
                                        addgallery.NamePic = qunterkategorie.TitleSubCategory;
                                        addgallery.SubCategoryID = qunterkategorie.ID;
                                        db.Tbl_UnterkategorieGalerie.Add(addgallery);
                                        db.SaveChanges();
                                        return RedirectToAction("Unterkategorie");
                                    }
                                }
                            }
                        }
                    }
                    return RedirectToAction("Unterkategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategoriegalerieBearbeiten(int? id) // GalleryEdit
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    var qunterkategorie = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                    var qunterkategoriegallery = db.Tbl_UnterkategorieGalerie.Where(m => m.SubCategoryID == qunterkategorie.ID).SingleOrDefault();
                    if (qunterkategorie == null)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    return View(qunterkategoriegallery);
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategoriegalerieBearbeitenConfirmed(int id, HttpPostedFileBase Image) // GalleryEditConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qunterkategorie = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                    var qunterkategoriegallery = db.Tbl_UnterkategorieGalerie.Where(m => m.SubCategoryID == qunterkategorie.ID).SingleOrDefault();
                    if (id != qunterkategoriegallery.SubCategoryID)
                    {
                        return RedirectToAction("Unterkategorie");
                    }
                    try
                    {
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                            {
                                var qupdate = db.Tbl_UnterkategorieGalerie.Where(a => a.ID == qunterkategoriegallery.ID).SingleOrDefault();
                                if (Image.ContentLength <= 5242880)
                                {
                                    var uploads = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\subategorygallery\\");
                                    var qgallerydelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\subategorygallery\\" + qupdate.Image);
                                    if (System.IO.File.Exists(qgallerydelete))
                                    {
                                        System.IO.File.Delete(qgallerydelete);
                                    }
                                    Random rnd = new Random();
                                    string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                                    Image.SaveAs(Path.Combine(uploads + FileName));
                                    qupdate.NamePic = qunterkategorie.TitleSubCategory;
                                    qupdate.Image = FileName;
                                    db.Tbl_UnterkategorieGalerie.Attach(qupdate);
                                    db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var qupdate = db.Tbl_UnterkategorieGalerie.Where(a => a.ID == qunterkategoriegallery.ID).SingleOrDefault();
                            qupdate.NamePic = qunterkategorie.TitleSubCategory;
                            db.Tbl_UnterkategorieGalerie.Attach(qupdate);
                            db.Entry(qupdate).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Unterkategorie");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UnterkategoriegalerieExists(id))
                        {
                            return RedirectToAction("Unterkategorie");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult UnterkategoriegalerieLöschenConfirmed(int id) // GalleryDeleteConfirmed
        {
            if (Session["EmailUser"] == null)
            {
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                {
                    var qunterkategorie = db.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
                    var qunterkategoriegallery = db.Tbl_UnterkategorieGalerie.Where(m => m.SubCategoryID == qunterkategorie.ID).SingleOrDefault();
                    var qgallerydelete = Path.Combine(Server.MapPath("~") + "wwwroot\\assets\\img\\backgrounds\\subategorygallery\\" + qunterkategoriegallery.Image);
                    if (System.IO.File.Exists(qgallerydelete))
                    {
                        System.IO.File.Delete(qgallerydelete);
                    }
                    db.Tbl_UnterkategorieGalerie.Remove(qunterkategoriegallery);
                    db.SaveChanges();
                    return RedirectToAction("Unterkategorie");
                }
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        private bool UnterkategoriegalerieExists(int id)
        {
            return db.Tbl_UnterkategorieGalerie.Any(e => e.SubCategoryID == id);
        }
        // Unterkategoriegalerie
    }
}
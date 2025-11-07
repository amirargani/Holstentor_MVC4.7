using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Models.Domin;
using Restaurant.Models.ViewModels;

namespace Restaurant.Models.Repository
{
    public class HomeRep : IDisposable
    {
        private DbHolstentorEntities db = null;
        public HomeRep()
        {
            db = new DbHolstentorEntities();
        }
        public Tbl_Index GetIndexSelect(string index = null)
        {
            try
            {
                var qindex = db.Tbl_Index.Where(a => a.IDIndex > 0).SingleOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    Tbl_Index ivm = new Tbl_Index();
                    ivm.Title = qindex.Title;
                    ivm.TypedText1 = qindex.TypedText1;
                    ivm.TypedText2 = qindex.TypedText2;
                    ivm.TypedText3 = qindex.TypedText3;
                    ivm.TypedText4 = qindex.TypedText4;
                    ivm.TypedText5 = qindex.TypedText5;
                    return ivm;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tbl_Hochladen GetHochladen(string index = null)
        {
            try
            {
                var qupload = db.Tbl_Hochladen.Where(a => a.ID > 0).SingleOrDefault();
                if (qupload == null)
                    return null;
                else
                {
                    Tbl_Hochladen up = new Tbl_Hochladen();
                    up.ID = qupload.ID;
                    up.Video = qupload.Video;
                    up.Logo = qupload.Logo;
                    up.ImageAbout = qupload.ImageAbout;
                    up.ImageGallery = qupload.ImageGallery;
                    up.ImageFooter = qupload.ImageFooter;
                    return up;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tbl_IndexET GetIndexHeaderSelect(string indexheader = null)
        {
            try
            {
                var qindex = db.Tbl_IndexET.Where(a => a.IDIndex > 0).SingleOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    Tbl_IndexET ivm = new Tbl_IndexET();
                    ivm.IDIndex = qindex.IDIndex;
                    ivm.Email = qindex.Email;
                    ivm.PhoneNumber = qindex.PhoneNumber;
                    ivm.Street = qindex.Street;
                    ivm.Number = qindex.Number;
                    ivm.PostCode = qindex.PostCode;
                    ivm.City = qindex.City;
                    ivm.Country = qindex.Country;
                    ivm.NameSite = qindex.NameSite;
                    ivm.EmbedLinkGoogleMap = qindex.EmbedLinkGoogleMap;
                    ivm.Description = qindex.Description;
                    return ivm;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<CategoryViewModel> GetCategoryLink(int id = 0)
        {
            try
            {
                var qcategory = db.Tbl_Kategorie.Where(a => a.ID > 0).ToList();
                if (qcategory == null)
                    return null;
                else
                {
                    IList<CategoryViewModel> lstcategory = new List<CategoryViewModel>();
                    foreach (var item in qcategory)
                    {
                        CategoryViewModel acategory = new CategoryViewModel();
                        acategory.ID = item.ID;
                        acategory.TitleCategory = item.TitleCategory;
                        acategory.Description = item.Description;
                        acategory.ActivePassive = item.ActivePassive;
                        acategory.FontName = item.FontName;
                        acategory.Link = /*item.TitleCategory + */ "@" + item.ID;
                        lstcategory.Add(acategory);
                    }
                    return lstcategory ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<SubCategoryViewModel> GetSubCategoryLink(int id = 0)
        {
            try
            {
                var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.ID > 0).ToList();
                if (qsubcategory == null)
                    return null;
                else
                {
                    IList<SubCategoryViewModel> lstsubcategory = new List<SubCategoryViewModel>();
                    foreach (var item in qsubcategory)
                    {
                        SubCategoryViewModel asubcategory = new SubCategoryViewModel();
                        asubcategory.ID = item.ID;
                        asubcategory.CategoryID = item.CategoryID;
                        asubcategory.TitleSubCategory = item.TitleSubCategory;
                        asubcategory.Description = item.Description;
                        asubcategory.ActivePassive = item.ActivePassive;
                        asubcategory.FontName = item.FontName;
                        asubcategory.Subtitle1 = item.Subtitle1;
                        asubcategory.Subtitle2 = item.Subtitle2;
                        asubcategory.Subtitle3 = item.Subtitle3;
                        asubcategory.Link = /*item.TitleSubCategory + */ "@" + item.ID;
                        lstsubcategory.Add(asubcategory);
                    }
                    return lstsubcategory ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetUserRole(string username)
        {
            try
            {
                // Select User: Email
                var quser = db.Tbl_Users.Where(a => a.UserName.Equals(username)).SingleOrDefault();
                var roleid = db.Tbl_Roles.Where(r => r.ID == quser.RoleID).SingleOrDefault();
                if (quser == null)
                    return null;
                if (roleid.Name == "Admin")
                    return roleid.Name; // Admin
                if (roleid.Name == "User")
                    return roleid.Name; // User
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tbl_Users GetUser(string id)
        {
            if (id == null)
                return null;
            var quser = db.Tbl_Users.Where(a => a.ID == id).SingleOrDefault();
            return quser ?? null;
        }
        public IList<Tbl_Album> GetAlbum(string album = null)
        {
            try
            {
                var qalbum = db.Tbl_Album.Where(a => a.ID > 0).ToList();
                if (qalbum == null)
                    return null;
                else
                {
                    IList<Tbl_Album> lstavm = new List<Tbl_Album>();
                    foreach (var item in qalbum)
                    {
                        Tbl_Album avm = new Tbl_Album();
                        avm.NamePic = item.NamePic;
                        avm.Image = item.Image;
                        avm.Date = item.Date;
                        lstavm.Add(avm);
                    }

                    return lstavm ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        ~HomeRep()
        {
            Dispose();
        }

        public void Dispose()
        {

        }
        public void Dispose(bool Dis)
        {
            if (Dis)
            {
                Dispose();
            }
        }
    }
}
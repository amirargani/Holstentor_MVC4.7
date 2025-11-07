using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Models.Domin;
namespace Restaurant.Models.Repository
{
    public class AdminRep : IDisposable
    {
        private DbHolstentorEntities db = null;
        public AdminRep()
        {
            db = new DbHolstentorEntities();
        }
        public List<Tbl_Users> GetUsers()
        {
            // All Users
            var qusers = db.Tbl_Users.ToList();
            return qusers;
        }
        public List<Tbl_Users> GetLastUsers()
        {
            DateTime days = DateTime.Today.AddDays(-30);
            var roleid = db.Tbl_Roles.Where(r => r.Name == "User").FirstOrDefault();
            var qlastusers = db.Tbl_Users.Where(a => a.RoleID == roleid.ID && a.Date >= days).ToList();
            return qlastusers;
        }
        public int GetUsersCount()
        {
            var roleid = db.Tbl_Roles.Where(r => r.Name == "User").FirstOrDefault();
            var qgetuserscount = db.Tbl_Users.Where(a => a.RoleID == roleid.ID).Count();
            return qgetuserscount;
        }

        public int GetUsersNachrichtCount()
        {
            // SELECT DISTINCT UserIdSend FROM Tbl_Nachricht;
            // SELECT UserIdSend   
            // FROM Tbl_Nachricht INTERSECT SELECT UserId FROM AspNetUserRoles;
            //var qgetusersnachrichtcount = (from a in db.Tbl_Nachricht select a.UserIdSend).Intersect(from u in db.UserRoles select u.UserId).Count();
            var qgetusersnachrichtcount = db.Tbl_Nachricht.OrderByDescending(a => a.ID).Count();
            return qgetusersnachrichtcount;
        }
        public int GetTotalUsersNachrichtTrueCount(string username)
        {
            var quser = db.Tbl_Users.Where(a => a.UserName == username).FirstOrDefault();
            var qgetusersnachrichttruecount = db.Tbl_Nachricht.Where(a => a.Confirm == true && a.UserIdRecive == quser.RoleID).Count();
            return qgetusersnachrichttruecount;
        }
        public int GetTotalUsersNachrichtFalseCount(string username)
        {
            var quser = db.Tbl_Users.Where(a => a.UserName == username).FirstOrDefault();
            var qgetusersnachrichtfalsecount = db.Tbl_Nachricht.Where(a => a.Confirm == false && a.UserIdRecive == quser.RoleID).Count();
            return qgetusersnachrichtfalsecount;
        }
        public Tbl_Index GetIndex(string index = null)
        {
            try
            {
                var qindex = db.Tbl_Index.Where(a => a.IDIndex > 0).FirstOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    Tbl_Index aindex = new Tbl_Index();
                    aindex.IDIndex = qindex.IDIndex;
                    aindex.Title = qindex.Title;
                    aindex.TypedText1 = qindex.TypedText1;
                    aindex.TypedText2 = qindex.TypedText2;
                    aindex.TypedText3 = qindex.TypedText3;
                    aindex.TypedText4 = qindex.TypedText4;
                    aindex.TypedText5 = qindex.TypedText5;
                    return aindex;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Tbl_Kategorie> GetCategory(string category = null)
        {
            try
            {
                var qcategory = db.Tbl_Kategorie.Where(a => a.ID > 0).ToList();
                if (qcategory == null)
                    return null;
                else
                {
                    IList<Tbl_Kategorie> lstcategory = new List<Tbl_Kategorie>();
                    foreach (var item in qcategory)
                    {
                        Tbl_Kategorie acategory = new Tbl_Kategorie();
                        acategory.ID = item.ID;
                        acategory.TitleCategory = item.TitleCategory;
                        acategory.Description = item.Description;
                        acategory.ActivePassive = item.ActivePassive;
                        acategory.FontName = item.FontName;
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
        public IList<Tbl_Users> GetUsersSend(string users = null)
        {
            try
            {
                DateTime days = DateTime.Today.AddDays(-30);
                var roleid = db.Tbl_Roles.Where(r => r.Name == "User").FirstOrDefault();
                var qusers = db.Tbl_Users.Where(a => a.RoleID == roleid.ID && a.Date >= days).ToList();
                if (qusers == null)
                    return null;
                else
                {
                    IList<Tbl_Users> lstausers = new List<Tbl_Users>();
                    foreach (var item in qusers)
                    {
                        if (item.EmailConfirmed == false)
                        {
                            Tbl_Users auser = new Tbl_Users();
                            auser.Name = item.Name;
                            auser.NameFamily = item.NameFamily;
                            auser.Email = item.Email;
                            auser.EmailConfirmed = item.EmailConfirmed;
                            auser.ID = item.ID;
                            auser.Date = item.Date;
                            auser.PhoneNumber = item.PhoneNumber;
                            lstausers.Add(auser);
                        }
                    }
                    return lstausers.ToList() ?? null; /*.Take(10)*/
                }
            }
            catch (Exception)
            {
                return null;
            }
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
                    IList<Tbl_Album> lstalbum = new List<Tbl_Album>();
                    foreach (var item in qalbum)
                    {
                        Tbl_Album aalbum = new Tbl_Album();
                        aalbum.Date = item.Date;
                        aalbum.NamePic = item.NamePic;
                        aalbum.Image = item.Image;
                        aalbum.ID = item.ID;
                        lstalbum.Add(aalbum);
                    }
                    return lstalbum ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Tbl_Users> GetUsersSendAll(string users = null)
        {
            try
            {
                var roleid = db.Tbl_Roles.Where(r => r.Name == "User").FirstOrDefault();
                var qusers = db.Tbl_Users.Where(a => a.RoleID == roleid.ID).ToList();
                if (qusers == null)
                    return null;
                else
                {
                    IList<Tbl_Users> lstausers = new List<Tbl_Users>();
                    foreach (var item in qusers)
                    {
                        Tbl_Users auser = new Tbl_Users();
                        auser.Name = item.Name;
                        auser.NameFamily = item.NameFamily;
                        auser.Email = item.Email;
                        auser.EmailConfirmed = item.EmailConfirmed;
                        auser.ID = item.ID;
                        auser.Date = item.Date;
                        auser.PhoneNumber = item.PhoneNumber;
                        lstausers.Add(auser);
                    }
                    return lstausers ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Tbl_Users> GetDetailsShow(string id)
        {
            var quasrid = db.Tbl_Users.Where(a => a.ID == id).ToList();
            return quasrid;
        }
        ~AdminRep()
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
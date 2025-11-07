using Restaurant.Models.Domin;
using Restaurant.Models.Repository;
using Restaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        private DbHolstentorEntities db = null;
        private ProfileRep auserrep = null;
        public readonly HomeRep _ivm = new HomeRep();

        public ProfileController()
        {
            db = new DbHolstentorEntities();
            auserrep = new ProfileRep();
        }
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
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return View();
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult MeinProfil(UserViewModel model) // MyProfile
        {
            if (Session["EmailUser"] != null )
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                {
                    var quser = db.Tbl_Users.Where(a => a.UserName == username).FirstOrDefault();
                    quser.Name = model.Name;
                    quser.NameFamily = model.NameFamily;
                    quser.PhoneNumber = model.PhoneNumber;
                    db.Tbl_Users.Attach(quser);
                    db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");

        }
        [HttpPost]
        public ActionResult MeinProfilBearbeiten(UserViewModel model) // EditMyProfile
        {
            if (Session["EmailUser"] != null)
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                {
                    var quser = db.Tbl_Users.Where(a => a.UserName == username).FirstOrDefault();
                    quser.Name = model.Name;
                    quser.NameFamily = model.NameFamily;
                    quser.PhoneNumber = model.PhoneNumber;
                    db.Tbl_Users.Attach(quser);
                    db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
        }
        public ActionResult Nachricht() // Message
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
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return View();
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult NachrichtGesendet(MessageViewModel model) // MessageSend
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
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                {
                    try
                    {
                        var qadmin = db.Tbl_Roles.Where(a => a.Name == "Admin").FirstOrDefault();
                        //var quserrecive = db.Tbl_Users.Where(a => a.RoleID == qadmin.ID).FirstOrDefault();
                        var qusersend = db.Tbl_Users.Where(a => a.UserName == model.UserIdSend).FirstOrDefault();
                        Tbl_Nachricht msg = new Tbl_Nachricht();
                        msg.Confirm = false;
                        msg.Date = DateTime.Now;
                        msg.Text = model.Text;
                        msg.TextAdmin = "";
                        msg.UserIdRecive = qadmin.ID;
                        msg.UserIdSend = qusersend.ID;
                        db.Tbl_Nachricht.Add(msg);
                        db.SaveChanges();
                        return RedirectToAction(nameof(Nachricht));
                    }
                    catch (Exception)
                    {

                        return RedirectToAction(nameof(Nachricht));
                    }
                }
            }
            return RedirectToAction(nameof(AccountController.Einloggen), "Account");
        }
        [HttpGet]
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
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return View();
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
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
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
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
        }
    }
}
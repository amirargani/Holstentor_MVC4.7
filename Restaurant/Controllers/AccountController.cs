using Restaurant.Models.Domin;
using Restaurant.Models.Plugin;
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
    public class AccountController : Controller
    {
        private DbHolstentorEntities db = null;
        public readonly HomeRep _ivm = new HomeRep();
        Data RunDataClass = new Data();
        public string userid, emailid = null;

        public AccountController()
        {
            db = new DbHolstentorEntities();
        }
        // GET: Account
        [HttpGet]
        public ActionResult Anmelden()
        {
            if (Session["EmailUser"] == null)
            {
                return View();
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult Anmelden(RegisterViewModel user)
        {
            var quserfind = db.Tbl_Users.Where(a => a.Email.Equals(user.Email)).SingleOrDefault();
            if (quserfind != null)
                return RedirectToAction(nameof(AccountController.Anmelden), "Account");
            if (ModelState.IsValid)
            {
                Tbl_Users u = new Tbl_Users();
                userid = RunDataClass.GetCode(); ;
                u.ID = userid;
                u.Date = DateTime.Now;
                u.Email = user.Email;
                u.EmailConfirmed = false;
                u.Name = null;
                u.NameFamily = null;
                u.NormalizedEmail = user.Email.ToUpper();
                u.NormalizedUserName = user.Email.ToUpper();
                //u.PasswordHash = Crypto.Hash(user.Password, "MD5");
                u.PasswordHash = Crypto.SHA256(user.Password);
                u.PhoneNumber = null;
                u.PhoneNumberConfirmed = false;
                u.UserName = user.Email;
                u.RoleID = null;
                db.Tbl_Users.Add(u);
                if (Convert.ToBoolean(db.SaveChanges() > 0))
                {
                    Tbl_EmailConfirmed ec = new Tbl_EmailConfirmed();
                    emailid = RunDataClass.GetCode() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                    ec.ID = emailid;
                    ec.Date = DateTime.Now;
                    ec.UserID = userid;
                    ec.Confirmed = false;
                    db.Tbl_EmailConfirmed.Add(ec);
                    db.SaveChanges();
                    Email email = new Email(); // http://localhost:49908/Account/ConfirmEmail?Code=
                    email.SendEmail(user.Email, "Bitte bestätigen Sie Ihren Account", "<b>Benutzer: " + user.Email + "</b><br />Bitte bestätigen Sie Ihren Account, indem Sie auf diesen Link klicken: <a href='http://www.domain.de/Account/ConfirmEmail?Code=" + emailid + "'>Link</a>", "mail@");
                    return RedirectToAction("Einloggen", "Account");
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

            }
            else
                return View(user);
        }
        [HttpGet]
        public ActionResult ConfirmEmail(string code = null)
        {
            if (code != null)
            {
                var qcode = db.Tbl_EmailConfirmed.Where(a => a.ID.Equals(code)).SingleOrDefault();
                if (qcode == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                var quser = db.Tbl_Users.Where(a => a.ID == qcode.UserID).SingleOrDefault();
                if (quser == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                var roleid = db.Tbl_Roles.Where(r => r.Name == "User").SingleOrDefault();
                if (qcode.Date.AddDays(7) < DateTime.Today)
                {
                    if (quser.EmailConfirmed == false)
                    {
                        Tbl_EmailConfirmed ec = new Tbl_EmailConfirmed();
                        emailid = RunDataClass.GetCode() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                        ec.ID = emailid;
                        ec.Date = DateTime.Now;
                        ec.UserID = qcode.UserID;
                        ec.Confirmed = false;
                        db.Tbl_EmailConfirmed.Remove(qcode);
                        db.Tbl_EmailConfirmed.Add(ec);
                        db.SaveChanges();
                        Email email = new Email(); // http://localhost:49908/Account/ConfirmEmail?Code=
                        email.SendEmail(quser.Email, "Bitte bestätigen Sie Ihren Account", "<b>Benutzer: " + quser.Email + "</b><br />Bitte bestätigen Sie Ihren Account, indem Sie auf diesen Link klicken: <a href='http://www.domain.de/Account/ConfirmEmail?Code=" + emailid + "'>Link</a>", "mail@");
                        return RedirectToAction(nameof(AccountController.ForgotPasswordConfirmation), "Account");
                    }
                }
                if (qcode.Confirmed == false && quser.EmailConfirmed == false)
                {
                    qcode.Confirmed = true;
                    quser.RoleID = roleid.ID;
                    quser.EmailConfirmed = true;
                    db.Tbl_EmailConfirmed.Attach(qcode);
                    db.Entry(qcode).State = System.Data.Entity.EntityState.Modified;
                    db.Tbl_Users.Attach(quser);
                    db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(AccountController.Einloggen), "Account");
                }
                if (qcode.Confirmed == true && quser.EmailConfirmed == true)
                {
                    return RedirectToAction(nameof(AccountController.Einloggen), "Account");
                }
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
            return RedirectToAction(nameof(AccountController.Einloggen), "Account");
        }
        [HttpGet]
        public ActionResult Einloggen()
        {
            if (Session["EmailUser"] == null)
            {
                return View();
            }
            else
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public ActionResult Einloggen(LoginViewModel user)
        {
            var quserfind = db.Tbl_Users.Where(a => a.Email.Equals(user.Email)).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (quserfind.Email == user.Email)
                {
                    if (quserfind.PasswordHash == Crypto.SHA256(user.Password))
                    {
                        if (quserfind.EmailConfirmed == true)
                        {
                            // Select User: Email
                            Session["EmailUser"] = quserfind.Email;
                            string username = Session["EmailUser"].ToString();
                            if (_ivm.GetUserRole(username) == "Admin")
                                return RedirectToAction("Index", "Admin");
                            if (_ivm.GetUserRole(username) == "User")
                                return RedirectToAction("Index", "Profile");
                        }
                        else
                            return RedirectToAction(nameof(AccountController.Einloggen), "Account");
                    }
                }
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Ausloggen()
        {
            try
            {
                if (Session["EmailUser"] != null)
                {
                    Session["EmailUser"] = null;
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch
            {
                // Select User: Email
                string username = Session["EmailUser"].ToString();
                if (_ivm.GetUserRole(username) == "Admin")
                    return RedirectToAction("Index", "Admin");
                if (_ivm.GetUserRole(username) == "User")
                    return RedirectToAction("Index", "Profile");
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
        }
        [HttpPost]
        public JsonResult EamilValid(string Email)
        {
            try
            {
                var qemail = db.Tbl_Users.Where(a => a.Email == Email).SingleOrDefault();
                if (qemail == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Tbl_Users.Where(a => a.Email == model.Email).SingleOrDefault();
                var qcode = db.Tbl_RestPassword.Where(a => a.UserID == user.ID).ToList();
                foreach (var item in qcode.Where(q => q.UserID == user.ID))
                {
                    db.Tbl_RestPassword.Remove(item);
                    db.SaveChanges();
                }
                if (user == null)
                    return View(model);
                else
                {
                    string codernd = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + RunDataClass.GetCode();
                    Tbl_RestPassword re = new Tbl_RestPassword();
                    re.ID = codernd;
                    re.Date = DateTime.Now;
                    re.UserID = user.ID;
                    re.Password = "";
                    re.ConfirmPassword = "";
                    db.Tbl_RestPassword.Add(re);
                    db.SaveChanges();
                    Email email = new Email(); // http://localhost:49908/Account/ResetPassword?Code=
                    email.SendEmail(user.Email, "Passwort zurücksetzen", "<b>Benutzer: " + user.Email + "</b><br />Bitte setzen Sie Ihr Passwort zurück, indem Sie auf diesen Link klicken: <a href='http://www.domain.de/Account/ResetPassword?Code=" + codernd + "'>Link</a>", "mail@");
                    return RedirectToAction(nameof(AccountController.ForgotPasswordConfirmation), "Account");
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult EmailInvalidPass(string Email)
        {
            try
            {
                var qemail = db.Tbl_Users.Where(a => a.Email == Email).SingleOrDefault();
                if (qemail == null)
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ResetPasswordEmail(string code = null) // ResetPasswordEmail
        {
            //if (code != null)
            //{
            //    var qcode = db.Tbl_RestPassword.Where(a => a.ID.Equals(code)).SingleOrDefault();
            //    var quser = db.Tbl_Users.Where(a => a.ID == qcode.UserID).SingleOrDefault();
            //    if (quser == null)
            //        return RedirectToAction(nameof(HomeController.Error), "Home");
            //    if (qcode == null)
            //        return RedirectToAction(nameof(HomeController.Error), "Home");
            //    string codernd = RunDataClass.GetPassword();
            //    quser.PasswordHash = Crypto.SHA256(codernd);
            //    db.Tbl_Users.Attach(quser);
            //    db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
            //    db.Tbl_RestPassword.Remove(qcode);
            //    db.SaveChanges();
            //    Email email = new Email();
            //    email.SendEmail(quser.Email, "Passwort änderen", "Sehr geehrter Benutzer, Ihr Passwort wurde erfolgreich geändert.<br /><b>Benutzer: " + quser.Email + "</b><br /><b>Neues Passwort: " + codernd + "</b>");
            //    return RedirectToAction(nameof(AccountController.Einloggen), "Account");

            //}
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        [HttpGet]
        public ActionResult ResetPassword(string code = null)
        {
            if (code != null)
            {
                var qcode = db.Tbl_RestPassword.Where(a => a.ID.Equals(code)).SingleOrDefault();
                if (qcode == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                var quser = db.Tbl_Users.Where(a => a.ID == qcode.UserID).SingleOrDefault();
                if (quser == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                ViewData["Code"] = qcode.ID;
                return View();

            }
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        [HttpPost]
        public ActionResult ResetPasswordConfirmation(Tbl_RestPassword model, string code = null)
        {
            if (code != null)
            {
                var qcode = db.Tbl_RestPassword.Where(a => a.ID.Equals(code)).SingleOrDefault();
                if (qcode == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                var quser = db.Tbl_Users.Where(a => a.ID == qcode.UserID).SingleOrDefault();
                if (quser == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                quser.PasswordHash = Crypto.SHA256(model.Password);
                db.Tbl_Users.Attach(quser);
                db.Entry(quser).State = System.Data.Entity.EntityState.Modified;
                db.Tbl_RestPassword.Remove(qcode);
                db.SaveChanges();
                return RedirectToAction(nameof(AccountController.Einloggen), "Account");
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }
    }
}
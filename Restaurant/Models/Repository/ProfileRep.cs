using Restaurant.Models.Domin;
using Restaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.Repository
{
    public class ProfileRep : IDisposable
    {
        private DbHolstentorEntities db = null;
        public ProfileRep()
        {
            db = new DbHolstentorEntities();
        }
        public bool InsertUser(string username)
        {
            try
            {
                var quser = db.Tbl_Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser.Name != null && quser.Name != "" &&
                    quser.NameFamily != null && quser.NameFamily != "" &&
                    quser.PhoneNumber != null && quser.PhoneNumber != "")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public Tbl_Users GetUser(string username)
        {
            try
            {
                // Select User: Email
                var quser = db.Tbl_Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser == null)
                    return null;
                else
                {
                    Tbl_Users auser = new Tbl_Users();
                    auser.Email = quser.Email;
                    auser.UserName = quser.UserName;
                    auser.Name = quser.Name;
                    auser.NameFamily = quser.NameFamily;
                    auser.PhoneNumber = quser.PhoneNumber;
                    return auser;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<MessageViewModel> GetMsgSend(string username)
        {
            try
            {
                var quser = db.Tbl_Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser == null)
                    return null;
                else
                {
                    var qmsgsend = db.Tbl_Nachricht.Where(a => a.UserIdSend.Equals(quser.ID)).ToList();
                    if (qmsgsend == null)
                        return null;

                    IList<MessageViewModel> lstmsg = new List<MessageViewModel>();
                    foreach (var item in qmsgsend)
                    {
                        MessageViewModel msg = new MessageViewModel();
                        msg.Confirm = item.Confirm;
                        msg.Date = item.Date;
                        msg.ID = item.ID;
                        msg.Text = item.Text;
                        msg.TextAdmin = item.TextAdmin;
                        msg.DateUpdate = item.DateUpdate;
                        msg.UserIdRecive = item.UserIdRecive;
                        msg.UserIdSend = item.UserIdSend;
                        msg.UsernameSend = quser.NameFamily;
                        lstmsg.Add(msg);
                    }

                    return lstmsg ?? null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        ~ProfileRep()
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
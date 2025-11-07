using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Remote("EmailInvalidPass", "Account", HttpMethod = "Post", ErrorMessage = Message.EmailInvalidLogin)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Geben Sie die E-Mail richtig ein.")]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
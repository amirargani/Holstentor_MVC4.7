using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Remote("EamilValid", "Account", HttpMethod = "Post", ErrorMessage = Message.EmailInvalid)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Geben Sie die E-Mail richtig ein.")]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Buchstaben haben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Passwort Wiederholung")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Das Passwort und das Bestätigung Passwort sind nicht gleich.")]
        public string ConfirmPassword { get; set; }
    }
}
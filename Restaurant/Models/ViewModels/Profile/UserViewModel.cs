using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{12})$", ErrorMessage = "Das Feld {0} ist keine gültige Telefonnummer.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        [StringLength(50, ErrorMessage = "Der {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Vorname")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Der {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Familienname")]
        public string NameFamily { get; set; }
    }
}
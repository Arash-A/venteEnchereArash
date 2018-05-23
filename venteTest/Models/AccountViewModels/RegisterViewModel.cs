using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        //[Required]
        //[Display(Name = "Nom")]
        //public string Nom { get; set; }

        //[Required]
        //[Display(Name = "Prenom")]
        //public string Prenom { get; set; }

        [Required, Display(Name = "Courriel", ResourceType = typeof(StringsAccount))]
        [EmailAddress]
        //[Display(Name = "Courriel")]
        public string Email { get; set; }

        [Required, Display(Name = "Mdp", ResourceType = typeof(StringsAccount))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmerMdp", ResourceType = typeof(StringsAccount))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

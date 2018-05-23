using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AccountViewModels
{
    public class LoginViewModel
    {

        [Required, Display(Name = "Courriel", ResourceType = typeof(StringsAccount))]
        [EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Mdp", ResourceType = typeof(StringsAccount))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "SeSouvenir", ResourceType = typeof(StringsAccount))]
        public bool RememberMe { get; set; }
    }
}

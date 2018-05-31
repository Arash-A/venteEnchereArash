using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required, Display(Name = "Courriel", ResourceType = typeof(StringsAccount))]
        [EmailAddress]
        public string Email { get; set; }
    }
}

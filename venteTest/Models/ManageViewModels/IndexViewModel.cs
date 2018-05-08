using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.ManageViewModels
{
    public class IndexViewModel
    {


        public string Username { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}

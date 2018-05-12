using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using venteTest.Resources.Views;


namespace venteTest.Models.ManageViewModels
{
    public class IndexViewModel
    {

        public string Username { get; set; }

        [Required]
        [Display(Name = "Nom", ResourceType = typeof(SharedStrings))]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prenom", ResourceType = typeof(SharedStrings))]
        public string Prenom { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telephone", ResourceType = typeof(SharedStrings))]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Required]
        [Display(Name = "Civilite")]
        public string Civilite { get; set; }

        [Required]
        [Display(Name = "Langue")]
        public string Langue { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

    }
}

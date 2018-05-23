using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using venteTest.Resources.Views;
using venteTest.Resources.Models;


namespace venteTest.Models.ManageViewModels
{
    public class IndexViewModel
    {

        public string Username { get; set; }

        [Display(Name = "UsagerNom", ResourceType = typeof(StringsManage))]
        public string Nom { get; set; }

        [Display(Name = "UsagerPrenom", ResourceType = typeof(StringsManage))]
        public string Prenom { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required, Display(Name = "UsagerCourriel", ResourceType = typeof(StringsManage))]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "UsagerTelephone", ResourceType = typeof(StringsManage))]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Display(Name = "UsagerCivilite", ResourceType = typeof(StringsManage))]
        public string Civilite { get; set; }

        [Display(Name = "UsagerLangue", ResourceType = typeof(StringsManage))]
        public string Langue { get; set; }

        [Display(Name = "UsagerAdresse", ResourceType = typeof(StringsManage))]
        public string Adresse { get; set; }

    }
}

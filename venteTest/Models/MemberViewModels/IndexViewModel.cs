using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Models.AdminViewModels;
using venteTest.Resources.Models;

namespace venteTest.Models.MemberViewModels
{
    public class IndexViewModel {

        public string Id { get; set; }

        [Display(Name = "UsagerCourriel", ResourceType = typeof(StringsManage))]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "UsagerNom", ResourceType = typeof(StringsManage))]
        public string Nom { get; set; }

        [Display(Name = "UsagerPrenom", ResourceType = typeof(StringsManage))]
        public string Prenom { get; set; }

        [Display(Name = "UsagerCivilite", ResourceType = typeof(StringsManage))]
        public string Civilite { get; set; }

        public double Cote { get; set; }

        public double NbEvaluation { get; set; }

        //public SendCotesReportViewModel SendCotesReportViewModel { get; set; }

        public virtual ICollection<Objet> Objets {
            get;
            set;
        }
    }
}

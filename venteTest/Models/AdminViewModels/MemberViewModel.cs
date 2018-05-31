using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AdminViewModels
{
    public class MemberViewModel {

        [Display(Name = "Courriel", ResourceType = typeof(StringsAdmin))]
        public string Email {
            get;
            set;
        }
        [Display(Name = "CourrielConfirme", ResourceType = typeof(StringsAdmin))]
        public bool EmailConfirmed {
            get;
            set;
        }
        [Display(Name = "Telephone", ResourceType = typeof(StringsAdmin))]
        public string PhoneNumber {
            get;
            set;
        }

        [Display(Name = "TelephoneConfirme", ResourceType = typeof(StringsAdmin))]
        public bool PhoneNumberConfirmed {
            get;
            set;
        }

        [Display(Name = "Civilite", ResourceType = typeof(StringsAdmin))]
        public string Civilite {
                get;
                set;
            }

        [Display(Name = "Nom", ResourceType = typeof(StringsAdmin))]
        public string Nom {
            get;
            set;
        }

        [Display(Name = "Prenom", ResourceType = typeof(StringsAdmin))]
        public string Prenom {
            get;
            set;
        }

        [Display(Name = "Langue", ResourceType = typeof(StringsAdmin))]
        public string Langue {
            get;
            set;
        }
        [Display(Name = "Telephone", ResourceType = typeof(StringsAdmin))]
        public string Telephone {
            get;
            set;
        }

        [Display(Name = "Adresse", ResourceType = typeof(StringsAdmin))]
        public string Adresse {
            get;
            set;
        }

        [Display(Name = "DateInscription", ResourceType = typeof(StringsAdmin))]
        public DateTime? DateInscription {
            get;
            set;
        }


        // N.B. 1 vendeur peut être un miseur en même temps
        // 1 user(vendeur) a 1 ou * objects à vendre

        public virtual ICollection<Objet> Objets {
            get;
            set;
        }

        // 1 user(miseur) a 1 ou * encheres(mises supérieur) sur 1 objets

        public virtual ICollection<Enchere> Encheres {
            get;
            set;
        }
    }
}

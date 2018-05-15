using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class MemberViewModel { 

        public string Email {
            get;
            set;
        }
        public bool EmailConfirmed {
            get;
            set;
        }
        public string PhoneNumber {
            get;
            set;
        }
        public bool PhoneNumberConfirmed {
            get;
            set;
        }
        public string Civilite {
                get;
                set;
            }

        public string Nom {
            get;
            set;
        }

        public string Prenom {
            get;
            set;
        }

        public string Langue {
            get;
            set;
        }
        public string Telephone {
            get;
            set;
        }

        public string Adresse {
            get;
            set;
        }

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

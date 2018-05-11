using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace venteTest.Models {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {


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
        // N.B.: Telephone est deja nativement ds AspNetUsers

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

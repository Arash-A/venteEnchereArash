using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace venteTest.Models {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {


        public  string Civilite {
            get;
            set;
        }

        public  string Nom {
            get;
            set;
        }

        public  string Prenom {
            get;
            set;
        }

        public  string Langue {
            get;
            set;
        }

        public  int Telephone {
            get;
            set;
        }

        public  string Adresse {
            get;
            set;
        }

        public DateTime? DateInscription {
            get;
            set;
            //get {
            //    return DateInscription;
            //}
            //set { DateInscription = DateTime.Now; }
        }

        public virtual int ObjetId {
            get;
            set;
        }

        public virtual ICollection<Objet> Objets {
            get;
            set;
        }

        public virtual ICollection<Enchere> Encheres {
            get;
            set;
        }
        public virtual ICollection<Evaluation> Evaluations {
            get;
            set;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Categorie
    {

        public int CategorieId {
            get;
            set;
        }

        public String Nom {
            get;
            set;
        }

        public String Description { get; set; }



        public virtual ICollection<Objet> Objets {
            get;
            set;
        }
    }
}
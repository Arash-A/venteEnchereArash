using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using venteTest.Resources.Models;

namespace venteTest.Models
{
    public class Categorie
    {

        [Display(Name = "CategorieId", ResourceType = typeof(StringsShared))]
        public int CategorieId {
            get;
            set;
        }

        [Display(Name = "Nom", ResourceType = typeof(StringsShared))]
        public String Nom {
            get;
            set;
        }

        [Display(Name = "Description", ResourceType = typeof(StringsShared))]
        public String Description { get; set; }



        public virtual ICollection<Objet> Objets {
            get;
            set;
        }
    }
}
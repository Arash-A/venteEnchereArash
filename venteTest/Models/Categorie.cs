using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Categorie
    {
        public int CategorieId { get; set; }

        [RegularExpression("[a-z A-Z]{30}", ErrorMessage = "Please enter name")]
        public String Nom { get; set; }

        public String Description{ get; set; }

        public virtual ICollection<Objet> Objets { get; set; }
    }
}
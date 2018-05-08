using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Enchere
    {
        public int EnchereID { get; set; }

        [Required]
        public decimal NiveauEnchere{ get; set; }
 
        //propriéte de Navigation
        public int MembreID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        //propriéte de Navigation
        public int ObjetID { get; set; }
        public virtual Objet Objet{ get; set; }

    }
}
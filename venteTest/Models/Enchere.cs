using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using venteTest.Resources.Models;

namespace venteTest.Models
{
    public class Enchere
    {

        // Une enchère est une « offre d’un prix supérieur à la mise à prix, ou au prix qu’un autre a déjà offert, en parlant des choses qui se vendent ou s’afferment au plus offrant. »
        [Display(Name = "EnchereId", ResourceType = typeof(StringsShared))]
        public int EnchereId { get; set; }

        [Display(Name = "Niveau", ResourceType = typeof(StringsShared))]
        public double Niveau { get; set; } // mise à prix... ex: 40

        [Display(Name = "MiseMax", ResourceType = typeof(StringsShared))]
        public double MiseMax { get; set; } // mise max offerte par un miseur.. ex: 50

        [Display(Name = "Miseur", ResourceType = typeof(StringsShared))]
        public Miseur Miseur { get; set; }

        [Display(Name = "ObjetId", ResourceType = typeof(StringsShared))]
        public int ObjetId { get; set; }

        [Display(Name = "Objet", ResourceType = typeof(StringsShared))]
        public Objet Objet { get; set; }

    }
}
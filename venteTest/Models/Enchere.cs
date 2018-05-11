using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Enchere
    {

        // Une enchère est une « offre d’un prix supérieur à la mise à prix, ou au prix qu’un autre a déjà offert, en parlant des choses qui se vendent ou s’afferment au plus offrant. »
        public int EnchereId { get; set; }

        public double Niveau { get; set; } // mise à prix

        public Miseur Miseur { get; set; }

        public int ObjetId { get; set; }

        public Objet Objet { get; set; }

    }
}
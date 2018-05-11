using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Fichier
    {
        public int FichierId { get; set; }

        [Required]
        public string NomOriginal { get; set; }

        public string NomLocale { get; set; }

        public DateTime verseLe { get; private set; }

        public string Remarques { get; set; }

        public int ObjetId { get; set; }

        public Objet Objet { get; set; }
    }
}
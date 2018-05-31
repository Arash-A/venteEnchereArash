using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using venteTest.Resources.Models;

namespace venteTest.Models
{
    public class Fichier
    {
        [Display(Name = "FichierId", ResourceType = typeof(StringsShared))]
        public int FichierId { get; set; }

        [Required, Display(Name = "NomOriginal", ResourceType = typeof(StringsShared))]
        public string NomOriginal { get; set; }

        [Display(Name = "NomLocal", ResourceType = typeof(StringsShared))]
        public string NomLocale { get; set; }

        [Display(Name = "DateVerse", ResourceType = typeof(StringsShared))]
        public DateTime verseLe { get;  set; }

        [Display(Name = "Remarques", ResourceType = typeof(StringsShared))]
        public string Remarques { get; set; }

        [Display(Name = "ObjetId", ResourceType = typeof(StringsShared))]
        public int ObjetId { get; set; }

        [Display(Name = "Objet", ResourceType = typeof(StringsShared))]
        public Objet Objet { get; set; }
    }
}
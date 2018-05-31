using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using venteTest.Resources.Models;

namespace venteTest.Models
{
    public abstract class Evaluation
    {
        [Display(Name = "EvaluationId", ResourceType = typeof(StringsShared))]
        public int EvaluationID { get; set; }

        [Display(Name = "Numero", ResourceType = typeof(StringsShared))]
        public String Numero { get; set; }


        [Required, Display(Name = "DateEvaluation", ResourceType = typeof(StringsShared))]
        public DateTime DateEvaluation { get; set; }

        [Display(Name = "Cote", ResourceType = typeof(StringsShared))]
        [Range(-3, 3, ErrorMessage = "Cote  Must be between -3 to 3")]
        public int Cote { get; set; }

        [Display(Name = "Commentaire", ResourceType = typeof(StringsShared))]
        [MaxLength(10000, ErrorMessage = "1000 caracteres max")]
        public String Commentaire { get; set; }

        //public int ObjetId { get; set; }
        [Display(Name = "Objet", ResourceType = typeof(StringsShared))]
        public Objet Objet { get; set; }
    }
}
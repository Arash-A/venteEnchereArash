using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.ObjetViewModel
{
    public class EvaluationViewModel
    {
        [Range(-3, 3, ErrorMessage = "Cote  Must be between -3 to 3")]
        public int Cote { get; set; }

        public DateTime DateEvaluation { get; set; }

        [MaxLength(10000, ErrorMessage = "1000 caracteres max")]
        public String Commentaire { get; set; }

        public Objet Objet { get; set; }
        public int ObjetId { get; set; }

        public ApplicationUser leUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public abstract class Evaluation
    {
        public int EvaluationID { get; set; }


        public String Numero { get; set; }


        [Required]
        public DateTime DateEvaluation { get; set; }


        [Range(-3, 3, ErrorMessage = "Cote  Must be between -3 to 3")]
        public int Cote { get; set; }


        [MaxLength(10000, ErrorMessage = "1000 caracteres max")]
        public String Commentaire { get; set; }

        //public int ObjetId { get; set; }
        public Objet Objet { get; set; }
    }
}
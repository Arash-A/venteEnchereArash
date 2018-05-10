using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Evaluation
    {
        public int EvaluationID { get; set; }


        [RegularExpression("[a-z A-Z 0-9 ]{10}", ErrorMessage = "Please enter name")]
        public String Numero{ get; set; }


        [Required]
        public DateTime DateEvaluation{ get; set; }


        [Range(-3, 3, ErrorMessage = "Cote  Must be between -3 to 3")]
        public int Cote { get; set; }


        [RegularExpression("[a-z A-Z]{1000}", ErrorMessage = "Please enter name")]
        [MaxLength(10000, ErrorMessage = "1000 caracteres max")]
        public String Commentaire { get; set; }

        public virtual Enchere Enchere { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
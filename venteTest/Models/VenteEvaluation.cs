using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models
{
    public class VenteEvaluation : Evaluation {

        [Display(Name = "Acheteur", ResourceType = typeof(StringsShared))]
        public Miseur Acheteur { get; set; }
    }
}

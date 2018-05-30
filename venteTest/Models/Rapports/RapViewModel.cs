using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.Rapports
{
    public class RapViewModel
    {
        [Display(Name = "Rapport #1")]
        public bool rap1 { get; set; }

        [Display(Name = "Rapport #2")]
        public bool rap2 { get; set; }

        [Display(Name = "Rapport #3")]
        public bool rap3 { get; set; }

        [Display(Name = "Rapport #4")]
        public bool rap4 { get; set; }

        [Display(Name = "Rapport #5")]
        public bool rap5 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.Rapports
{
    public class RapViewModel
    {
        [Display(Name = "Rapport1", ResourceType = typeof(StringsRapports))]
        public bool rap1 { get; set; }

        [Display(Name = "Rapport2", ResourceType = typeof(StringsRapports))]
        public bool rap2 { get; set; }

        [Display(Name = "Rapport3", ResourceType = typeof(StringsRapports))]
        public bool rap3 { get; set; }

        [Display(Name = "Rapport4", ResourceType = typeof(StringsRapports))]
        public bool rap4 { get; set; }

        [Display(Name = "Rapport5", ResourceType = typeof(StringsRapports))]
        public bool rap5 { get; set; }
    }
}

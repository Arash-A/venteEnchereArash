using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AdminViewModels
{
    public class ConfigurationAdminViewModel {
        [Display(Name = "IdMise", ResourceType = typeof(StringsAdmin))]
        public int ConfigurationAdminId { get; set; }

        [Display(Name = "FacteurComm", ResourceType = typeof(StringsAdmin))]
        public decimal TauxGlobalComissionAuVendeur { get; set; }

        [Display(Name = "MisePas", ResourceType = typeof(StringsAdmin))]
        public decimal PasGlobalEnchere { get; set; }
    }
}

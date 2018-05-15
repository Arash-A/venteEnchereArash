using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class ConfigurationAdminViewModel {
        [Display(Name = "Bid Config Id")]
        public int ConfigurationAdminId { get; set; }

        [Display(Name = "Commission Factor")]
        public decimal TauxGlobalComissionAuVendeur { get; set; }

        [Display(Name = "Bid Step")]
        public decimal PasGlobalEnchere { get; set; }
    }
}

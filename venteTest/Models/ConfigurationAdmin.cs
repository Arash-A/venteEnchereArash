using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models
{
    public class ConfigurationAdmin
    {
        public int ConfigurationAdminId { get; set; }
        public decimal TauxGlobalComissionAuVendeur { get; set; }
        public decimal PasGlobalEnchere { get; set; }
        public Administrateur AdminDesVentes { get; set; }
    }
}

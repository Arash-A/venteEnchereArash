using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models
{
    public class Administrateur : Vendeur { // L'admin peut vendre et miser aussi
        public virtual ICollection<ConfigurationAdmin> ConfigurationAdmins { get; set; }
    }
}

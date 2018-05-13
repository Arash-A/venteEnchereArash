using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models
{
    public class Vendeur : Miseur { // 1 vendeur peut miser aussi.

        // 1 user(vendeur) possède 1 ou * objects à vendre
        //public virtual ICollection<Objet> Objets {
        //    get;
        //    set;
        //}
        //public virtual ICollection<VenteEvaluation> VenteEvaluation {
        //    get;
        //    set;
        //}
    }
}

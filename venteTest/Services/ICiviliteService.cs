using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Services
{
    interface ICiviliteService
    {
        IEnumerable<Civilite> List();
    }
    public class CiviliteService : ICiviliteService {
        public IEnumerable<Civilite> List() {
            return new List<Civilite>
            {
                new Civilite { Abbreviation = CiviliteAbbreviation.M.ToString(), Name = CiviliteName.Monsieur.ToString()},
                new Civilite { Abbreviation = CiviliteAbbreviation.Mme.ToString(), Name = CiviliteName.Madame.ToString()}
            };
        }
    }
   public class Civilite {
        public String Abbreviation { get; set; }
        public String Name { get; set; }
    }

    public enum CiviliteName {
        Monsieur,
        Madame,
    }
    public enum CiviliteAbbreviation {
        M,
        Mme,
    }

}

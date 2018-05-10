using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Services
{
    interface ILanguageService
    {
        IEnumerable<Language> List();
    }
    public class LanguageService : ILanguageService {
        public IEnumerable<Language> List() {
            return new List<Language>
            {
                new Language { Abbreviation = LanguageAbbreviation.en.ToString(), Name = LanguageName.EN.ToString() },
                new Language { Abbreviation = LanguageAbbreviation.fr.ToString(), Name = LanguageName.FR.ToString() }
            };
        }
    }
    public class Language {
        public String Abbreviation { get; set; }
        public String Name { get; set; }
    }

    public enum LanguageName {
        FR,
        EN
    }
    public enum LanguageAbbreviation {
        fr,
        en
    }
}

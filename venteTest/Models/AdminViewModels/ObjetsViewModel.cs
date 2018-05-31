using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AdminViewModels {
    public class ObjetsViewModel {
        [StringLength(75, MinimumLength = 10, ErrorMessage = "Please enter object name between 10 and 75 characters")]
        [Display(Name = "ObjetNom", ResourceType = typeof(StringsAdmin))]
        public string Nom { get; set; }

        [StringLength(300, MinimumLength = 10, ErrorMessage = "Please enter object description between 10 and 300 characters")]
        [Display(Name = "ObjetDescription", ResourceType = typeof(StringsAdmin))]
        public string Description { get; set; }

        [Display(Name = "ObjetMiseDepart", ResourceType = typeof(StringsAdmin))]
        [Range(10, double.MaxValue, ErrorMessage = "The starting bid value must be greater than 10.")]
        public double PrixDepart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "ObjetDateDepart", ResourceType = typeof(StringsAdmin))]
        public DateTime DateInscription { get; set; }
        //par défault on va mettre DateInscription à DateTime.Now à la création de l'annonce


        [DataType(DataType.Date)]
        [Display(Name = "ObjetDateFin", ResourceType = typeof(StringsAdmin))]
        public DateTime DateLimite { get; set; } // fixé par le Vendeur à l'ajout dans ce ViewModel

        [Display(Name = "ObjetDuree", ResourceType = typeof(StringsAdmin))]
        public String DureeMiseVente {
            get {
                TimeSpan diff1 = DateTime.Now.Subtract(DateInscription);
                String outpp = diff1.ToString("%d");
                return (outpp);
            }
        }

        public Status Status { get; set; }

        [Display(Name = "ObjetImage", ResourceType = typeof(StringsAdmin))]
        [StringLength(1024, ErrorMessage = "Your image path is too long because exceeding 1024 characters. Try again!")]
        public string imageUrl { get; set; }

        [Display(Name = "Categorie", ResourceType = typeof(StringsAdmin))]
        public Categorie Categorie { get; set; }

        [Required, Display(Name = "Vendeur", ResourceType = typeof(StringsAdmin))]
        public Vendeur Vendeur { get; set; }

        [Display(Name = "Acheteur", ResourceType = typeof(StringsAdmin))]
        public Miseur Acheteur { get; set; }
        // 1 objet à vendre possède une Configuration de PasDenchère et 1 TauxGlobalComissionAuVendeur qui ne change pas durant une vente
        public ConfigurationAdmin ConfigurationAdmin { get; set; }
        public VenteEvaluation VenteEvaluation { get; set; }

        public AchatEvaluation AchatEvaluation { get; set; }

        // 1 objet à 1 ou * des mises(Encheres)
        public virtual ICollection<Enchere> Encheres { get; set; }
        // 1 objet à 1 ou * des fichiers
        public virtual ICollection<Fichier> Fichiers { get; set; }
    }
}

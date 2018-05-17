using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels {
    public class ObjetsViewModel {
        [StringLength(75, MinimumLength = 10, ErrorMessage = "Please enter object name between 10 and 75 characters")]
        [Display(Name = "Name")]
        public string Nom { get; set; }

        [StringLength(300, MinimumLength = 10, ErrorMessage = "Please enter object description between 10 and 300 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Bid Price Start")]
        [Range(10, double.MaxValue, ErrorMessage = "The starting bid value must be greater than 10.")]
        public double PrixDepart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Bid Start Date")]
        public DateTime DateInscription { get; set; }
        //par défault on va mettre DateInscription à DateTime.Now à la création de l'annonce


        [DataType(DataType.Date)]
        [Display(Name = "Bid end date")]
        public DateTime DateLimite { get; set; } // fixé par le Vendeur à l'ajout dans ce ViewModel

        [Display(Name = "Time since start of sell")]
        public String DureeMiseVente {
            get {
                TimeSpan diff1 = DateTime.Now.Subtract(DateInscription);
                String outpp = diff1.ToString("%d");
                return (outpp);
            }
        }

        public Status Status { get; set; }

        [Display(Name = "Image")]
        [StringLength(1024, ErrorMessage = "Your image path is too long because exceeding 1024 characters. Try again!")]
        public string imageUrl { get; set; }

        public Categorie Categorie { get; set; }
        [Required]
        public Vendeur Vendeur { get; set; }

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

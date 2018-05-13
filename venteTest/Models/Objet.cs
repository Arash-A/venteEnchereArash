using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models {
    public enum Status {
        EnVente,
        Vendu
    }
    // Class corespondante à un objet à vendre
    public class Objet {
        public int ObjetID { get; set; } // The unique key

        [Required]
        public string Nom { get; set; }


        [Required]
        [MaxLength(400)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Price")]
        public double PrixDepart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date added")]
        public DateTime DateInscription { get; set; } // fixé par le Vendeur à l'ajout

        public DateTime DateLimite { get; set; } // fixé par le Vendeur à l'ajout

        [Display(Name = "Time since start of sell")]
        public String DureeMiseVente {
            get {
                TimeSpan diff1 = DateTime.Now.Subtract(DateInscription);
                 String outpp= diff1.ToString("%d");
                return (outpp);
            }
        }
        public Status Status { get; set; }

        [DisplayName("Image")]
        [StringLength(1024)]
        public string imageUrl { get; set; }

        //propriéte de Navigation
        public int CategorieID { get; set; }

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
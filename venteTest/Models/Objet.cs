using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models {
    public class Objet {
        public int ObjetID { get; set; } // The unique key

        [Required]
        public string Nom { get; set; }


        [Required]
        [MaxLength(300)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Prix")]
        public decimal PrixDepart { get; set; }

        private DateTime _date1 = DateTime.MinValue;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date d'ajout")]
        public DateTime DateInscription {
            get {
                return (_date1 == DateTime.MinValue) ? DateTime.Now : _date1;
            }
            set { _date1 = value; }
        }



        [Display(Name = "Durée de mise en vente")]
        public int DureeMiseVente { get; set; }


        [DisplayName("Image")]
        [StringLength(1024)]
        public string imageUrl { get; set; }

        //propriéte de Navigation
        public int CategorieID { get; set; }


        public virtual Categorie Categorie { get; set; }


        public virtual ICollection<Enchere> Encheres { get; set; }


        public virtual ICollection<Fichier> Fichiers { get; set; }
    }
}
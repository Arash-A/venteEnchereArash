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
        [MaxLength(400)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Prix")]
        public double PrixDepart { get; set; }

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
        public String DureeMiseVente {
            get {
                TimeSpan diff1 = DateTime.Now.Subtract(_date1);
               String duree= diff1.ToString("d");
                return (duree);
            }
            set {}
        }

        [DisplayName("Image")]
        [StringLength(1024)]
        public string imageUrl { get; set; }

        //propriéte de Navigation
        public int CategorieID { get; set; }

        public virtual Categorie Categorie { get; set; }

        public string Status { get; set; }

        public DateTime DateVendu { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Enchere> Encheres { get; set; }


        public virtual ICollection<Fichier> Fichiers { get; set; }
    }
}
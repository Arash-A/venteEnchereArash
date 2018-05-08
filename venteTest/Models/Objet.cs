using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace venteTest.Models
{
    public class Objet
    {
        public int ObjetID { get; set; } // The unique key

        [Required]
        public string Nom { get; set; }


        [Required]
        [MaxLength(300)]
        public string Description{ get; set; }


        [Required]
        public decimal PrixDepart { get; set; }


        [Required]
        public DateTime DateInscription{ get; set; }



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
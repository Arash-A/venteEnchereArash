using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.ObjetViewModel
{
    public class AnnoncerObjectViewModel
    {
        [Required, StringLength(75, MinimumLength = 10, ErrorMessage = "Please enter object name between 10 and 75 characters")]
        [Display(Name = "ObjetNom", ResourceType = typeof(StringsObjets))]
        public string Nom { get; set; }

        [Required, StringLength(300, MinimumLength = 10, ErrorMessage = "Please enter object description between 10 and 300 characters")]
        [Display(Name = "ObjetDescription", ResourceType = typeof(StringsObjets))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "ObjetPrix", ResourceType = typeof(StringsObjets))]
        [Range(10, double.MaxValue, ErrorMessage = "The starting bid value must be greater than 10.")]
        public double PrixDepart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "ObjetMiseDateDepart", ResourceType = typeof(StringsObjets))]
        public DateTime DateInscription { get; set; }
        //par défault on va mettre DateInscription à DateTime.Now à la création de l'annonce


        [Required]
        //[DataType(DataType.Date)]
        [Display(Name = "ObjetMiseDateFin", ResourceType = typeof(StringsObjets))]
        [UIHint("DateTimePicker")]
          public DateTime DateLimite { get; set; } // fixé par le Vendeur à l'ajout dans ce ViewModel

        // NON REQUIS POUR ce VIEWMODEL :
        ////public String DureeMiseVente {

        //public Status Status { get; set; }

        //public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectionStatus { get; set; }

        [Display(Name = "ObjetImage", ResourceType = typeof(StringsObjets))]
        [StringLength(1024, ErrorMessage = "Your image path is too long because exceeding 1024 characters. Try again!")]
        public string imageUrl { get; set; }

        [Display(Name = "CategorieId", ResourceType = typeof(StringsObjets))]
        //propriéte de Navigation
        public int CategorieID { get; set; }

        [Display(Name = "Categorie", ResourceType = typeof(StringsObjets))]
        public Categorie Categorie { get; set; }
    }
}

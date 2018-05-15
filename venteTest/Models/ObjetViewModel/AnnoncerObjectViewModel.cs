using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.ObjetViewModel
{
    public class AnnoncerObjectViewModel
    {
        [Required, StringLength(75, MinimumLength = 10, ErrorMessage = "Please enter object name between 10 and 75 characters")]
        [Display(Name = "Name")]
        public string Nom { get; set; }

        [Required, StringLength(300, MinimumLength = 10, ErrorMessage = "Please enter object description between 10 and 300 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(10, double.MaxValue, ErrorMessage = "The starting bid value must be greater than 10.")]
        public double PrixDepart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Bid start date")]
        public DateTime DateInscription { get; set; }
        //par défault on va mettre DateInscription à DateTime.Now à la création de l'annonce


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Bid end date")]
        [UIHint("DateTimePicker")]
          public DateTime DateLimite { get; set; } // fixé par le Vendeur à l'ajout dans ce ViewModel

        // NON REQUIS POUR ce VIEWMODEL :
        ////public String DureeMiseVente {

        //public Status Status { get; set; }

        //public Microsoft.AspNetCore.Mvc.Rendering.SelectList SelectionStatus { get; set; }

        [Display(Name = "Image")]
        [StringLength(1024, ErrorMessage = "Your image path is too long because exceeding 1024 characters. Try again!")]
        public string imageUrl { get; set; }

        //propriéte de Navigation
        public int CategorieID { get; set; }

        public Categorie Categorie { get; set; }
    }
}

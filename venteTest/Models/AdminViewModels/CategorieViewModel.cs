using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class CategorieViewModel
    {
        public int CategorieId { get; set; }

        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Please enter name of categorie between 5 and 30 characters")]
        public String Nom { get; set; }

        [Required, StringLength(256, MinimumLength = 5, ErrorMessage = "Please enter description between 5 and 256 characters")]
        public String Description { get; set; }
    }
}

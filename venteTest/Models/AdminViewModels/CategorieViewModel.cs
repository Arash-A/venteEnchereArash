using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class CategorieViewModel
    {
        public int CategorieId {
            get;
            set;
        }

        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Please enter name of categorie")]
        public String Nom {
            get;
            set;
        }

//      [RegularExpression("[a-z A-Z]{10}", ErrorMessage = "Please enter description of at least 10 characters")]
        [Required, StringLength(256, MinimumLength = 5, ErrorMessage = "Please enter description")]
        public String Description { get; set; }
    }
}

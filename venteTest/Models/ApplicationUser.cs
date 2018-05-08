using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace venteTest.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        //[Required(ErrorMessage = "Please enter your civility")]
        public String Civilite { get; set; }

        // [Required]
        public String Nom { get; set; }


        //[Required(ErrorMessage = "Please enter your last Name")]
        public String Prenom { get; set; }


        // [Required(ErrorMessage = "Please enter your language")]
        public String Langue { get; set; }

        //[Required]
        public DateTime? DateInscription { get; set; }

        public virtual ICollection<Enchere> Encheres { get; set; }
    }
}

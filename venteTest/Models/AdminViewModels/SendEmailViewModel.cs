using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class SendEmailViewModel
    {
        [Required, Display(Name = "Member Email"), EmailAddress]
        public string ToEmail { get; set; }

        [Required, StringLength(500, MinimumLength = 4, ErrorMessage = "Please enter email message between 4 and 500 characters.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required, StringLength(25, MinimumLength = 4, ErrorMessage = "Please enter email title name between 4 and 25 characters.")]
        [Display(Name = "Titre")]
        public string Titre { get; set; }
    }
}

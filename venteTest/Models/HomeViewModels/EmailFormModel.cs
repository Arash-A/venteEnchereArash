using System.ComponentModel.DataAnnotations;

namespace venteTest.Models.HomeViewModels
{
    public class EmailFormModel
    {

        //COURRIEL
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        //MESSAGE
        [Required]
        public string Message { get; set; }

        ////Verification is the reCaptcha
        //[Required]
        //public bool Verification { get; set; }
    }
}

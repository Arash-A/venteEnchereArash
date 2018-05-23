using System.ComponentModel.DataAnnotations;
using venteTest.Resources.Models;

namespace venteTest.Models.HomeViewModels
{
    public class EmailFormModel
    {

        //COURRIEL
        [Required, Display(Name = "VotreEmail", ResourceType = typeof(StringsHome))]
        public string FromEmail { get; set; }

        //MESSAGE
        [Required, Display(Name = "VotreCourriel", ResourceType = typeof(StringsHome))]
        public string Message { get; set; }

        ////Verification is the reCaptcha
        //[Required]
        //public bool Verification { get; set; }
    }
}

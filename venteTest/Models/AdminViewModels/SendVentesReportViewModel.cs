using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AdminViewModels
{
    public class SendVentesReportViewModel
    {
        [Required, Display(Name = "Annee", ResourceType = typeof(StringsAdmin))]
        public int Year { get; set; }
    }
}

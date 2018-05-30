using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class SendVentesReportViewModel
    {
        [Required]
        public int Year { get; set; }
    }
}

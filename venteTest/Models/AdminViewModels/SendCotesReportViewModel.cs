using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venteTest.Models.AdminViewModels
{
    public class SendCotesReportViewModel
    {
        [Required]
        public int SelectedMonthStart {
            get;
            set;
        }
        [Required]
        public int SelectedYearStart {
            get;
            set;
        }
        [Required]
        public int SelectedMonthEnd {
            get;
            set;
        }
        [Required]
        public int SelectedYearEnd {
            get;
            set;
        }
    }
}

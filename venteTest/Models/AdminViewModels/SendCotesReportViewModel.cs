using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AdminViewModels
{
    public class SendCotesReportViewModel
    {
        [Required, Display(Name = "MoisDepart", ResourceType = typeof(StringsAdmin))]
        public int SelectedMonthStart {
            get;
            set;
        }

        [Required, Display(Name = "AnneeDepart", ResourceType = typeof(StringsAdmin))]
        public int SelectedYearStart {
            get;
            set;
        }

        [Required, Display(Name = "MoisFin", ResourceType = typeof(StringsAdmin))]
        public int SelectedMonthEnd {
            get;
            set;
        }

        [Required, Display(Name = "AnneeFin", ResourceType = typeof(StringsAdmin))]
        public int SelectedYearEnd {
            get;
            set;
        }
    }
}

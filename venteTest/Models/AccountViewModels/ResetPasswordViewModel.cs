﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Resources.Models;

namespace venteTest.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required, Display(Name = "Courriel", ResourceType = typeof(StringsAccount))]
        [EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Mdp", ResourceType = typeof(StringsAccount))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmerMdp", ResourceType = typeof(StringsAccount))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}

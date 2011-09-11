using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace RecommendStuff.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="*")]
        [DisplayName("Alias")]
        [StringLength(15, MinimumLength = 3,ErrorMessage="Must be between 3 and 15 characters long")]
        public string username { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Must be between 4 and 15 characters long")]
        [DisplayName("Password")]
        public string password { get; set; }
        
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Valid Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "*")]
        [EqualTo("password")]
        [DisplayName("Retyped Password")]
        public string retypePassword { get; set; }

        public string gender { get; set; }
        public string county { get; set; }
        public string dobYear { get; set; }
        public List<SelectListItem> GenderOptions { get; set; }
        public List<SelectListItem> dobYearOptions { get; set; }
        public List<SelectListItem> countyOptions { get; set; }
        public string errorMessage { get; set; }
    }
}
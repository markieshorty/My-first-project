using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecommendStuff.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Alias")]
        [MinLength(3)]
        public string username { get; set; }

        [Required]
        [MinLength(4)]
        [DisplayName("Password")]
        public string password { get; set; }
        
        [DisplayName("Email Address")]
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Valid Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [MinLength(4)]
        [DisplayName("Retyped Password")]
        public string retypePassword { get; set; }

        public string gender { get; set; }
        public string stereotype { get; set; }
        public string dobYear { get; set; }
        public List<SelectListItem> GenderOptions { get; set; }
        public List<SelectListItem> dobYearOptions { get; set; }
        public List<SelectListItem> stereotypeOptions { get; set; }
        public string errorMessage { get; set; }
    }
}
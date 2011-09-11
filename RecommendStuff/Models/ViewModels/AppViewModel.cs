using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecommendStuff.Models.ViewModels
{
    public class AppViewModel
    {
        [Required]
        public string appName { get; set; }

        [Required]
        public string platform { get; set; }

        public string comment { get; set; }

        [Required]
        [RegularExpression(@"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)", ErrorMessage = "A valid URL is required.")]
        public string url { get; set; }

        public List<SelectListItem> platforms { get; set; } 
    }
}
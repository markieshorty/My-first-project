using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecommendStuff.Models.ViewModels
{
    public class FilmViewModel
    {
        [Required]
        public string filmName { get; set; }

        public string comment { get; set; }

        [Required]
        [RegularExpression(@"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)", ErrorMessage = "A valid URL is required.")]
        public string url { get; set; }
    }
}
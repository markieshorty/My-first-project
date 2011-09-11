using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecommendStuff.Models.ViewModels
{
    public class BookViewModel
    {
        [Required]
        public string bookName { get; set; }

        [Required]
        public string authorName { get; set; }

        public string comment { get; set; }

        [Required]
        [RegularExpression(@"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)", ErrorMessage = "A valid URL is required.")]
        public string url { get; set; }
    }
}
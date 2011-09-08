using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecommendStuff.Models.ViewModels
{
    public class GameViewModel
    {
        [Required]
        public string gameName { get; set; }

        [Required]
        public string consoleName { get; set; }

        public string comment { get; set; }

        [Required]
        [RegularExpression(@"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)", ErrorMessage = "A valid URL is required.")]
        public string url { get; set; }

        public List<SelectListItem> consoles { get; set; }
    }
}
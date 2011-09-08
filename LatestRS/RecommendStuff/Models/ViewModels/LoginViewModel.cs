using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecommendStuff.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {  
        }
        public User user { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string errorMessage { get; set; }

    }
}
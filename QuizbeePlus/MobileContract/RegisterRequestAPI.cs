using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizbeePlus.MobileContract
{
    public class RegisterRequestAPI
    {
        public string APIKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }


}
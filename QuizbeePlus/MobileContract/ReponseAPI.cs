using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizbeePlus.MobileContract
{
    public class ReponseAPI
    {
        public string Status  { get; set; }
        public string AuthKey { get; set; }
        public string Error { get; set; }
    }

    public class ForgetPasswordRequest
    {
        public string APIKey { get; set; }
        public string Email { get; set; }
    }
    public class ForgetPasswordResponse
    {
        public string Status { get; set; }
        public string OTPCODE { get; set; }
        public string Error { get; set; }
    }

}
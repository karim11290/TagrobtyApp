
using Postal;
using QuizbeePlus.Entities;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Xamarin.Essentials;

namespace QuizbeePlus
{
    public class BaseLogics
    {
        public static string BaseUrl = "https://localhost:44394/"; //localhost:88823 or http://www.example.com
        public static string adminEmail = "karimabobakribrahim@gmail.com"; //for password forgot send email from email address




        public static void PasswordForgotEmailSend(QuizbeeUser userObj)
        {
            dynamic email = new Postal.Email("PasswordForgot");
            email.To = userObj.Email;
            email.from = adminEmail;
            email.subject = "Password Reset Hello Navsari";
            email.username = userObj.Email;
            email.password = userObj.Password;
            email.name = userObj.UserName;
            email.baseurl = BaseUrl;
            email.Send();
        }


    }
}
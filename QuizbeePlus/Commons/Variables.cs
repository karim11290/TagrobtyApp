using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizbeePlus.Commons
{
    public static class Variables
    {
        public static string ImageFolderPath { get; set; } = "/Content/images/";

        public static string Administrator { get; set; } = "Administrator";
        public static string Manager { get; set; } = "Manager";
        public static string UserRole { get; set; } = "User";

        public static string RandomString { get {
                return "";
            }
        }
    }
}
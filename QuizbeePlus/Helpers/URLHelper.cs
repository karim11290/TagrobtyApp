using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Helpers
{
    public static class URLHelper
    {
        public static string Home(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;
            
            if(string.IsNullOrEmpty(searchTerm) && (!pageNo.HasValue || pageNo.Value == 1) && (!pageSize.HasValue || pageSize.Value == 10))
            {
                routeURL = helper.RouteUrl("Home", new
                {
                    controller = "Home",
                    action = "Index"
                });
            }
            else
            {
                routeURL = helper.RouteUrl("Home", new
                {
                    controller = "Home",
                    action = "Index",
                    search = searchTerm,
                    page = pageNo.Value,
                    items = pageSize.Value
                });
            }
            
            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);

            return routeURL.ToLower();
        }

        public static string Register(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Register", new
            {
                controller = "Account",
                action = "Register"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Login(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Login", new
            {
                controller = "Account",
                action = "Login"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string Logout(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Logout", new
            {
                controller = "Account",
                action = "LogOff"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Me(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Me", new
            {
                controller = "Manage",
                action = "Index"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UploadImage(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UploadImage", new
            {
                controller = "Shared",
                action = "UploadImage"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string MyPhoto(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("MyPhoto", new
            {
                controller = "Manage",
                action = "MyPhoto"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateInfo(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateInfo", new
            {
                controller = "Manage",
                action = "UpdateInfo"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateUserInfo(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateUserInfo", new
            {
                controller = "Manage",
                action = "UpdateUserInfo",
                userID= userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DeleteUser(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteUser", new
            {
                controller = "Manage",
                action = "DeleteUser",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdatePassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdatePassword", new
            {
                controller = "Manage",
                action = "UpdatePassword"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

  
   
        public static string ControlPanel(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ControlPanel", new
            {
                controller = "ControlPanel",
                action = "Index"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Users(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Users", new
            {
                controller = "ControlPanel",
                action = "Users",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UserDetails(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserDetails", new
            {
                controller = "ControlPanel",
                action = "UserDetails",
                ID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string UserPhoto(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserPhoto", new
            {
                controller = "Manage",
                action = "UserPhoto",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CPRoles(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Roles", new
            {
                controller = "ControlPanel",
                action = "Roles",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CPNewRole(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("NewRole", new
            {
                controller = "ControlPanel",
                action = "NewRole"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string RoleDetails(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("RoleDetails", new
            {
                controller = "ControlPanel",
                action = "RoleDetails",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateRole(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateRole", new
            {
                controller = "ControlPanel",
                action = "UpdateRole",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string DeleteRole(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteRole", new
            {
                controller = "ControlPanel",
                action = "DeleteRole",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string AddUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AddUserRole", new
            {
                controller = "ControlPanel",
                action = "AddUserRole",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string RemoveUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("RemoveUserRole", new
            {
                controller = "ControlPanel",
                action = "RemoveUserRole",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }



        

    }
}
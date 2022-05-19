using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuizbeePlus
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UnAuthorized",
                url: "unauthorized/",
                defaults: new { controller = "Base", action = "UnAuthorized" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register/",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login/",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout/",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "Me",
                url: "me/",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "UploadPhoto",
                url: "me/upload-photo/",
                defaults: new { controller = "Manage", action = "UploadPhoto" }
            );

            routes.MapRoute(
                name: "MyPhoto",
                url: "me/photo/",
                defaults: new { controller = "Manage", action = "MyPhoto" }
            );

            routes.MapRoute(
                name: "UpdateInfo",
                url: "me/update-info/",
                defaults: new { controller = "Manage", action = "UpdateInfo" }
            );

            routes.MapRoute(
                name: "UpdatePassword",
                url: "me/update-password/",
                defaults: new { controller = "Manage", action = "UpdatePassword" }
            );

         


            routes.MapRoute(
                name: "UploadImage",
                url: "upload-image",
                defaults: new { controller = "Shared", action = "UploadImage" }
            );


          

           

            routes.MapRoute(
                name: "ControlPanel",
                url: "control-panel/",
                defaults: new { controller = "ControlPanel", action = "Index" }
            );

            routes.MapRoute(
                name: "Users",
                url: "control-panel/users/",
                defaults: new { controller = "ControlPanel", action = "Users" }
            );

            routes.MapRoute(
                name: "UserDetails",
                url: "control-panel/user/user-details/",
                defaults: new { controller = "ControlPanel", action = "UserDetails" }
            );

            routes.MapRoute(
                name: "UserPhoto",
                url: "control-panel/user/photo/",
                defaults: new { controller = "Manage", action = "UserPhoto" }
            );

            routes.MapRoute(
                name: "UpdateUserInfo",
                url: "control-panel/user/update-info/",
                defaults: new { controller = "Manage", action = "UpdateUserInfo" }
            );

            routes.MapRoute(
                name: "DeleteUser",
                url: "control-panel/user/delete/",
                defaults: new { controller = "Manage", action = "DeleteUser" }
            );

          

            routes.MapRoute(
                name: "Roles",
                url: "control-panel/roles/",
                defaults: new { controller = "ControlPanel", action = "Roles" }
            );

            routes.MapRoute(
                name: "NewRole",
                url: "control-panel/role/new/",
                defaults: new { controller = "ControlPanel", action = "NewRole" }
            );

            routes.MapRoute(
                name: "RoleDetails",
                url: "control-panel/role/role-details/",
                defaults: new { controller = "ControlPanel", action = "RoleDetails" }
            );

            routes.MapRoute(
                name: "UpdateRole",
                url: "control-panel/role/update/",
                defaults: new { controller = "ControlPanel", action = "UpdateRole" }
            );

            routes.MapRoute(
                name: "DeleteRole",
                url: "control-panel/role/delete/",
                defaults: new { controller = "ControlPanel", action = "DeleteRole" }
            );

            routes.MapRoute(
                name: "AddUserRole",
                url: "control-panel/user/role/add/",
                defaults: new { controller = "ControlPanel", action = "AddUserRole" }
            );

            routes.MapRoute(
                name: "RemoveUserRole",
                url: "control-panel/user/role/remove/",
                defaults: new { controller = "ControlPanel", action = "RemoveUserRole" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

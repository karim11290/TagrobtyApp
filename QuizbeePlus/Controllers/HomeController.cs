using QuizbeePlus.Services;
using QuizbeePlus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public ActionResult Index(string search, int? page, int? items)
        {
        

            return View();
        }
    }
}
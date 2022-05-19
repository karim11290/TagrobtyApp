using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Data
{
    // Configure the application sign-in manager which is used in this application.
    public class QuizbeeSignInManager : SignInManager<QuizbeeUser, string>
    {
        public QuizbeeSignInManager(QuizbeeUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(QuizbeeUser user)
        {
            return user.GenerateUserIdentityAsync((QuizbeeUserManager)UserManager);
        }

        public static QuizbeeSignInManager Create(IdentityFactoryOptions<QuizbeeSignInManager> options, IOwinContext context)
        {
            return new QuizbeeSignInManager(context.GetUserManager<QuizbeeUserManager>(), context.Authentication);
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Data
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class QuizbeeUserManager : UserManager<QuizbeeUser>
    {
        public QuizbeeUserManager(IUserStore<QuizbeeUser> store)
            : base(store)
        {
        }

        public static QuizbeeUserManager Create(IdentityFactoryOptions<QuizbeeUserManager> options, IOwinContext context)
        {
            var manager = new QuizbeeUserManager(new UserStore<QuizbeeUser>(context.Get<QuizbeeContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<QuizbeeUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<QuizbeeUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<QuizbeeUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<QuizbeeUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

}

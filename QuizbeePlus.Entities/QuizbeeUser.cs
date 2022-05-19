using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuizbeePlus.Entities
{
    //You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class QuizbeeUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<QuizbeeUser> manager)
        {
            return await GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<QuizbeeUser> manager,
            string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string Password { get; set; }
        public string Photo { get; set; }
        public Nullable<DateTime> RegisteredOn { get; set; }
    }    
}

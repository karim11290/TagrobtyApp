using Microsoft.AspNet.Identity.EntityFramework;
using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Data
{
    public class QuizbeeContext : IdentityDbContext<QuizbeeUser>, IDisposable
    {
        public QuizbeeContext()
            : base("QuizbeePlusConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<QuizbeeContext>(new CreateDatabaseIfNotExists<QuizbeeContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<QuizbeeUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        public static QuizbeeContext Create()
        {
            return new QuizbeeContext();
        }

   
        public DbSet<Image> Images { get; set; }

        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessAmenities> BusinessAmenities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }


    }
}

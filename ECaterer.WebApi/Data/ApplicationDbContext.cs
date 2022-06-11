using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.WebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "producer", NormalizedName = "PRODUCER".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1cf47549-bf5a-49b4-805a-48cad29cdea8", Name = "deliverer", NormalizedName = "DELIVERER".ToUpper() });

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "producent@producent.pl",
                    NormalizedUserName = "PRODUCENT@PRODUCENT.PL",
                    Email = "producent@producent.pl",
                    NormalizedEmail = "PRODUCENT@PRODUCENT.PL",
                    PasswordHash = hasher.HashPassword(null, "Producent123!")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
              new IdentityUser
              {
                  Id = "d645ed4d-8474-4ead-a3b3-0b42f63d35a4", // primary key
                  UserName = "dostawca@dostawca.pl",
                  NormalizedUserName = "DOSTAWCA@DOSTAWCA.PL",
                  Email = "dostawca@dostawca.pl",
                  NormalizedEmail = "DOSTAWCA@DOSTAWCA.PL",
                  PasswordHash = hasher.HashPassword(null, "Dostawca123!")
              }
          );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "1cf47549-bf5a-49b4-805a-48cad29cdea8",
                   UserId = "d645ed4d-8474-4ead-a3b3-0b42f63d35a4"
               });
        }
    }
}

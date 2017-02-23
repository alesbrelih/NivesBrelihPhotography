using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NivesBrelihPhotography.Models;

namespace NivesBrelihPhotography.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NivesBrelihPhotography.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NivesBrelihPhotography.Models.ApplicationDbContext";
        }

        protected override void Seed(NivesBrelihPhotography.Models.ApplicationDbContext context)
        {
            if (!(context.Users.Any(u => u.UserName == "nivesbrelih@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "nivesbrelih@gmail.com",
                    PhoneNumber = "041621928",
                    Email = "nivesbrelih@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "to$XA0X86s");
            }
            if (!(context.Users.Any(u => u.UserName == "krneki07@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "krneki07@gmail.com",
                    PhoneNumber = "041621928",
                    Email = "krneki07@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "ewn5R$61");
            }
        }
    }
}

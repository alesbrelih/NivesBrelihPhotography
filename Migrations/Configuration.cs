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
            AutomaticMigrationsEnabled = false;
            ContextKey = "NivesBrelihPhotography.Models.ApplicationDbContext";
        }

        protected override void Seed(NivesBrelihPhotography.Models.ApplicationDbContext context)
        {
            if (!(context.Users.Any(u => u.UserName == "krneki07@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "krneki07@gmail.com", PhoneNumber = "0797697898" };
                userManager.Create(userToInsert, "E7x0j225..");
            }
        }
    }
}

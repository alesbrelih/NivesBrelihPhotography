using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NivesBrelihPhotography.Models;

namespace NivesBrelihPhotography.HelperClasses
{
    /// <summary>
    /// Methods to calculate various data 
    /// Created 27.12.2015
    /// </summary>
    public class HelperMethods
    {
        public string GetDisplayName()
        {
            //            //When the User Logs in, you can display the profile information by doing the following
            //            //Get the current logged in UserId, so you can look the user up in ASP.NET Identity system
            //            var currentUserId = User.Identity.GetUserId();
            //            //Instantiate the UserManager in ASP.Identity system so you can look up the user in the system
            //var manager = new UserManager<MyUser>(new UserStore<MyUser>(new MyDbContext()));
            //            //Get the User object
            //            var currentUser = manager.FindById(User.Identity.GetUserId());
            //            //Get the profile information about the user


            //            currentUser.BirthDate
            return "";
        }
    }
}
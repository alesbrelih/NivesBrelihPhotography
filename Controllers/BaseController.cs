using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NivesBrelihPhotography.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // TEMPORARY FIX - REMOVING MULTILANG - 8.2.2017

            //string cultureName = "";

            ////find cookie
            //var cookie = Request.Cookies["_culture"];

            ////get value from cookie
            //if (cookie != null && cookie.Value!=null)
            //{
            //    cultureName = cookie.Value;
            //}
            //else
            //{
            //    //if no cookie value get default culture from browser
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length>0
            //        ? Request.UserLanguages[0]
            //        : null;
            //}

            ////Use helper for safety and avoid all catches defined in helepr (null string, empty string, not matching culture)
            //cultureName = HelperClasses.CultureHelper.GetCultureName(cultureName);

            ////set culture
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("sl-SI");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}
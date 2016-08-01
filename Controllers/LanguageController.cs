using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace NivesBrelihPhotography.Controllers
{
    public class LanguageController : BaseController
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string cultureName)
        {


            //using helper so the route isnt corrupted
            var culture = HelperClasses.CultureHelper.GetCultureName(cultureName);

            //check if cookie exists -> create or modify it

            var cookie = Request.Cookies["_culture"];

            //cookie already exists so we modify value
            if (cookie != null && cookie.Value != null)
            {
                cookie.Value = culture;
            }

            //cookie doesnt exist
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(2);
            }

            //set cookie in response
            Response.SetCookie(cookie);

            //if request happened from a page
            var returnPage = Request.UrlReferrer;
            if (returnPage != null)
            {
              return  Redirect(returnPage.ToString());
            }

            //request was manually requested with link
            return RedirectToAction("Index", "Photos");
        }
    }
}
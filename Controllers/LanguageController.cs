using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.Controllers
{
    public class LanguageController : BaseController
    {
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

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
            //var returnPage = Request.UrlReferrer.LocalPath;
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.LocalPath);
            }
            return RedirectToAction("Index", "Portfolio");
           
        }


        //gets refferer controller name
        private string GetReferrerControlerName(string urlRefferer)
        {
            var fullUrl = urlRefferer;
            string url = fullUrl;

            var request = new HttpRequest(null, url, null);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            var values = routeData.Values;
            string controllerName = values["controller"].ToString();

            return controllerName;
        }

        //gets refferer action name
        private string GetReferrerActionName(string urlRefferer)
        {
            var fullUrl = urlRefferer;
            string url = fullUrl;

            var request = new HttpRequest(null, url, null);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            var values = routeData.Values;
            if (values["action"] != null)
            {
                string actionName = values["action"].ToString();
                return actionName;
            }

            return "Index";
            
            
       
        }

        //gets refferer route values
        private RouteValueDictionary GetReffererRouteValues(string urlRefferer)
        {
            //split refferer url to main address and route keys/values
            var splitReffererUrl = urlRefferer.Split(new char[] { '?', '&' });

            //create new dictionary - if no values it will remain empty
            var urlReffererRouteValues = new RouteValueDictionary();

            //check all route values if exist - 0 index is main url before route values
            for (int i = 1; i < splitReffererUrl.Length; i++)
            {
                //split value and key from every route value
                var paramSplit = splitReffererUrl[i].Split('=');

                //check length just in case
                if (paramSplit.Length.Equals(2))
                {
                    //add to route value dictionary
                    urlReffererRouteValues.Add(paramSplit[0], paramSplit[1]);
                }
            }
            return urlReffererRouteValues;
        }

    }
}
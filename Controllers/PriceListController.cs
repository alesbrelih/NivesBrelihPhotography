using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NivesBrelihPhotography.Controllers
{
    [RoutePrefix("Cenik")]
    public class PriceListController : BaseController
    {
        // GET: PriceList
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}
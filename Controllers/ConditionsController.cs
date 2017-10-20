using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NivesBrelihPhotography.Controllers
{
    public class ConditionsController : BaseController
    {
        private NbpContext _db = new NbpContext();

        // GET: Conditions
        public ActionResult Index()
        {
            TermsConditions terms = _db.TermsConditions.FirstOrDefault();
            string content = terms != null ? terms.Content : "";
            return View((object)content);
        }
    }
}
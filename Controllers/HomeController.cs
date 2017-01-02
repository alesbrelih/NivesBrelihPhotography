using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;

namespace NivesBrelihPhotography.Controllers
{
    public class HomeController : Controller
    {
        //get db connection
        private NbpContext _db = new NbpContext();

        // GET: Home
        public ActionResult Index()
        {
            //get photos

            return View();
        }

        //dispose method
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
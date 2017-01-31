using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;

namespace NivesBrelihPhotography.Controllers
{
    public class ReferencesController : BaseController
    {
        //db
        private NbpContext db = new NbpContext();

        // GET: References
        public async Task<ActionResult> Index()
        {
            //get all reference photos
            var referencePhotos = await db.ReferencePhotos.OrderBy(x => x.ReferenceId).ToListAsync();

            //return view with data
            return View(referencePhotos);
        }

        //dispose method
        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
            base.Dispose(disposing);
        }
    }
}
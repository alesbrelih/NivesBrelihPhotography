using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;

namespace NivesBrelihPhotography.Controllers
{
    public class HomeController : Controller
    {
        //get db connection
        private NbpContext _db = new NbpContext();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            //get photos
            var query = await _db.Photos.Where(x => x.HomeCarousel).Select(x=>new PhotoView()
            {
                PhotoUrl = x.PhotoUrl,
                PhotoTitle = x.PhotoTitle
            }).ToListAsync();

            //no photos were added to home carousel yet
            if (query.Count == 0)
            {
                return RedirectToAction("Index","Photos");
            }

            //return view
            return View(query);
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.AboutModels.ViewModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;
using PagedList;

namespace NivesBrelihPhotography.Controllers
{
    public class ReferencesController : BaseController
    {
        //db
        private NbpContext _db = new NbpContext();

        // GET: References
        public async Task<ActionResult> Index(int? page, int pageSize = 5)
        {
            //get all reference photos
            var references = await _db.References.OrderBy(x => x.ReferenceId)
                .Select(x=> new ReferenceView()
                {
                    ReferenceId = x.ReferenceId,
                    Title = x.ReferenceTitle,
                    LeadPhoto = new PhotoView()
                    {
                        PhotoTitle = x.LeadPhoto.PhotoTitle,
                        PhotoUrl = x.LeadPhoto.PhotoUrl
                    }
                })
                .ToListAsync();

            int pageNumber = (page ?? 1);

            //return view with data
            return View(references.ToPagedList(pageNumber,pageSize));
        }

        //GET: single reference detailed information
        public async Task<ActionResult> Reference(int? id = null)
        {
            //id wasnt given
            if (id == null)
            {
                //redirect user to index
                return RedirectToAction("Index");
            }

            var reference = await _db.References.FindAsync(id);
            var referenceVm = new ReferenceDetailsView(reference); //create vm

            //show view
            return View(referenceVm);
        }

        //dispose method
        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
            base.Dispose(disposing);
        }
    }
}
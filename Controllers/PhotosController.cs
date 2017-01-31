using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;


namespace NivesBrelihPhotography.Controllers
{
    [RoutePrefix("Portfolio")]
    public class PhotosController : BaseController
    {
        private NbpContext db = new NbpContext();
        //current category
        private static int? _categoryId;

        // GET: Photos
        /// <summary>
        /// Index for Photos
        /// </summary>
        /// <param name="categoryId">Category Id to browse from</param>
        /// <returns></returns>
        [Route]
        public async Task<ActionResult> Index(int? categoryId = null)
        {
            #region indexPhotoList

            if (Request.IsAjaxRequest())
            {
                if (!categoryId.Equals(null))
                {
                    //Ajax request for specific category

                    //save current category
                    _categoryId = categoryId;

                    //list of photos matching current category
                    List<Photo> categoryPhotos =
                        await
                            db.PhotoCategories.Where(x => x.CategoryId.Equals((int) categoryId))
                                .Select(x => x.Photo)
                                .Take(10)
                                .ToListAsync();

                    //if ajax request for category then return partial view
                    return PartialView("_indexPhotos", categoryPhotos);

                }

                
                _categoryId = null; //reset currently selected category to all

                ViewBag.CurrentCategory = null; //current category

                var moreCategoryPhotos = await db.Photos.OrderBy(x => x.Uploaded).Take(10).ToListAsync();

                return PartialView("_indexPhotos", moreCategoryPhotos);
                
                

            }

            //reset current currently selected category
            _categoryId = null;

            //displayed categories
            ViewBag.Categories = await db.Categories.ToListAsync();

            //get photos
            var photos = await db.Photos.OrderBy(x => x.Uploaded).Take(10).ToListAsync();

            //returns pictures ordered by date
            return View(photos);
            
            
            #endregion


        }

        //JSON more Index results
        public async Task<JsonResult> LoadPhotos(int pageNumber = 0)
        {
            //return list of photos, 
            //if there are no photos it will send empy aray as json and then in JS it will stop processing
            List<PhotoView> returnList = new List<PhotoView>();  //Using PhotoView else object is too big for JSON, causes 500 server error


            //skip number of records in database
            var skipNumber = pageNumber * 10;

            //if user selects a specific category of photos
            if (_categoryId != null)
            {
                returnList =
                   await db.PhotoCategories.Where(x => x.CategoryId.Equals((int)_categoryId))
                       .Select(x => new PhotoView() { PhotoTitle = x.Photo.PhotoTitle, PhotoUrl = x.Photo.PhotoUrl })
                       .Skip(skipNumber)
                       .Take(10).ToListAsync();
            }

            //user wants to see all photos
            else
            {
                returnList = await db.Photos.OrderBy(x => x.Uploaded).Select(x => new PhotoView() { PhotoUrl = x.PhotoUrl, PhotoTitle = x.PhotoTitle }).Skip(skipNumber).Take(10).ToListAsync();
            }
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }

            }
            base.Dispose(disposing);
        }
    }
}

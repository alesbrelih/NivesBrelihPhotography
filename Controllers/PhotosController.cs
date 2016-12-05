using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;

namespace NivesBrelihPhotography.Controllers
{
    public class PhotosController : BaseController
    {
        private NbpContext db = new NbpContext();
        //current category
        private int? _categoryId = null;

        // GET: Photos
        /// <summary>
        /// Index for Photos
        /// </summary>
        /// <param name="categoryId">Category Id to browse from</param>
        /// <returns></returns>
        public ActionResult Index(int? categoryId = null)
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
                        db.PhotoCategories.Where(x => x.CategoryId.Equals((int)categoryId)).Select(x => x.Photo).Take(10).ToList();

                    //if ajax request for category then return partial view
                    return PartialView("_indexPhotos", categoryPhotos);

                }

                _categoryId = null; //reset currently selected category to all

                ViewBag.CurrentCategory = null; //current category

                return PartialView("_indexPhotos", db.Photos.OrderBy(x => x.Uploaded).Take(10).ToList());

            }

            //reset current currently selected category
            _categoryId = null;

            //displayed categories
            ViewBag.Categories = db.Categories.ToList();

            //returns pictures ordered by date
            return View(db.Photos.OrderBy(x => x.Uploaded).Take(10).ToList());
            #endregion


        }

        //JSON more Index results
        public JsonResult LoadPhotos(int pageNumber = 0)
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
                   db.PhotoCategories.Where(x => x.CategoryId.Equals((int)_categoryId))
                       .Select(x => new PhotoView() { PhotoTitle = x.Photo.PhotoTitle, PhotoUrl = x.Photo.PhotoUrl })
                       .Skip(skipNumber)
                       .Take(10).ToList();
            }

            //user wants to see all photos
            else
            {
                returnList = db.Photos.OrderBy(x => x.Uploaded).Select(x => new PhotoView() { PhotoUrl = x.PhotoUrl, PhotoTitle = x.PhotoTitle }).Skip(skipNumber).Take(10).ToList();
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
                _categoryId = null;

            }
            base.Dispose(disposing);
        }
    }
}

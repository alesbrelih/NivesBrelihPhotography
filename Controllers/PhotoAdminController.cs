using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.Controllers
{
    public class PhotoAdminController : BaseController
    {
        //current orderBy
        private static int _orderBy = -1;

        //ascending or descending order, ascending is true
        private static bool _orderType = true;

        private static readonly int _pageSize = 10;

        //connection to db
        private NbpContext _db = new NbpContext();

        // GET: PhotoAdmin
        public ActionResult Index(int categoryId = -1, int orderBy = -1,int page = 1)
        {
            ViewBag.CurrentPage = page;
            if (Request.IsAjaxRequest())
            {
                var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex( page - 1, _pageSize,_db);
                return PartialView("_photoAdminIndexList",query);
            }

            if (orderBy > 0 && orderBy < 5) //valid orderBy
            {
                if (_orderBy == orderBy)
                {
                    _orderType = !_orderType;
                }
                if (_orderBy != orderBy)
                {
                    _orderType = true;
                    _orderBy = orderBy;
                }
               
                var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex(page-1,_pageSize,_db);

                ViewBag.NumberOfPages = GetNumberOfPages(query);
                return View(query);
            }
            //default query when this action is first accessed (no ajax or requests)
            if (categoryId == -1)
            {
                var query = _db.Photos.OrderBy(x => x.Uploaded)
                    .Select(x => new AdminPhotoIndexVm()
                {
                    PhotoId = x.PhotoId,
                    PhotoUrl = x.PhotoUrl,
                    PhotoTitle = x.PhotoTitle,
                    Album = x.PhotoAlbum.AlbumName,
                    OnPortfolio = x.IsOnFrontPage,
                    Uploaded = x.Uploaded
                }).ToList();

                //match ceiling for maximum number of pages
                ViewBag.NumberOfPages = GetNumberOfPages(query);
                return View(query);
            }
            return View();
        }

        //GET: PhotoAdd
        [HttpGet]
        public ActionResult Add()
        {
            //create viewmodel for photo create
            var photoAdminView = new AdminPhotoCreateVm();

            //return view
            return View(photoAdminView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Add(
        //    [Bind(Include = "PhotoDescription,AlbumId,PhotoFile,IsOnPortfolio,IsAlbumCover,PhotoCategories,PhotoTitle,PhotoUrl")] AdminPhotoCreateVm photoCreateVm)
        //{
        //    //check if user selected any of the categories (1 is needed)
        //    var numberOfCategories = photoCreateVm.PhotoCategories.Count();
        //    if (numberOfCategories == 0)
        //    {
        //        //return view with error that categories needs to be checked
        //        ModelState.AddModelError(string.Empty,"Please select atelast one category for the picture");
        //        return View(photoCreateVm);
        //    }


        //    if (ModelState.IsValid)
        //    {
        //        var result = PhotosDatabase.AddNewPhotoToDatabase(photoCreateVm,_db);

        //        if (result == DbResults.PhotoDb.OtherFailure)
        //        {
        //            ModelState.AddModelError(string.Empty, "Other failure with database saving / connection. Please try later.");                
        //            return View(photoCreateVm);
        //        }

        //        if (result == DbResults.PhotoDb.FileIsNotImage)
        //        {
        //            ModelState.AddModelError(string.Empty, "Selected file is not an image file.");
        //            return View(photoCreateVm);
        //        }

        //        if (result == DbResults.PhotoDb.NameAlreadyExist)
        //        {
        //            ModelState.AddModelError(string.Empty, "Selected image with same name already exist in database.");
        //            return View(photoCreateVm);
        //        }

        //        if (result == DbResults.PhotoDb.Success)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(photoCreateVm);
        //}


        //GET: Details
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = _db.Photos.Find(id);

            var photoVm = new AdminPhotoDetailsVm(photo);

            return View(photoVm);
        }


        //GET: Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }


        //GET: Remove
        [HttpGet]
        public ActionResult Remove(int? id)
        {
            var photoDb = _db.Photos.Find(id);

            var photoVm = AdminPhotoDeleteVm.CreateAdminPhotoDeleteVm(photoDb);

            return View(photoVm);
        }

        //POST: Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {

            try
            {
                var photo = _db.Photos.Find(id);
                _db.Photos.Remove(photo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Remove", new {id = id});
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
            base.Dispose(disposing);
        }

        //gets number of pages for index page - needs page ceiling
        private int GetNumberOfPages(IEnumerable<AdminPhotoIndexVm> query)
        {
            return (int)Math.Ceiling((double)_db.Photos.Count() / _pageSize);
        }
    }
}
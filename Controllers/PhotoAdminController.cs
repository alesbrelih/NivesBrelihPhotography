using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;

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
        public ActionResult Index(int categoryId = -1, int orderBy = -1,int page = 0)
        {

            if (Request.IsAjaxRequest())
            {
                var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex(_orderBy, _orderType, page - 1, _pageSize);
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
               
                var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex(_orderBy,_orderType,page,_pageSize);

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

        private int GetNumberOfPages(IEnumerable<AdminPhotoIndexVm> query)
        {
            return (int)Math.Ceiling((double)_db.Photos.Count() / _pageSize);
        }
    }
}
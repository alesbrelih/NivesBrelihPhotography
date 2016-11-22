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

        //albums index page
        public ActionResult Albums(int pageNumber = 0)
        {
            //ajax request
            //
            if (Request.IsAjaxRequest())
            {
                var query = db.Photos.Where(x => x.IsPhotoAlbumCover)
                    .OrderBy(x => x.Uploaded).Skip(pageNumber * 10)
                    .Take(10)
                    .Select(x => new PhotoAlbumView()
                {
                    PhotoAlbumId = x.PhotoAlbum.PhotoAlbumId,
                    AlbumName = x.PhotoAlbum.AlbumName,
                    AlbumPhotoUrl = x.PhotoUrl,
                    AlbumDate = x.PhotoAlbum.AlbumDate
                }).ToList();

                if (pageNumber == 0)
                {
                    return PartialView("_indexAlbums", query);

                }
                else
                {
                    return Json(query,JsonRequestBehavior.AllowGet);
                }
            }
            //query to find all albums and its cover photos
            else
            {
                var query = db.Photos.Where(x => x.IsPhotoAlbumCover).OrderBy(x=>x.Uploaded)
                    .Take(10).Select(x => new PhotoAlbumView()
                {
                    PhotoAlbumId = x.PhotoAlbum.PhotoAlbumId,
                    AlbumName = x.PhotoAlbum.AlbumName,
                    AlbumPhotoUrl = x.PhotoUrl,
                    AlbumDate = x.PhotoAlbum.AlbumDate
                }).ToList();
                return View(query);
            }


        }

        //all photos for albums
        public ActionResult AlbumPhotos(int? albumId = null, int pageNumber = 0)
        {
            if (Request.IsAjaxRequest())
            {
                //requesting first page with album information aswell
                if (pageNumber.Equals(0))
                {
                    var selectedAlbum = db.PhotoAlbums.First(x => x.PhotoAlbumId == albumId);
                    var albumPhotos =
                        db.Photos.Where(x => x.PhotoAlbumId == albumId).OrderBy(x => x.Uploaded)
                            .Take(10)
                            .Select(x => new PhotoView() {PhotoTitle = x.PhotoTitle, PhotoUrl = x.PhotoUrl})
                            .ToList();
                    var albumDesc = new AlbumView(selectedAlbum.PhotoAlbumId, selectedAlbum.AlbumName,
                        selectedAlbum.AlbumDate,
                        selectedAlbum.AlbumDescription);

                    return Json(new AlbumAndPhotosView(albumDesc, albumPhotos), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var albumPhotos =
                        db.Photos.Where(x => x.PhotoAlbumId == albumId).OrderBy(x => x.Uploaded)
                            .Skip(10*pageNumber)
                            .Take(10)
                            .Select(x => new PhotoView()
                            {
                                PhotoUrl = x.PhotoUrl,
                                PhotoTitle = x.PhotoTitle
                            }).ToList();
                    return Json(albumPhotos, JsonRequestBehavior.AllowGet);
                }
            }
            else                  //normal call - no ajax

            {
                //TODO: IF BLOGID IS NULL RETURN ERROR PAGE
                var selectedAlbum = db.PhotoAlbums.First(x => x.PhotoAlbumId == albumId);  //selected album

                //album photos
                var albumPhotos =
                    db.Photos.Where(x => x.PhotoAlbumId == albumId).OrderBy(x => x.Uploaded)
                        .Take(10)
                        .Select(x => new PhotoView() { PhotoTitle = x.PhotoTitle, PhotoUrl = x.PhotoUrl })
                        .ToList();

                //viewmodel with album description and album photos
                var albumDesc = new AlbumView(selectedAlbum.PhotoAlbumId, selectedAlbum.AlbumName, selectedAlbum.AlbumDate,
                    selectedAlbum.AlbumDescription);

                //return normal view

                //find blogId from previous page
                //urlrefferer returns previous address this action was accessed from
                //parsequeryString finds route values

                var previousBlogId = String.Empty; //if page was accesed not from a blog link - default value

                var previousUrl = Request.UrlReferrer;  //previous link
                if (previousUrl != null) //if was accessed from a link
                {
                    previousBlogId = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["blogId"];

                }

                //set blog id in viewBag
                ViewBag.BlogId = previousBlogId;

                //return view
                return View(new AlbumAndPhotosView(albumDesc, albumPhotos));
            }
            
        }





        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoId,PhotoTitle,PhotoText,PhotoUrl,Uploaded,CommentsEnabled")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,PhotoTitle,PhotoText,PhotoUrl,Uploaded,CommentsEnabled")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
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

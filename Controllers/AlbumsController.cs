using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;

namespace NivesBrelihPhotography.Controllers
{
    public class AlbumsController : BaseController
    {

        private NbpContext db = new NbpContext();

        // GET: Albums
        public ActionResult Index(int pageNumber = 0)
        {
            //ajax request
            //
            if (Request.IsAjaxRequest())
            {
                var query = db.AlbumCovers.OrderBy(x => x.Album.AlbumDate).Skip(pageNumber * 10)
                    .Take(10)
                    .Select(x => new PhotoAlbumView()
                    {
                        PhotoAlbumId = x.Album.PhotoAlbumId,
                        AlbumName = x.Album.AlbumName,
                        AlbumPhotoUrl = x.Photo.PhotoUrl ?? Properties.Resources.NoAlbumCoverPhoto,
                        AlbumDate = x.Album.AlbumDate
                    }).ToList();

                if (pageNumber == 0)
                {
                    return PartialView("_indexAlbums", query);

                }
                else
                {
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
            }
            //query to find all albums and its cover photos
            else
            {
                var query = db.AlbumCovers.OrderBy(x => x.Album.AlbumDate)
                    .Take(10).Select(x => new PhotoAlbumView()
                    {
                        PhotoAlbumId = x.Album.PhotoAlbumId,
                        AlbumName = x.Album.AlbumName,
                        AlbumPhotoUrl = x.Photo.PhotoUrl ?? Properties.Resources.NoAlbumCoverPhoto,
                        AlbumDate = x.Album.AlbumDate
                    }).ToList();
                return View(query);
            }

        }

        // GET: Single album
        public ActionResult Album(int? albumId = null, int pageNumber = 0)
        {
            //if no album id provided redirect to index
            if (albumId == null)
            {
                return RedirectToAction("Index");
            }

            //ajax request
            if (Request.IsAjaxRequest())
            {
                var albumPhotos =
                    db.Photos.Where(x => x.PhotoAlbumId == albumId).OrderBy(x => x.Uploaded)
                        .Skip(10 * pageNumber)
                        .Take(10)
                        .Select(x => new PhotoView()
                        {
                            PhotoUrl = x.PhotoUrl,
                            PhotoTitle = x.PhotoTitle
                        }).ToList();
                return Json(albumPhotos, JsonRequestBehavior.AllowGet);
            }
            else                  //normal call - no ajax

            {

                var selectedAlbum = db.PhotoAlbums.First(x => x.PhotoAlbumId == albumId);  //selected album
                var coverPhoto = db.AlbumCovers.First(x => x.AlbumId == albumId).Photo;  //album cover photo

                //case no cover photo set
                var coverPhotoUrl = coverPhoto == null ? Properties.Resources.NoAlbumCoverPhoto : coverPhoto.PhotoUrl;


                //if album couldn't be found from id, redirect to index page
                if (selectedAlbum == null)
                {
                    return RedirectToAction("Index");
                }

                //album photos
                var albumPhotos =
                    db.Photos.Where(x => x.PhotoAlbumId == albumId).OrderBy(x => x.Uploaded)
                        .Take(10)
                        .Select(x => new PhotoView() { PhotoTitle = x.PhotoTitle, PhotoUrl = x.PhotoUrl })
                        .ToList();

                //viewmodel with album description and album photos
                var albumDesc = new AlbumView(selectedAlbum.PhotoAlbumId, selectedAlbum.AlbumName, selectedAlbum.AlbumDate,
                    selectedAlbum.AlbumDescription,coverPhotoUrl);

                //return normal view

                //return view
                return View(new AlbumAndPhotosView(albumDesc, albumPhotos));
            }

        }

        //dispose controller
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
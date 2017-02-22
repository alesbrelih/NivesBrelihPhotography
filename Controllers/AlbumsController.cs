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

    [RoutePrefix("Projects")]
    public class AlbumsController : BaseController
    {

        private NbpContext db = new NbpContext();

        // GET: Albums
        [Route("{pageNumber=0}")]
        public async Task<ActionResult> Index(int pageNumber = 0)
        {
            //ajax request
            //
            if (Request.IsAjaxRequest())
            {
                var query = await db.AlbumCovers.OrderBy(x => x.Album.AlbumDate).Skip(pageNumber * 10)
                    .Take(10)
                    .Select(x => new PhotoAlbumView()
                    {
                        PhotoAlbumId = x.Album.PhotoAlbumId,
                        AlbumName = x.Album.AlbumName,
                        AlbumPhotoUrl = x.Photo.PhotoUrl ?? Properties.Resources.NoAlbumCoverPhoto,
                        AlbumDate = x.Album.AlbumDate
                    }).ToListAsync();

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
                var query = await db.AlbumCovers.OrderBy(x => x.Album.AlbumDate)
                    .Take(10).Select(x => new PhotoAlbumView()
                    {
                        PhotoAlbumId = x.Album.PhotoAlbumId,
                        AlbumName = x.Album.AlbumName,
                        AlbumPhotoUrl = x.Photo.PhotoUrl ?? Properties.Resources.NoAlbumCoverPhoto,
                        AlbumDate = x.Album.AlbumDate
                    }).ToListAsync();
                return View(query);
            }

        }

        // GET: Single album
        [Route("Project/{id=null}/{pageNumber=0}")]
        public async Task<ActionResult> Album(int? id = null, int pageNumber = 0)
        {
            //if no album id provided redirect to index
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            //ajax request
            if (Request.IsAjaxRequest())
            {
                var albumPhotos =
                    await db.Photos.Where(x => x.PhotoAlbumId == id).OrderBy(x => x.Uploaded)
                        .Skip(10 * pageNumber)
                        .Take(10)
                        .Select(x => new PhotoViewWithAlbumId()
                        {
                            AlbumId = id,
                            PhotoUrl = x.PhotoUrl,
                            PhotoTitle = x.PhotoTitle
                        }).ToListAsync();
                return Json(albumPhotos, JsonRequestBehavior.AllowGet);
            }
            else                  //normal call - no ajax

            {

                var selectedAlbum = await db.PhotoAlbums.FirstAsync(x => x.PhotoAlbumId == id);  //selected album

                //if no album was found in db, go to index
                if (selectedAlbum == null)
                {
                    return RedirectToAction("Index");
                }

                //get cover photo
                var coverPhoto = db.AlbumCovers.First(x => x.AlbumId == id).Photo;  //album cover photo

                //case no cover photo set
                var coverPhotoUrl = coverPhoto == null ? Properties.Resources.NoAlbumCoverPhoto : coverPhoto.PhotoUrl;



                //album photos
                var albumPhotos =
                    await db.Photos.Where(x => x.PhotoAlbumId == id).OrderBy(x => x.Uploaded)
                        .Take(10)
                        .Select(x => new PhotoView() { PhotoTitle = x.PhotoTitle, PhotoUrl = x.PhotoUrl })
                        .ToListAsync();

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
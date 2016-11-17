using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class PhotosDatabase
    {

        public static IEnumerable<AdminPhotoIndexVm> ReturnPhotosForAdminPhotoIndex(int orderBy, bool ascending,int page, int pagesize,NbpContext _db)
        {

            switch (orderBy)
            {

                case 1:
                    if (ascending)
                    {
                        return _db.Photos.OrderBy(x => x.PhotoTitle).Skip(page*pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                    else
                    {
                        return _db.Photos.OrderByDescending(x => x.PhotoTitle).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                case 2:
                    if (ascending)
                    {
                        return _db.Photos.OrderBy(x => x.PhotoAlbum.AlbumName).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                    else
                    {
                        return _db.Photos.OrderByDescending(x => x.PhotoAlbum.AlbumName).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                case 3:
                    if (ascending)
                    {
                        return _db.Photos.OrderBy(x => x.IsOnFrontPage).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                    else
                    {
                        return _db.Photos.OrderByDescending(x => x.IsOnFrontPage).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                case 4:
                    if (ascending)
                    {
                        return _db.Photos.OrderBy(x => x.Uploaded).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                    else
                    {
                        return _db.Photos.OrderByDescending(x => x.Uploaded).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
                        {
                            PhotoTitle = x.PhotoTitle,
                            Album = x.PhotoAlbum.AlbumName,
                            OnPortfolio = x.IsOnFrontPage,
                            PhotoId = x.PhotoId,
                            PhotoUrl = x.PhotoUrl,
                            Uploaded = x.Uploaded
                        }).ToList();
                    }
                    
            }
            return new List<AdminPhotoIndexVm>();
        }

        //adds adminphotocreate vm to db and converts it before
        public static DbResults.PhotoDb AddNewPhotoToDatabase(AdminPhotoCreateVm photoCreateVm,NbpContext _db)
        {


            //check if image
            if (!HttpPostedFileBaseExtensions.IsImage(photoCreateVm.PhotoFile)) //selected file isnt img
            {
                
                return DbResults.PhotoDb.FileIsNotImage;

            }

            //convert to db model
            var photo = photoCreateVm.CovertToDbModel();

            //check if img with same name exists in DB
            if (CheckIfPathAlreadyExist(photo.PhotoUrl,_db))
            {
                return DbResults.PhotoDb.NameAlreadyExist;
            }

            try
            {

                _db.Photos.Add(photo);

                var fileName = Path.GetFileName(photoCreateVm.PhotoFile.FileName);
                var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

                photoCreateVm.PhotoFile.SaveAs(imagePath);

                _db.SaveChanges();

                return DbResults.PhotoDb.Success;

            }
            catch (Exception)
            {

                return DbResults.PhotoDb.OtherFailure;
            }


            
        }

        //returns true if path already exist in db
        private static bool CheckIfPathAlreadyExist(string path, NbpContext _db)
        {
            //find all images with same path
            var images =
                _db.Photos.Where(x => x.PhotoUrl.Equals(path, StringComparison.InvariantCultureIgnoreCase)).ToList();

            return images.Count != 0;
        }

        //initialize db
        public static bool InitializeDb(NbpContext db)
        {
            //try opening connection to db
            try
            {
                db = new NbpContext();
                return true;
            }
            catch (Exception) //return false else
            {
                return false;
            }
        }

        //dispose db
        public static void DisposeDb(NbpContext db)
        {
            if (db != null) //close and dispose db
            {
                db.Dispose();
                db = null;
            }
        }
    }
}

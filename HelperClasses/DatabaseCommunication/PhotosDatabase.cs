using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class PhotosDatabase
    {
        private static NbpContext _db = new NbpContext();

        public static IEnumerable<AdminPhotoIndexVm> ReturnPhotosForAdminPhotoIndex(int orderBy, bool ascending,int page, int pagesize)
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
        public static void AddNewPhotoToDatabase(AdminPhotoCreateVm photoCreateVm)
        {
            var photo = AdminPhotoCreateVm.CovertToDbModel(photoCreateVm);
        }
    }
}

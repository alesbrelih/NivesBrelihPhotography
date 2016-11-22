﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.Models.PhotoModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class PhotosDatabase
    {
        //returns photos for admin photo index
        public static IEnumerable<AdminPhotoIndexVm> ReturnPhotosForAdminPhotoIndex(int page, int pagesize,NbpContext _db)
        {

            try
            {
                var photos = _db.Photos.OrderBy(x=>x.Uploaded).Skip(page*pagesize).Take(pagesize).Select(x=>new AdminPhotoIndexVm()
                {
                    PhotoTitle = x.PhotoTitle,
                    Album = x.PhotoAlbum.AlbumName,
                    OnPortfolio = x.IsOnFrontPage,
                    PhotoId = x.PhotoId,
                    PhotoUrl = x.PhotoUrl,
                    Uploaded = x.Uploaded
                }).ToList();

                if (!photos.Any())
                {
                    throw new Exception("No more photos");
                }

                return photos;
            }
            catch (Exception err)
            {
                //throw err if any errors
                throw new Exception(err.Message);
            }

            //switch (orderBy)
            //{

            //    case 1:
            //        if (ascending)
            //        {
            //            return _db.Photos.OrderBy(x => x.PhotoTitle).Skip(page*pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //        else
            //        {
            //            return _db.Photos.OrderByDescending(x => x.PhotoTitle).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //    case 2:
            //        if (ascending)
            //        {
            //            return _db.Photos.OrderBy(x => x.PhotoAlbum.AlbumName).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //        else
            //        {
            //            return _db.Photos.OrderByDescending(x => x.PhotoAlbum.AlbumName).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //    case 3:
            //        if (ascending)
            //        {
            //            return _db.Photos.OrderBy(x => x.IsOnFrontPage).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //        else
            //        {
            //            return _db.Photos.OrderByDescending(x => x.IsOnFrontPage).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //    case 4:
            //        if (ascending)
            //        {
            //            return _db.Photos.OrderBy(x => x.Uploaded).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }
            //        else
            //        {
            //            return _db.Photos.OrderByDescending(x => x.Uploaded).Skip(page * pagesize).Take(pagesize).Select(x => new AdminPhotoIndexVm()
            //            {
            //                PhotoTitle = x.PhotoTitle,
            //                Album = x.PhotoAlbum.AlbumName,
            //                OnPortfolio = x.IsOnFrontPage,
            //                PhotoId = x.PhotoId,
            //                PhotoUrl = x.PhotoUrl,
            //                Uploaded = x.Uploaded
            //            }).ToList();
            //        }

            //}
            //return new List<AdminPhotoIndexVm>();
        }

        //adds adminphotocreate vm to db and converts it before
        public static DbResults.PhotoDb AddNewPhotoToDatabase(AdminPhotoCreateVm photoCreateVm,MultipartFileData file,NbpContext _db)
        {


            //check if image
            //if (!HttpPostedFileBaseExtensions.IsImage(photoCreateVm.PhotoFile)) //selected file isnt img
            //{

            //    return DbResults.PhotoDb.FileIsNotImage;

            //}

            //extract file name
            string fileName = file.Headers.ContentDisposition.FileName;

            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
            {
                fileName = fileName.Trim('"');
            }
            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
            {
                fileName = Path.GetFileName(fileName);
            }

            //set filepath to where file will be saved
            var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

            //dynamic path in db
            photoCreateVm.PhotoUrl = "/Images/Photos/" + fileName;

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


                #region previousCode
                //var fileName = Path.GetFileName(photoCreateVm.PhotoFile.FileName);
                //var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

                //photoCreateVm.PhotoFile.SaveAs(imagePath);
                #endregion

                //move file from temp folder to main folder, ovveride if needed
                File.Copy(file.LocalFileName, imagePath,true);

                //save changes in db
                _db.SaveChanges();

                if (photo.IsPhotoAlbumCover) //if new album cover
                {
                    try
                    {
                        var previousAlbumPhotoCover = _db.Photos.First(x=>x.PhotoAlbumId == photo.PhotoAlbumId && x.IsPhotoAlbumCover == true); //find photo that was previous album cover
                        previousAlbumPhotoCover.IsPhotoAlbumCover = false; //set album cover photo id to this one
                        _db.Entry(previousAlbumPhotoCover).State = EntityState.Modified; //notify EF6 that entry was changed

                        _db.SaveChanges(); //save changes
                    }
                    catch(Exception ex)
                    {
                        return DbResults.PhotoDb.ErrorSettingAlbumCoverPhoto;
                    }
                    
                }

                return DbResults.PhotoDb.Success;

            }
            catch (Exception ex)
            {

                return DbResults.PhotoDb.OtherFailure;
            }


            
        }

        //deletes photo from db
        public static void DeletePhotoFromDatabase(int? id, NbpContext _db)
        {
            //get photo from database
            Photo photo = _db.Photos.Find(id);
            if (photo == null)
            {
                //photo wasnt found
                throw new Exception("Photo couldn't be found in database. Already deleted?");
            }
            else
            {
                //image was found, so try to delete it
                try
                {
                    //Set entry state to deleted
                    _db.Entry(photo).State = EntityState.Deleted;

                    //save changes to db
                    _db.SaveChanges();

                }

                //error modifying db
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

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

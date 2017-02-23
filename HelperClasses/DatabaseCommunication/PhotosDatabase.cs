using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ImageResizerLibrary;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.Models.PhotoModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;
using ListExtensions = WebGrease.Css.Extensions.ListExtensions;


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
                    Uploaded = x.Uploaded,
                    HomeCarousel = x.HomeCarousel,
                    Orientation = x.Orientation
                }).ToList();

                if (!photos.Any())
                {
                    throw new Exception("end");
                }

                return photos;
            }
            catch (Exception err)
            {
                //throw err if any errors
                throw new Exception(err.Message);
            }

        }

        //adds adminphotocreate vm to db and converts it before
        public static AdminPhotoIndexVm AddNewPhotoToDatabase(AdminPhotoCreateVm photoCreateVm,MultipartFileData file,NbpContext _db)
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
            var directoryPath = HttpContext.Current.Server.MapPath("~/Images/Photos");

            //dynamic path in db
            photoCreateVm.PhotoUrl = fileName;

            //convert to db model
            var photo = photoCreateVm.CovertToDbModel();

            //check if img with same name exists in DB
            if (CheckIfPathAlreadyExist(photo.PhotoUrl,_db))
            {
                //Delete uploaded file bodypart
                File.Delete(file.LocalFileName);

                //throw exception
                throw new Exception("Photo with the same path already exists");
            }
            photo.Orientation = ImageResizer.GetOrientation(file.LocalFileName); //get photo orientation     
            try
            {

                _db.Photos.Add(photo);


                #region previousCode
                //var fileName = Path.GetFileName(photoCreateVm.PhotoFile.FileName);
                //var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

                //photoCreateVm.PhotoFile.SaveAs(imagePath);
                #endregion

                //move file from temp folder to main folder, ovveride if needed
                //File.Move(file.LocalFileName, imagePath);
                using (var imgSizer = new ImageResizer(file.LocalFileName))
                {
                    imgSizer.Resize();
                    imgSizer.SaveImages(directoryPath, fileName);
                }
                

                File.Delete(file.LocalFileName);

                //save changes in db
                _db.SaveChanges();

                if (photoCreateVm.IsAlbumCover) //if new album cover
                {
                    try
                    {
                        var albumCover = _db.AlbumCovers.First(x=>x.AlbumId == photo.PhotoAlbumId); //find photo that was previous album cover
                        albumCover.PhotoId = photo.PhotoId;
                        _db.Entry(albumCover).State = EntityState.Modified; //notify EF6 that entry was changed

                        _db.SaveChanges(); //save changes
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Error setting photo as album photo.");
                    }
                    
                }


                //returns photo view model to be added to list
                var returnPhoto = new AdminPhotoIndexVm()
                {
                    PhotoTitle = photo.PhotoTitle,
                    OnPortfolio = photo.IsOnFrontPage,
                    PhotoId = photo.PhotoId,
                    PhotoUrl = photo.PhotoUrl,
                    Uploaded = photo.Uploaded,
                    HomeCarousel = photo.HomeCarousel
                };
                
                //add album name if not null
                if (photo.PhotoAlbumId != null)
                {
                    var albumDb = _db.PhotoAlbums.Find(photo.PhotoAlbumId);
                    returnPhoto.Album = albumDb.AlbumName;
                }

                //return name
                return returnPhoto;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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

            //image was found, so try to delete it
            try
            {
                //Set entry state to deleted
                _db.Entry(photo).State = EntityState.Deleted;
                
                //delete file from server?
                FileManipulate.DeletePhoto(HttpContext.Current.Server.MapPath("~/Images/Photos"), photo.PhotoUrl);

                //save changes to db
                _db.SaveChanges();

            }

            //error modifying db
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }     
        }

        //returns single photo edit
        public static AdminPhotoEditVm ReturnSinglePhotoForEdit(int? id, NbpContext db)
        {
            var photoVm = new AdminPhotoEditVm();

            try
            {
                var photoDb = db.Photos.Find(id);

                var isAlbumCover =
                    db.AlbumCovers.Count(x => x.AlbumId == photoDb.PhotoAlbumId && x.PhotoId == photoDb.PhotoId) > 0;

                photoVm.ChangeProps(photoDb,isAlbumCover);

                return photoVm;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        //async task for changing photo in db
        public static async Task<bool> EditPhotoInDatabase(AdminPhotoEditVm photo,NbpContext db)
        {
            var photoDb = await db.Photos.FindAsync(photo.Id);
            photoDb.PhotoTitle = photo.PhotoTitle;
            
            //find if unedited was album cover to some album
            var albumCoverQuery =
                await
                    db.AlbumCovers.Where(x => x.AlbumId == photoDb.PhotoAlbumId && x.PhotoId == photoDb.PhotoId)
                        .ToListAsync();

            // ------ flag if it was a cover ----- //
            var wasAlbumCover = albumCoverQuery.Count() != 0;

            #region handling album change and album cover change
            // ---------- handling album changing ----------- //
            if (photoDb.PhotoAlbumId.ToString() != photo.AlbumId) //if album id is different
            {
                if (wasAlbumCover) //and it was a cover for previus album
                {
                    var albumCoverEntry = albumCoverQuery[0]; //reset previous album cover
                    albumCoverEntry.PhotoId = null;

                    if (photo.IsAlbumCover)
                    {
                        albumCoverEntry.PhotoId = photo.Id;
                    }

                }

                //no album selected
                if (photo.AlbumId == "-1" || photo.AlbumId == "0")
                {
                    photoDb.PhotoAlbumId = null;
                }
                else
                {
                    photoDb.PhotoAlbumId = int.Parse(photo.AlbumId); //change album id
                }
               
            }
            else
            {
                if (photo.IsAlbumCover != wasAlbumCover)
                {
                    if (photo.IsAlbumCover)
                    {
                        //if it becomes album cover then set it as album cover
                        var query = await db.AlbumCovers.SingleOrDefaultAsync(x => x.AlbumId.ToString() == photo.AlbumId);
                        query.PhotoId = photoDb.PhotoId;
                    }
                    else
                    {
                        var albumCoverEntry = albumCoverQuery[0]; //reset previous album cover
                        albumCoverEntry.PhotoId = null;
                    }
                }
            }
            #endregion

            // handle portfolio change //

            photoDb.IsOnFrontPage = photo.IsOnPortfolio;

            //handle home carousel change
            photoDb.HomeCarousel = photo.HomeCarousel;

            ///////////////////////////////////////////
            // -------- handle categories ---------- //
            //////////////////////////////////////////
            
            // 1. get those categories that already existed before

            var list = photoDb.Categories.Where(x => photo.PhotoCategories.Contains(x.CategoryId.ToString())).Select(x=>x.CategoryId.ToString()).ToList();

            // 2. foreach na photoDB categories, if not in photo.categories, entrystate deleted

            var removedCategories = photoDb.Categories.Where(x => !photo.PhotoCategories.Contains(x.CategoryId.ToString())).ToList();

            //linq doesnt support foreach
            removedCategories.ForEach(x =>
            {
                db.Entry(x).State = EntityState.Deleted;
            });


            // 3. add those categories that didnt match before
            photo.PhotoCategories.Where(x => !list.Contains(x)).ToList().ForEach(x => photoDb.Categories.Add(new PhotoCategory()
            {
                CategoryId = int.Parse(x),
                PhotoId = photo.Id
            }));


            // ------ SAVE CHANGES TO DB ----- //
            await db.SaveChangesAsync();
            return true;
        }
    }
}

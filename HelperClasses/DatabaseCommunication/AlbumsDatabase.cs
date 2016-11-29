using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels.Album;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class AlbumsDatabase
    {


        //returns select list to select album on photo create/edit
        public static async Task<ICollection<AdminAlbumIndexVm>> ReturnAlbumsForSelectList(NbpContext _db)
        {
            //selectlist items from db
            var albums = new List<AdminAlbumIndexVm>() {};

            //get albums
            await _db.PhotoAlbums.OrderBy(x => x.AlbumName).ForEachAsync(x =>
            {
                albums.Add(new AdminAlbumIndexVm()
                {
                    Id = x.PhotoAlbumId,
                    Name = x.AlbumName,
                    Description = x.AlbumDescription,
                    Date = x.AlbumDate,
                });

            });

            //add albumCover url
            foreach (var album in albums)
            {
                var albumCover = await _db.AlbumCovers.FirstOrDefaultAsync(x => x.AlbumId == album.Id);
                var photo = albumCover.Photo;
                album.AlbumCoverUrl = (photo == null || photo.PhotoUrl == null)
                    ? Properties.Resources.NoAlbumCoverPhoto
                    : photo.PhotoUrl;

            }


            //Return albums
            return albums;
        }

        //get album data from album id
        public static async Task<AdminAlbumModifyVm> GetAlbumByIdAsync(int id, NbpContext db)
        {
            var albumDb = await db.PhotoAlbums.FindAsync(id);
            //check if album with that id was returned
            if (albumDb == null)
            {
                throw new Exception("No album was found for selected id. Check your URL or try again later.");
            }

            var albumVm = new AdminAlbumModifyVm()
            {
                Id = albumDb.PhotoAlbumId,
                Name = albumDb.AlbumName,
                Description = albumDb.AlbumDescription,
            };

            // get album photos ids
            await db.Photos.Where(x => x.PhotoAlbumId == albumDb.PhotoAlbumId).ForEachAsync(x =>
            {
                albumVm.Photos.Add(x.PhotoId.ToString());
            });

            //get cover photo id
            var coverPhoto = await db.AlbumCovers.SingleOrDefaultAsync(x => x.AlbumId == albumDb.PhotoAlbumId);
            albumVm.CoverPhotoId = coverPhoto.Photo == null ? "" : coverPhoto.PhotoId.ToString();

            //return album vm 
            return albumVm;

        }

        //deletes album from db
        public static async Task DeleteAlbumAsync(int id, bool photos, NbpContext db)
        {
            //get album and delete it
            var album = await db.PhotoAlbums.FindAsync(id);
            db.Entry(album).State = EntityState.Deleted;

            //delete album cover entry
            var albumCover = await db.AlbumCovers.FirstOrDefaultAsync(x => x.AlbumId == id);
            db.Entry(albumCover).State = EntityState.Deleted;



            //if user wants also album photos deleted
            if (photos)
            {
                //all album photos
                var albumPhotos = await db.Photos.Where(x => x.PhotoAlbumId == id).ToListAsync();

                //check if array of photos exists and its count isnt null
                if (albumPhotos != null && albumPhotos.Count > 0)
                {
                    foreach (var photo in albumPhotos)
                    {
                        //delete every photo
                        db.Entry(photo).State = EntityState.Deleted;
                    }
                }

            }


            //save db changes
            await db.SaveChangesAsync();

        }

        //creates and uploads album
        public static async Task<AdminAlbumIndexVm> CreateAlbumAsync(AdminAlbumCreateVm album, NbpContext db)
        {

            //1. create db model
            var albumDb = new PhotoAlbum()
            {
                AlbumDate = DateTime.Now,
                AlbumName = album.Name,
                AlbumDescription = album.Description
            };

            //save db model so we get id
            db.PhotoAlbums.Add(albumDb);

            await db.SaveChangesAsync();

            // 2. create album cover entry
            int photoCoverId = -1; //parsing string - prevent injection
            int.TryParse(album.AlbumCover, out photoCoverId);

            //get photo from db/ valid id will return photo, if not valid it will be null which is fine because photoid is
            //nullable for albumcover in db
            var photoCoverDb = await db.Photos.FindAsync(photoCoverId);

            //set album cover
            var albumCover = new AlbumCover()
            {
                AlbumId = albumDb.PhotoAlbumId,
                Photo = photoCoverDb
            };
            db.AlbumCovers.Add(albumCover);

            // 3. set album photos
            foreach (var photo in album.Photos)
            {
                //try to convert number to in (prevent injection)
                int number;
                bool result = int.TryParse(photo, out number);
                if (result)
                {
                    //if it is a number get photo and set album id to selected one
                    var photoDb = await db.Photos.FindAsync(photo);
                    photoDb.PhotoAlbumId = albumDb.PhotoAlbumId;
                    db.Entry(photoDb).State = EntityState.Modified;
                }

            }

            // 4. Save all changes
            await db.SaveChangesAsync();


            //get photo url for vm
            var photoUrl = await db.AlbumCovers.FirstOrDefaultAsync(x => x.AlbumId == albumDb.PhotoAlbumId);
            var albumCoverPhoto = photoUrl.Photo;
            var albumCoverPhotoUrl = (albumCoverPhoto == null || albumCoverPhoto.PhotoUrl == null)
                ? Properties.Resources.NoAlbumCoverPhoto
                : albumCoverPhoto.PhotoUrl;


            //return vm object to add to list
            return new AdminAlbumIndexVm()
            {
                Id = albumDb.PhotoAlbumId,
                Date = albumDb.AlbumDate,
                Name = albumDb.AlbumName,
                Description = albumDb.AlbumDescription,
                AlbumCoverUrl = albumCoverPhotoUrl
            };


        }

        //edits album from db
        public static async Task EditAlbumAsync(AdminAlbumModifyVm album, NbpContext db)
        {
            //get current album data from db
            var albumDb = await db.PhotoAlbums.FindAsync(album.Id);
            //check if there was a result from db
            if (albumDb == null)
            {
                throw new Exception("Album to modify wasn't found in the database");
            }


            // 1. modify static data
            albumDb.AlbumName = album.Name;
            albumDb.AlbumDescription = album.Description;
            
            // 2. set cover photo
            //check if album.cover is not null or empty //because album has to have cover photo when editing it
            if (!string.IsNullOrEmpty(album.CoverPhotoId))
            {
                int photoId;
                //check if cover photo id is actually an integer
                if (int.TryParse(album.CoverPhotoId, out photoId))
                {

                    var coverPhotoDb = await db.Photos.FindAsync(photoId);
                    //check if int is a valid photo id key
                    if (coverPhotoDb != null)
                    {
                        var albumCoverEntry =
                            await db.AlbumCovers.SingleOrDefaultAsync(x => x.AlbumId == albumDb.PhotoAlbumId);
                        albumCoverEntry.Photo = coverPhotoDb;
                        db.Entry(albumCoverEntry).State = EntityState.Modified;

                    }
                    else
                    {
                        throw new Exception("Selected cover photo is invalid");
                    }
                }
                else
                {
                    throw new Exception("Selected cover photo is invalid");
                }

            }
            else
            {
                throw new Exception("Selected cover photo is invalid");
            }


            // 3. set album photos

            //previous album photos id - strings
            var previousAlbumPhotos = albumDb.AlbumPhotos.Select(x => x.PhotoId.ToString()).ToList();

            //add those that didnt exist before
            foreach (var newPhoto in album.Photos)
            {
                if (!previousAlbumPhotos.Contains(newPhoto))
                {
                    //find photo in db and set its photoalbum id to current
                    var photoDb = await db.Photos.FindAsync(int.Parse(newPhoto));
                    if (photoDb != null)
                    {
                        photoDb.PhotoAlbumId = albumDb.PhotoAlbumId;
                        db.Entry(photoDb).State = EntityState.Modified;
                    }
                    
                }
            }

            //remove those that were removed
            foreach (var prevPhoto in previousAlbumPhotos)
            {
                // if photo that was in prev album photos doesnt exist in current one then find it and set
                //photoalbum id to null
                if (!album.Photos.Contains(prevPhoto))
                {
                    var photoDb = await db.Photos.FindAsync(int.Parse(prevPhoto));
                    if (photoDb != null)
                    {
                        photoDb.PhotoAlbumId = null;
                        db.Entry(photoDb).State = EntityState.Modified;
                    }
                    
                }
            }


            //save changes
            await db.SaveChangesAsync();
        }

    }
}

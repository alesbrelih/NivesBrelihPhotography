using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.AboutModels;
using NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels;
using System.Collections;
using System.Drawing;
using ImageResizerLibrary;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class ProfileDatabase
    {
        //gets profile information

        public static async Task<Profile> GetProfile(NbpContext db)
        {
            var profile = await db.Profile.FirstOrDefaultAsync();
            return profile;
        }

        //edits profile information
        public static async Task EditProfile(AdminAboutProfileInformation profile, MultipartFileData file, NbpContext db)
        {
            //find profile info in db and check if not null
            var profileDb = await db.Profile.FindAsync(profile.Id);
            if (profileDb == null)
            {
                throw new Exception("Error retrieving profile information from database to change it.");
            }

            //change information
            profileDb.Name = profile.Name;
            profileDb.Lastname = profile.Lastname;
            profileDb.About = profile.About;
            profileDb.ContactPhone = profile.Phone;
            profileDb.ContactEmail = profile.Email;

            //change file if it was changed
            if (file != null)
            {
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
                var directory = HttpContext.Current.Server.MapPath("~/Images");
                //var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

                //current photo
                string currentPhotoPath = HttpContext.Current.Server.MapPath(profileDb.ProfilePicture);

                //try to delete previous if exist
                if (File.Exists(currentPhotoPath))
                {
                    File.Delete(currentPhotoPath);
                }

                //save new photo
                using (var newPhoto = new ImageResizer(file.LocalFileName))
                {
                    newPhoto.Resize();
                    newPhoto.SaveProfileImage(directory,fileName);
                }
                

                //dynamic path in db
                profileDb.ProfilePicture = "/Images/Profile/" + fileName;

            }

            //set entity state to modified
            db.Entry(profileDb).State = EntityState.Modified;

            await db.SaveChangesAsync();

        }

        //gets all social links
        public static async Task<ICollection<ProfileLink>> GetSocialLinks(NbpContext db)
        {
            
            var query = await db.ProfileLinks.ToListAsync();

            return query;
        }

        //update selected social link
        public static async Task UpdateSocialLink(AdminAboutProfileLinkUpdate socialLink,NbpContext db)
        {
            //get selected social link db model
            var socialDb = await db.ProfileLinks.FindAsync(socialLink.ProfileLinkId);


            //change properties
            socialDb.LinkDescription = socialLink.LinkDescription;
            socialDb.LinkUrl = socialLink.LinkUrl;
            socialDb.ShownOnProfile = socialLink.ShownOnProfile;

            //set entity state
            db.Entry(socialDb).State = EntityState.Modified;

            //save changes async
            await db.SaveChangesAsync();
        }


        //gets reviews
        public static async Task<ICollection<PhotoShootReview>> GetReviews(NbpContext db)
        {
            var reviews = await db.PhotoShootReviews.ToListAsync();
            return reviews;
        }

        //adds review
        public static async Task<PhotoShootReview> AddReview(AdminPhotoShootReviewCreate review, NbpContext db)
        {
            //create review db model
            var reviewDb = new PhotoShootReview()
            {
                Review = review.Review,
                ReviewerName = review.ReviewerName
            };

            //add review to db
            db.PhotoShootReviews.Add(reviewDb);

            //save changes async
            await db.SaveChangesAsync();

            //return created object
            return reviewDb;

        }

        //updates review
        public static async Task UpdateReview(PhotoShootReview review, NbpContext db)
        {
            //get review in db
            var reviewDb = await db.PhotoShootReviews.FindAsync(review.PhotoShootReviewId);

            //update data
            reviewDb.Review = review.Review;
            reviewDb.ReviewerName = review.ReviewerName;

            //notify EF6 that it was modified
            db.Entry(reviewDb).State = EntityState.Modified;

            //save changes async
            await db.SaveChangesAsync();

        }

        //remove review
        public static async Task RemoveReview(int id, NbpContext db)
        {
            //find entry in db
            var reviewDb = await db.PhotoShootReviews.FindAsync(id);

            //set it was deleted
            db.Entry(reviewDb).State = EntityState.Deleted;

            //save changes async
            await db.SaveChangesAsync();

        }


        // --- REFERENCES -- //

        //get references
        public static async Task<ICollection<AdminAboutReferencesListItem>> GetReferences(NbpContext db)
        {
            //gets references from db
            var query = await db.References.Select(x => new AdminAboutReferencesListItem()
            {
                Id = x.ReferenceId,
                Title = x.ReferenceTitle,
                Description = x.ReferenceDescription

            }).ToListAsync();

            //return query
            return query;

        }

        //delete reference
        public static async Task DeleteReference(int id, NbpContext db)
        {
            // get reference from db
            var referenceDb = await db.References.FindAsync(id);

            db.Entry(referenceDb).State = EntityState.Deleted;

            await db.SaveChangesAsync();

        }

        //add reference
        public static async Task AddReference(AdminAboutReferenceModify reference, NbpContext db)
        {
            //new reference db model
            var referenceDb = new Reference()
            {
                ReferenceTitle = reference.Title,
                ReferenceDescription = reference.Description
            };

            //if reference contains reference photos
            if (reference.ReferencePhotos.Any())
            {
                referenceDb.Photos = new List<ReferencePhoto>();

                //set reference phtos
                foreach (var refPhoto in reference.ReferencePhotos)
                {
                    int photoId;
                    bool result = int.TryParse(refPhoto, out photoId); //try to convert string to valid int
                    if (result && (photoId > 0))
                    {
                        referenceDb.Photos.Add(new ReferencePhoto()
                        {
                            PhotoId = photoId,
                            Reference = referenceDb

                        });
                    }

                }
            }

            

            //add new reference to db
            db.References.Add(referenceDb);

            //await save changes
            await db.SaveChangesAsync();



        }

        //edit reference
        public static async Task EditReference(AdminAboutReferenceModify reference, NbpContext db)
        {
            //get reference from db
            var referenceDb = await db.References.FindAsync(reference.Id);
            if (referenceDb == null)
            {
                throw new Exception("Reference could not be found in database.");
            }

            //set data
            referenceDb.ReferenceTitle = reference.Title;
            referenceDb.ReferenceDescription = reference.Description;

            //set photos
            //get previous photos 
            var previousPhotos = referenceDb.Photos.Select(x => x.PhotoId.ToString());

            //delete those which were removed
            foreach (var refPhoto in referenceDb.Photos)
            {
                if (!reference.ReferencePhotos.Contains(refPhoto.PhotoId.ToString()))
                {
                    db.Entry(refPhoto).State = EntityState.Deleted;
                }
            }

            //add new
            reference.ReferencePhotos.Where(x=>!previousPhotos.Contains(x)).ToList().ForEach(x=>referenceDb.Photos.Add(new ReferencePhoto()
            {
                PhotoId = int.Parse(x),
                ReferenceId = referenceDb.ReferenceId
            }));

            await db.SaveChangesAsync();


        }

        //get single reference
        public static async Task<AdminAboutReferenceModify> GetReference(int id, NbpContext db)
        {
            //get reference in db
            var referenceDb = await db.References.FindAsync(id);

            //set reference view model
            var viewModelReference = new AdminAboutReferenceModify()
            {
                Id = referenceDb.ReferenceId,
                Title = referenceDb.ReferenceTitle,
                Description = referenceDb.ReferenceDescription
            };

            //add ids of all reference photos
            foreach (var refPhoto in referenceDb.Photos)
            {
                viewModelReference.ReferencePhotos.Add(refPhoto.PhotoId.ToString());
            }


            return viewModelReference;
        }
    }
}

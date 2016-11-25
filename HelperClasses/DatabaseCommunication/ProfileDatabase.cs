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
                var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Photos"), fileName);

                //dynamic path in db
                profileDb.ProfilePicture = "/Images/Photos/" + fileName;

                //move file from temp folder to main folder, ovveride if needed
                File.Copy(file.LocalFileName, imagePath, true);
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

    }
}

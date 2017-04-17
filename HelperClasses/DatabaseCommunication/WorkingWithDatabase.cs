using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ImageResizerLibrary;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.AboutModels;
using NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class WorkingWithDatabase
    {
        /// <summary>
        /// Get all working withs
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task<ICollection<WorkingWith>> GetAll(NbpContext db)
        {
            var workingWiths = await db.WorkingWiths.ToListAsync();
            return workingWiths;
        }

        /// <summary>
        /// Get single working with
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="db"> dbcontext </param>
        /// <returns></returns>
        public static async Task<AdminAboutWorkingWithModify> GetSingle(int? id, NbpContext db)
        {
            var workingWithDb = await db.WorkingWiths.FindAsync(id);

            if (workingWithDb == null)
            {
                throw new Exception("Such id was not found in DB");
            }

            var workingWithVm = new AdminAboutWorkingWithModify()
            {
                Id = workingWithDb.WorkingWithId,
                Title = workingWithDb.Title,
                Description = workingWithDb.Description,
                Importance =  workingWithDb.Importance,
                Link = workingWithDb.Link,
                LinkText = workingWithDb.LinkText,
                PhotoUrl = workingWithDb.PhotoUrl
            };

            return workingWithVm;


        }

        //public static async Task<AdminAboutWorkingWithModify> GetSingle(int id, NbpContext db)
        //{
        //    //var workingWith = 
        //}

        /// <summary>
        /// Add new working with
        /// </summary>
        /// <param name="workingWithVm"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task AddWorkingWith(AdminAboutWorkingWithModify workingWithVm,MultipartFileData file, NbpContext db)
        {

            //try to save
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
            var directoryPath = HttpContext.Current.Server.MapPath("~/Images/WorkingWith");

            var newPath = Path.Combine(directoryPath, fileName);
            //check if file exists
            if (File.Exists(newPath))
            {
                var dateString = DateTime.Now.ToString("yyyyMMddHHmmss");
                fileName = dateString + '_' + fileName;
            }

            //move file from temp folder to main folder, ovveride if needed
            //File.Move(file.LocalFileName, imagePath);
            using (var imgSizer = new ImageResizer(file.LocalFileName))
            {
                imgSizer.ResizeForWorkingWith();
                imgSizer.SaveWorkingWith(fileName,directoryPath);
            }

            //dynamic path in db
            workingWithVm.PhotoUrl = "/Images/WorkingWith/" + fileName;


            //working with db model
            var workingWithDb = new WorkingWith()
            {
                Title = workingWithVm.Title,
                Link = workingWithVm.Link,
                LinkText = workingWithVm.LinkText,
                Importance = workingWithVm.Importance,
                Description = workingWithVm.Description,
                PhotoUrl = workingWithVm.PhotoUrl
            };

            db.WorkingWiths.Add(workingWithDb); //add to db

            File.Delete(file.LocalFileName); //delete bodypart

            await db.SaveChangesAsync(); //Save changes to db
        }

        /// <summary>
        /// Edit existing with
        /// </summary>
        /// <param name="workingWithVm"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task EditWorkingWith(AdminAboutWorkingWithModify workingWithVm, MultipartFileData file, NbpContext db)
        {
            //get from db
            var workingWithDb = await db.WorkingWiths.FindAsync(workingWithVm.Id);
            
            
            // new file was uploaded
            if (file != null)
            {
                var photoUrl = HttpContext.Current.Server.MapPath(workingWithDb.PhotoUrl);
                //Path.Combine(root, workingWithDb.PhotoUrl);
                

                //save new
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
                var directoryPath = HttpContext.Current.Server.MapPath("~/Images/WorkingWith");

                var newPath = Path.Combine(directoryPath, fileName);
                //check if file exists
                if (File.Exists(newPath))
                {
                    var dateString = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName = dateString + '_' + fileName;
                }

                //move file from temp folder to main folder, ovveride if needed
                //File.Move(file.LocalFileName, imagePath);
                using (var imgSizer = new ImageResizer(file.LocalFileName))
                {
                    imgSizer.ResizeForWorkingWith();
                    imgSizer.SaveWorkingWith(fileName, directoryPath);
                }

                //dynamic path in db
                workingWithVm.PhotoUrl = "/Images/WorkingWith/" + fileName;

                File.Delete(file.LocalFileName); //delete bodypart
                //delete previous
                if (workingWithVm.PhotoUrl != workingWithDb.PhotoUrl)
                {
                    File.Delete(photoUrl);
                }
                

            }

            //save new data to db model
            workingWithDb.Title = workingWithVm.Title;
            workingWithDb.Link = workingWithVm.Link;
            workingWithDb.LinkText = workingWithVm.LinkText;
            workingWithDb.Importance = workingWithVm.Importance;
            workingWithDb.Description = workingWithVm.Description;
            workingWithDb.PhotoUrl = workingWithVm.PhotoUrl;
     

            db.Entry(workingWithDb).State = EntityState.Modified;


            await db.SaveChangesAsync(); //Save changes to db
        }

        /// <summary>
        /// Delete working with
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task DeleteWorkingWith(int? id, NbpContext db)
        {
            //find working with
            var workingWith = await db.WorkingWiths.FindAsync(id);

            //set state and try to delete
            db.Entry(workingWith).State = EntityState.Deleted;

            await db.SaveChangesAsync();

            //delete linked photo if exists
            if (workingWith.PhotoUrl != null)
            {
                File.Delete(workingWith.PhotoUrl);
            }
        }
    }
}

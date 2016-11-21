using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class PhotosController : ApiController
    {

        private NbpContext _db = new NbpContext();
        
        [System.Web.Http.HttpGet]
        //GET LIST OF ALL PHOTOS, LIMITED BY PAGE
        public IHttpActionResult GetPhotos([FromUri] int page = 0,[FromUri] int pagesize=20)
        {
            try
            {
                //query for db, need only size and page, everything else will take angular over
                var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex(page, pagesize, _db);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,query));
            }
            catch(Exception err)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, err.Message));
            }
            



        }

        [System.Web.Http.HttpDelete]
        //DELETES PHOTO FROM DATABASE
        public IHttpActionResult DeletePhoto([FromUri]int? id)
        {
            //try to delete photo
            try
            {
                PhotosDatabase.DeletePhotoFromDatabase(id, _db);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Photo deleted"));
            }
            //error 
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, e.Message));
            }
            
            
        }

        [System.Web.Http.HttpPost]
        //ADDS PHOTO TO DATABASE AND SERVER
        public async Task<HttpResponseMessage> AddPhoto(HttpRequestMessage photo)
        {
            //create admin createvm object that will hold data
            var requestBody = new AdminPhotoCreateVm();

            //root temp folder for multiform data
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");

            //multiform reader
            var provider = new MultipartFormDataStreamProvider(root);

            //object that will hold photo file
            MultipartFileData photoFile = null;

            //try
            try
            {

                //read file
                await Request.Content.ReadAsMultipartAsync(provider);

                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    //set object values
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "AlbumId")
                        {
                            requestBody.AlbumId = int.Parse(val);
                        }
                        else if (key == "IsOnPortfolio")
                        {
                            requestBody.IsOnPortfolio = bool.Parse(val);
                        }
                        else if (key == "IsAlbumCover")
                        {
                            requestBody.IsAlbumCover = bool.Parse(val);
                        }
                        else if (key == "PhotoCategories")
                        {
                            var splittedValues = val.Split(',');
                            foreach(string category in splittedValues)
                            {
                                requestBody.PhotoCategories.Add(category);
                            }
                            
                        }
                        else if (key == "PhotoTitle")
                        {
                            requestBody.PhotoTitle = val;
                        }
                    }
                }

                //check if only 1 file was appended
                if (provider.FileData.Count != 1)
                {
                    throw new Exception("Multiple photos were uploaded");
                }
                else
                {
                    //save file reference
                    photoFile = provider.FileData[0];
                }

                //add photo to db and save to server
                var result = PhotosDatabase.AddNewPhotoToDatabase(requestBody, photoFile, _db);

                //check if it was added successfully
                if (result == DbResults.PhotoDb.Success)
                {
                    //response with OK - 200
                    return Request.CreateResponse(HttpStatusCode.OK, DbResults.PhotoDb.Success.ToString());
                }

                //img wasnt added, return error msg
                return Request.CreateResponse(HttpStatusCode.BadRequest, result.ToString());
            }
            catch (System.Exception e)
            {

                //catch any other error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
       
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.HelperClasses;
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
        public IHttpActionResult GetPhotos([FromUri] int page = 0,[FromUri] int pagesize=20, [FromUri] int? id = null)
        {
            //not a single id get.
            if (id != null)
            {
                try
                {
                    var query = PhotosDatabase.ReturnSinglePhotoForEdit(id, _db);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, query));
                }
                catch(Exception ex)
                {
                    ErrorHandler.ServerError(ServerType.API, ex);
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
                }
                

            }
            else
            {
                try
                {

                    //query for db, need only size and page, everything else will take angular over
                    var query = PhotosDatabase.ReturnPhotosForAdminPhotoIndex(page, pagesize, _db);

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, query));
                }
                catch (Exception err)
                {
                    ErrorHandler.ServerError(ServerType.API, err);
                    if (err.Message == "end")
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent,"No more photos"));
                    }
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, err.Message));
                }
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
                ErrorHandler.ServerError(ServerType.API, e);
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
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

            //multiform reader
            var provider = new MultipartFormDataStreamProvider(root);


            //try
            try
            {

                //read file
                await Request.Content.ReadAsMultipartAsync(provider);

                //set multipart content data and get uploaded file
                MultipartFileData photoFile = getMultipartContent(requestBody,provider);          

                //add photo to db and save to server
                var result = PhotosDatabase.AddNewPhotoToDatabase(requestBody, photoFile, _db);


                //response with OK - 200
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }



            catch (System.Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                //catch any other error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [System.Web.Http.HttpPut]
        //EDITS PHOTO IN DATABASE AND SERVER
        public async Task<HttpResponseMessage> EditPhoto(AdminPhotoEditVm photo)
        {
            try
            {
                //try to edit photo in database
                await PhotosDatabase.EditPhotoInDatabase(photo, _db);
                return Request.CreateErrorResponse(HttpStatusCode.OK, "success");

            }
            catch(Exception ex)
            {
                //throw exception
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

        //get data from multipart content 
        private MultipartFileData getMultipartContent(AdminPhotoCreateVm photoData,
            MultipartFormDataStreamProvider provider)
        {
            // Show all the key-value pairs.
            foreach (var key in provider.FormData.AllKeys)
            {
                //set object values
                foreach (var val in provider.FormData.GetValues(key))
                {
                    if (key == "AlbumId")
                    {
                        photoData.AlbumId = int.Parse(val);
                    }
                    else if (key == "IsOnPortfolio")
                    {
                        photoData.IsOnPortfolio = bool.Parse(val);
                    }
                    else if (key == "IsAlbumCover")
                    {
                        photoData.IsAlbumCover = bool.Parse(val);
                    }
                    else if (key == "PhotoCategories")
                    {
                        var splittedValues = val.Split(',');
                        foreach (string category in splittedValues)
                        {
                            photoData.PhotoCategories.Add(category);
                        }

                    }
                    else if (key == "PhotoTitle")
                    {
                        photoData.PhotoTitle = val;
                    }
                    else if (key == "HomeCarousel")
                    {
                        photoData.HomeCarousel = bool.Parse(val);
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
                return provider.FileData[0];
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}

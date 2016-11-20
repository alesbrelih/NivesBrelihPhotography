using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult AddPhoto([FromBody] AdminPhotoCreateVm photo)
        {
            //check if there is any data recieved
            if (photo == null)
            {
                return
                    ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "No data attached to the request"));
            }
            //tries to adds photo to db
            var result = PhotosDatabase.AddNewPhotoToDatabase(photo, _db);
           
            //check if it was added successfully
            if (result == DbResults.PhotoDb.Success)
            {
                //response with OK - 200
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, DbResults.PhotoDb.Success));
            }

            //img wasnt added, return error msg
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result));


            
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
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

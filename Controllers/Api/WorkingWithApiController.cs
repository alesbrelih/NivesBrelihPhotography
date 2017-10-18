using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels;
using NivesBrelihPhotography.HelperClasses;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class WorkingWithController : ApiController
    {
        private NbpContext _db = new NbpContext();

        //get all working withs
        [HttpGet]
        public async Task<HttpResponseMessage> GetWorkingWiths(int? id = null)
        {
            try
            {
                if (id != null) //return single
                {
                    var single = await WorkingWithDatabase.GetSingle(id, _db);
                    return Request.CreateResponse(HttpStatusCode.OK, single);
                }
                else //return list
                {
                    var query = await WorkingWithDatabase.GetAll(_db);
                    return Request.CreateResponse(HttpStatusCode.OK, query);
                }
                

            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //[HttpGet]
        //public async Task<HttpResponseMessage> GetSingleWorkingWith(int id)
        //{
        //    try
        //    {
        //        var query = await WorkingWithDatabase.GetSingle(id,_db);
        //        return Request.CreateResponse(HttpStatusCode.OK, query);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        // add new working with
        [HttpPost]
        public async Task<HttpResponseMessage> AddWorkingWith(HttpRequestMessage workingWithVm)
        {
            //check if working with vm isnt null
            if (workingWithVm == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No request body");
            }

            MultipartFileData photoFile = null;

            //try to add
            try
            {
                //create admin createvm object that will hold data
                var requestBody = new AdminAboutWorkingWithModify();

                //root temp folder for multiform data
                string root = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

                //multiform reader
                var provider = new MultipartFormDataStreamProvider(root);

                //read file
                await Request.Content.ReadAsMultipartAsync(provider);

                //set multipart content data and get uploaded file
                photoFile = getMultipartContent(requestBody, provider);

                await WorkingWithDatabase.AddWorkingWith(requestBody, photoFile, _db);

                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                if (photoFile != null && File.Exists(photoFile.LocalFileName))
                {
                    File.Delete(photoFile.LocalFileName); //clear temp folder
                }
            }

        }

        // edit working with
        [HttpPut]
        public async Task<HttpResponseMessage> EditWorkingWith(HttpRequestMessage workingWithVm)
        {
            //check if data was send
            if (workingWithVm == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No working with data was provided");
            }
            MultipartFileData photoFile = null;
            //try to edit
            try
            {

                //create admin createvm object that will hold data
                var requestBody = new AdminAboutWorkingWithModify();

                //root temp folder for multiform data
                string root = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

                //multiform reader
                var provider = new MultipartFormDataStreamProvider(root);

                //read file
                await Request.Content.ReadAsMultipartAsync(provider);

                //set multipart content data and get uploaded file
                photoFile = getMultipartContent(requestBody, provider);

                //get data
                await WorkingWithDatabase.EditWorkingWith(requestBody, photoFile, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            finally
            {

                if (photoFile != null && File.Exists(photoFile.LocalFileName))
                {
                    File.Delete(photoFile.LocalFileName);
                }
            }
        }

        //delete selected working with
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteWorkingWith([FromUri] int? id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No id was provided.");
            }

            //try to delete else err
            try
            {
                await WorkingWithDatabase.DeleteWorkingWith(id,_db);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //get data from multipart content 
        private MultipartFileData getMultipartContent(AdminAboutWorkingWithModify workingWithData,
            MultipartFormDataStreamProvider provider)
        {
            // Show all the key-value pairs.
            foreach (var key in provider.FormData.AllKeys)
            {
                //set object values
                foreach (var val in provider.FormData.GetValues(key))
                {
                    if (key == "Id")
                    {
                        workingWithData.Id = int.Parse(val);
                    }
                    else if (key == "Title")
                    {
                        workingWithData.Title = val;
                    }
                    else if (key == "Description")
                    {
                        workingWithData.Description = val;
                    }
                    else if (key == "Link")
                    {
                        workingWithData.Link = val;
                    }
                    else if (key == "LinkText")
                    {
                        workingWithData.LinkText = val;
                    }
                    else if (key == "Importance")
                    {
                        workingWithData.Importance = int.Parse(val);
                    }
                }
            }

            //check if only 1 file was appended
            if (provider.FileData.Count > 1)
            {
                throw new Exception("Multiple photos were uploaded");
            }

            return provider.FileData.Count == 0 ? null : provider.FileData[0]; //return filename if it was uploaded else null
            //save file reference
           
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
            base.Dispose(disposing);
        }
    }
}

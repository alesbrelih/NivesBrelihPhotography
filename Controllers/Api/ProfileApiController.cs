using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Enums;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class ProfileController : ApiController
    {
        //get db connection
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //get profile info
        public async Task<HttpResponseMessage> GetProfileInfo()
        {
            try
            {
                var profile = await ProfileDatabase.GetProfile(_db);
                return Request.CreateResponse(HttpStatusCode.OK, profile);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpPut]
        //updates profile info
        public async Task<HttpResponseMessage> UpdateProfileInfo(HttpRequestMessage message)
        {
            //create admin  object that will hold data
            var requestBody = new AdminAboutProfileInformation();

            //root temp folder for multiform data
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

            //multiform reader
            var provider = new MultipartFormDataStreamProvider(root);

            //object that will hold photo file
            MultipartFileData photoFile = null;

            //try
            try
            {

                //read multiform data
                await Request.Content.ReadAsMultipartAsync(provider);

                #region getProfileData

                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    //set object values
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "ProfileId")
                        {
                            requestBody.Id = int.Parse(val);
                        }
                        else if (key == "Name")
                        {
                            requestBody.Name = val;
                        }
                        else if (key == "Lastname")
                        {
                            requestBody.Lastname = val;
                        }
                        else if (key == "About")
                        {
                            requestBody.About = val;

                        }
                        else if (key == "ContactEmail")
                        {
                            requestBody.Email = val;
                        }
                        else if (key == "ContactPhone")
                        {
                            requestBody.Phone = val;
                        }
                    }
                }

                #endregion


                #region getProfileImg

                //check if only 1 file was appended
                if (provider.FileData.Count > 1)
                {
                    throw new Exception("Multiple photos were uploaded");
                }
                else if (provider.FileData.Count == 1) //if file was updated
                {
                    //save file reference
                    photoFile = provider.FileData[0];
                }

                #endregion

                //update profile in db
                await ProfileDatabase.EditProfile(requestBody, photoFile, _db);

                //profile was updated successfully
                return Request.CreateResponse(HttpStatusCode.OK, "Success");


            }
            catch (Exception e)
            {

                //catch any other error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            finally
            {
                //check if provider and delete temp multipart data
                if (provider?.FileData[0] != null)
                {
                    //delete temp file
                    var toString = provider.FileData[0].LocalFileName;
                    System.IO.File.Delete(toString);
                }
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
    
using System;
using System.Collections.Generic;
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
    public class ReferencesController : ApiController
    {
        private NbpContext _db = new NbpContext();

        //get references
        [HttpGet]
        public async Task<HttpResponseMessage> GetReferences()
        {
            try
            {
                var query = await ProfileDatabase.GetReferences(_db);
                return Request.CreateResponse(HttpStatusCode.OK, query);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //get single reference
        public async Task<HttpResponseMessage> GetSingleReference([FromUri]int id)
        {
            try
            {
                var referenceDb = await ProfileDatabase.GetReference(id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, referenceDb);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //create reference
        [HttpPost]
        public async Task<HttpResponseMessage> AddReference(AdminAboutReferenceModify reference)
        {
            try
            {
                //try to create the reference
                await ProfileDatabase.AddReference(reference, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Success saving new reference.");
            }
            catch (Exception ex)
            {
                //err catch
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        //edit reference
        [HttpPut]
        public async Task<HttpResponseMessage> EditReference(AdminAboutReferenceModify reference)
        {
            //check if it has reference id
            if (reference.Id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No reference id provided");
            }
            try
            {
                await ProfileDatabase.EditReference(reference, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Reference successfully edited.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //delete reference with given id
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteReference(int id)
        {
            try
            {
                await ProfileDatabase.DeleteReference(id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Reference successfully deleted.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
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

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
    public class SocialsController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets all social links
        public async Task<HttpResponseMessage> GetSocials()
        {
            try
            {
                var socials = await ProfileDatabase.GetSocialLinks(_db);
                return Request.CreateResponse(HttpStatusCode.OK, socials);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        //update selected social link
        public async Task<HttpResponseMessage> UpdateSocials(AdminAboutProfileLinkUpdate socialLink)
        {
            //try to update social link
            try
            {
                await ProfileDatabase.UpdateSocialLink(socialLink,_db);
                return Request.CreateResponse(HttpStatusCode.OK, "Social link information successfully updated.");
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

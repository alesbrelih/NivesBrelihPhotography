using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class TermsConditionsController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets all social links
        public HttpResponseMessage GetTerms()
        {
            try
            {
                var terms = TermsConditionsDatabase.GetTermsConditions(_db);
                return Request.CreateResponse(HttpStatusCode.OK, terms);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        //update selected social link
        public async Task<HttpResponseMessage> SetSocials(TermsConditions termsConditions)
        {
            //try to update social link
            try
            {
                await TermsConditionsDatabase.SetTermsConditions(termsConditions, _db);
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

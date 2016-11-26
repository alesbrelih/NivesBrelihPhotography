using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class AlbumsController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        public IHttpActionResult GetAlbums()
        {
            try
            {
                var query = AlbumsDatabase.ReturnAlbumsForSelectList(_db);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, query));
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
            }
            

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {   
                //dispose if not null
                _db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

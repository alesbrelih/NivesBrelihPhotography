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
    public class BlogsController : ApiController
    {

        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets all blogs
        public async Task<HttpResponseMessage> GetBlogsAsync()
        {
            try
            {
                //try to get blogs from db
                var blogs = await BlogsDatabase.GetBlogsAsync(_db);
                return Request.CreateResponse(HttpStatusCode.OK, blogs);
            }
            catch (Exception ex)
            {
                //catch error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpDelete]
        //remove blog
        public async Task<HttpResponseMessage> DeleteBlogAsync([FromUri] int? id)
        {
            //check if there is Id
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No id provided");
            }
            try
            {
                await BlogsDatabase.DeleteBlogAsync((int) id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Blog successfully deleted.");
            }
            catch (Exception ex)
            {
                //catch err
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

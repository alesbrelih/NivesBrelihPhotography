using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Admin_ViewModels;
using NivesBrelihPhotography.HelperClasses;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class BlogsController : ApiController
    {
        //connection to db
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
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpGet]
        //get single blog
        public async Task<HttpResponseMessage> GetBlogAsync(int? id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No blog id was provided");
            }
            try
            {
                var blog = await BlogsDatabase.GetBlogAsync((int) id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, blog);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        //creates blog
        public async Task<HttpResponseMessage> CreateBlogAsync([FromBody] AdminBlogsModify blog)
        {
            if (blog == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No blog data was given.");
            }
            try
            {
                var blogVm = await BlogsDatabase.CreateBlogAsync(blog, _db);
                return Request.CreateResponse(HttpStatusCode.OK, blogVm);
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        //edits blog
        public async Task<HttpResponseMessage> EditBlogAsync([FromBody] AdminBlogsModify blog)
        {
            if (blog == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No data blog data was given");
            }
            try
            {
                await BlogsDatabase.EditBlogAsync(blog, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Blog successfully edited");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                if (ex.Message == "Blog with such Id does not exist.")
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                if (ex.Message == "Invalid album Id.")
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                if (ex.Message == "Invalid category id.")
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
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

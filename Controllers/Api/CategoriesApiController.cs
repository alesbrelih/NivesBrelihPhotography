using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;
using NivesBrelihPhotography.HelperClasses;

namespace NivesBrelihPhotography.Controllers.Api
{
    //categories api controller
    public class CategoriesController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets all categories
        public IHttpActionResult GetCategories()
        {
            //try getting all categories and return 200 status with queried categories
            try
            {
                var query = CategoriesDatabase.ReturnAllCategoriesViewModels(_db);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, query));
            }
            //if new exception thrown
            catch(Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
            }
            
        }

        [HttpPost]
        //creates new category
        public HttpResponseMessage AddCategory([FromBody] AdminCategoryCreateVm newCategory )
        {
            try
            {
                //tries to create new category type in db
                var category = CategoriesDatabase.CreateNewCategory(newCategory, _db);
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch(Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                if (ex.Message == "Category with the same name already exists")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            
        }

        [HttpDelete]
        //deleted selected category
        public async Task<HttpResponseMessage> RemoveCategory([FromUri] int id)
        {
            //try to delete
            try
            {
                await CategoriesDatabase.RemoveCategory(id,_db);

                return Request.CreateResponse(HttpStatusCode.OK, "Category successfully deleted");

            }
            catch (Exception ex) //catch any error
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        //edits category
        public async Task<HttpResponseMessage> EditCategory([FromBody] AdminCategoryVm category)
        {
            try
            {
                await CategoriesDatabase.EditCategory(category, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                ErrorHandler.ServerError(ServerType.API, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        protected override void Dispose(bool disposing)
        {
            //if disposing
            if(disposing)
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

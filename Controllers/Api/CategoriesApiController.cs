using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;

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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
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

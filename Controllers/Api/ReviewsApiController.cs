using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.AboutModels;
using NivesBrelihPhotography.Models.AboutModels.ViewModels;
using NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.Controllers.Api
{
    public class ReviewsApiController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets all reviews
        public async Task<HttpResponseMessage> GetReviews()
        {
            try
            {
                var reviews = await ProfileDatabase.GetReviews(_db);
                return Request.CreateResponse(HttpStatusCode.OK, reviews);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        //creates review
        public async Task<HttpResponseMessage> AddReview(AdminPhotoShootReviewCreate review)
        {
            try
            {
                //try to add review to db
                var reviewDb = await ProfileDatabase.AddReview(review, _db);
                return Request.CreateResponse(HttpStatusCode.OK, reviewDb);
            }
            catch (Exception ex)
            {
                //error handler
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        //updates review
        public async Task<HttpResponseMessage> UpdateReview(PhotoShootReview review)
        {
            try
            {
                //try to update review
                await ProfileDatabase.UpdateReview(review, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Review successfully updated");
            }
            catch (Exception ex)
            {
                //err catch
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        //deleted review
        public async Task<HttpResponseMessage> RemoveReview([FromUri] int id)
        {
            try
            {
                //try to delete review from db
                await ProfileDatabase.RemoveReview(id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Review successfully deleted");
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

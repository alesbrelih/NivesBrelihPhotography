using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.AboutModels.ViewModels;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.Controllers
{
    public class AboutController : BaseController
    {
        private NbpContext _db = new NbpContext();
        // GET: About
        public async Task<ActionResult> Index()
        {
            //only one profile in db
            var profileDb = await _db.Profile.FirstOrDefaultAsync();
            var profileVm = new ProfileView(); //default profile vm for error catch if profile db empty

            //if profile db not empty change values
            if (profileDb != null)
            {
                profileVm = new ProfileView()
                {
                    Name = profileDb.Name,
                    LastName = profileDb.Lastname,
                    About = profileDb.About,
                    ProfilePhoto = profileDb.ProfilePicture,
                    ContactEmail = profileDb.ContactEmail,
                    ContactPhone = profileDb.ContactPhone
                };
            }

            //social links list VM
            var socialLinks =
                await _db.ProfileLinks.Where(x=>x.ShownOnProfile).Select(
                    x =>
                        new ProfileLinkView()
                        {
                            Title = x.LinkName,
                            Description = x.LinkDescription,
                            IconLink = x.LinkImgUrl,
                            Link = x.LinkUrl
                        }).ToListAsync();

            //photoshot reviews list (view model)
            var photoShootReviewsList = await _db.PhotoShootReviews
                .OrderByDescending(x => x.PhotoShootReviewId).Take(6).Select(x => new PhotoShotReviewView
            {
                PhotoShotReviewId = x.PhotoShootReviewId,
                ReviewerName = x.ReviewerName,
                Review = x.Review
            }).ToListAsync();

            //about index VM
            var aboutIndexVm = new AboutIndexViewModel()
            {
                Profile = profileVm,
                SocialLinks = socialLinks,
                PhotoShootReviews = photoShootReviewsList
            };
            return View(aboutIndexVm);
        }

        // GET: Reviews
        public async Task<ActionResult> Reviews()
        {
            //all reviews
            var reviewsVm = await _db.PhotoShootReviews.OrderByDescending(x => x.PhotoShootReviewId).Select(x => new PhotoShotReviewView() { PhotoShotReviewId = x.PhotoShootReviewId, ReviewerName = x.ReviewerName, Review = x.Review }).ToListAsync();

            return View(reviewsVm);
        }

        // GET: WorkingWith
        public async Task<ActionResult> WorkingWith()
        {
            var query = await _db.WorkingWiths.OrderByDescending(x => x.Importance).ToListAsync();
            return View(query);
        }

        //disposing
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

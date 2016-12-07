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

            //reference list VM
            var referenceList =
                await _db.References.OrderByDescending(x => x.ReferenceId)
                    .Select(
                        x =>
                            new ReferenceView()
                            {
                                ReferenceId = x.ReferenceId,
                                Title = x.ReferenceTitle
                            })
                    .ToListAsync();

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
                References = referenceList,
                SocialLinks = socialLinks,
                PhotoShootReviews = photoShootReviewsList
            };
            return View(aboutIndexVm);
        }


        // GET: References
        public async Task<ActionResult> Reference(int? referenceId = null, int pageNumber = 0)
        {

            //no reference id was provided -> redirect to index
            if (referenceId == null)
            {
                return RedirectToAction("Index");
            }

            //if ajax request for more reference photos
            if (Request.IsAjaxRequest())
            {
                var morePhotosQuery =
                    await _db.ReferencePhotos.Where(x => x.ReferenceId == referenceId)
                        .OrderBy(x => x.PhotoId)
                        .Skip(pageNumber * 10)
                        .Take(10)
                        .Select(x => new PhotoView() { PhotoTitle = x.Photo.PhotoTitle, PhotoUrl = x.Photo.PhotoUrl })
                        .ToListAsync();
                return Json(morePhotosQuery, JsonRequestBehavior.AllowGet);
            }

            //get reference
            var referenceDb = await _db.References.FindAsync(referenceId);

            //if no ref found in db go to index
            if (referenceDb == null)
            {
                return RedirectToAction("Index");
            }

            var referenceDetailsVm = new ReferenceDetailsView(referenceDb);
            //referenceDetailsVm.ReferencePhotos = referencesPhotosVm;

            return View(referenceDetailsVm);
        }


        // GET: Reviews
        public async Task<ActionResult> Reviews()
        {
            //all reviews
            var reviewsVm = await _db.PhotoShootReviews.OrderByDescending(x => x.PhotoShootReviewId).Select(x => new PhotoShotReviewView() { PhotoShotReviewId = x.PhotoShootReviewId, ReviewerName = x.ReviewerName, Review = x.Review }).ToListAsync();

            return View(reviewsVm);
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

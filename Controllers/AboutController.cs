using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            //only one profile in db
            var profileDb = _db.Profile.FirstOrDefault();
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
                _db.ProfileLinks.Select(
                    x =>
                        new ProfileLinkView()
                        {
                            Title = x.LinkName,
                            Description = x.LinkDescription,
                            IconLink = x.LinkImgUrl,
                            Link = x.LinkUrl
                        }).ToList();

            //reference list VM
            var referenceList =
                _db.References.OrderByDescending(x => x.ReferenceId)
                    .Select(
                        x =>
                            new ReferenceView()
                            {
                                ReferenceId = x.ReferenceId,
                                Title = x.ReferenceTitle
                            })
                    .ToList();

            //photoshot reviews list (view model)
            var photoShootReviewsList = _db.PhotoShootReviews.OrderByDescending(x => x.PhotoShootReviewId).Take(6).Select(x => new PhotoShotReviewView
            {
                PhotoShotReviewId = x.PhotoShootReviewId,
                ReviewerName = x.ReviewerName,
                Review = x.Review
            }).ToList();

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

        // GET: About/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: About/Create
        public ActionResult Reference(int? referenceId = null, int pageNumber = 0)
        {
            //if ajax request for more reference photos
            if (Request.IsAjaxRequest())
            {
                var morePhotosQuery =
                    _db.ReferencePhotos.Where(x => x.ReferenceId == referenceId)
                        .OrderBy(x => x.PhotoId)
                        .Skip(pageNumber * 10)
                        .Take(10)
                        .Select(x => new PhotoView() { PhotoTitle = x.Photo.PhotoTitle, PhotoUrl = x.Photo.PhotoUrl })
                        .ToList();
                return Json(morePhotosQuery, JsonRequestBehavior.AllowGet);
            }

            var referenceDb = _db.References.Find(referenceId);

            var referenceDetailsVm = new ReferenceDetailsView(referenceDb);
            //referenceDetailsVm.ReferencePhotos = referencesPhotosVm;

            return View(referenceDetailsVm);
        }

        public ActionResult Reviews()
        {
            //all reviews
            var reviewsVm = _db.PhotoShootReviews.OrderByDescending(x => x.PhotoShootReviewId).Select(x => new PhotoShotReviewView() { PhotoShotReviewId = x.PhotoShootReviewId, ReviewerName = x.ReviewerName, Review = x.Review }).ToList();

            return View(reviewsVm);
        }

        // POST: About/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: About/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: About/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: About/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: About/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Detail;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Index;

namespace NivesBrelihPhotography.Controllers
{
    public class BlogController : BaseController
    {
        private NbpContext _db = new NbpContext();
        private const int NumberOfBlogs = 6;

        // GET: Blogs
        public async Task<ActionResult> Index(int categoryId = -1, int pageNumber = 0)
        {

            if (Request.IsAjaxRequest())
            {
                if (pageNumber == 0)  //user selects new category so pageNumber is 0
                {

                    var query = await
                        _db.BlogCategories.Where(x => x.CategoryId == categoryId)
                            .OrderBy(x => x.Blog.BlogDate)
                            .Take(NumberOfBlogs)
                            .Select(x => new BlogIndexView() //creates viewmodel with needed data
                                {
                                BlogId = x.Blog.BlogId,
                                BlogDate = x.Blog.BlogDate,
                                BlogTitle = x.Blog.BlogTitle,
                                Description = x.Blog.BlogDescription,
                                BlogCoverPhoto = x.Blog.CoverPhoto.PhotoUrl,
                                Categories = x.Blog.Categories.ToList()
                            }).ToListAsync();


                    ViewBag.CurrentCategory = categoryId; //current category of blogs
                    ViewBag.PageNumber = pageNumber;  //page number

                    //return partial view
                    return PartialView("_blogsContainerIndex", query);
                }
                else  //its continuation of blogs
                {
                    if (categoryId != -1) //category was selected on previous page
                    {
                        var query = await _db.BlogCategories.Where(x => x.CategoryId == categoryId)
                            .OrderBy(x => x.Blog.BlogDate).Skip(pageNumber*NumberOfBlogs)
                            .Take(NumberOfBlogs)
                            .Select(x => new BlogIndexView() //creates viewmodel with needed data
                            {
                                BlogId = x.Blog.BlogId,
                                BlogDate = x.Blog.BlogDate,
                                BlogTitle = x.Blog.BlogTitle,
                                Description = x.Blog.BlogDescription,
                                BlogCoverPhoto = x.Blog.CoverPhoto.PhotoUrl,
                                Categories = x.Blog.Categories.ToList()
                            }).ToListAsync();


                        ViewBag.PageNumber = pageNumber; //page number

                        return PartialView("_blogsListIndex", query); //list of blogs
                    }
                    else //category wasnt selected
                    {
                        //query blogs
                        var query = await _db.Blogs.OrderBy(x => x.BlogDate).Skip(pageNumber*NumberOfBlogs)
                            .Take(NumberOfBlogs).Select(x => new BlogIndexView()
                            {
                                BlogId = x.BlogId,
                                BlogDate = x.BlogDate,
                                BlogTitle = x.BlogTitle,
                                Description = x.BlogDescription,
                                BlogCoverPhoto = x.CoverPhoto.PhotoUrl,
                                Categories = x.Categories.ToList()
                            }).ToListAsync();

                        ViewBag.PageNumber = pageNumber; //page number

                        return PartialView("_blogsListIndex", query); //list of blogs
                    }

                    

                }
            }

            else
            {
                //takes blogs from database
                var query =
                    await _db.Blogs.OrderBy(x => x.BlogDate)
                        .Take(NumberOfBlogs)
                        .Select(
                            x => new BlogIndexView()  //creates viewmodel with needed data
                            {
                                BlogId = x.BlogId,
                                BlogDate = x.BlogDate,
                                BlogTitle = x.BlogTitle,
                                Description = x.BlogDescription,
                                BlogCoverPhoto = x.CoverPhoto.PhotoUrl,
                                Categories = x.Categories.ToList()
                            }
                                ).ToListAsync();

                ViewBag.CurrentCategory = categoryId;  //send category id to page

                //lists all categories
                ViewBag.Categories =
                    await _db.BlogCategories.GroupBy(x => x.CategoryId).Select(x => x.FirstOrDefault().Category).ToListAsync();

                ViewBag.PageNumber = pageNumber;  //page number

                return View(query); //returns blogs index view
            }

        }

        //GET: Blog
        public async Task<ActionResult> ViewBlog(int blogId)
        {
            //finds blog with blogId as PK
            var blog = await _db.Blogs.FindAsync(blogId);

            if (blog == null) //no blog was found
            {
                return RedirectToAction("Index");
            }

            var viewModel = new BlogDetailsView(blog);  //viewmodel blog for details page

            //if link to album exists, need to retrieve album data to show it
            if (viewModel.AlbumLink)
            {
                var photoAlbumQuery = await _db.Photos.FirstOrDefaultAsync(x => x.PhotoAlbumId == blog.AlbumId);
                if (photoAlbumQuery != null)
                {
                    viewModel.Album.AlbumPhotoUrl = photoAlbumQuery.PhotoUrl;
                    viewModel.Album.AlbumName = photoAlbumQuery.PhotoAlbum.AlbumName;
                    viewModel.Album.AlbumDate = photoAlbumQuery.PhotoAlbum.AlbumDate;
                    viewModel.Album.PhotoAlbumId = photoAlbumQuery.PhotoAlbum.PhotoAlbumId;
                }
                else
                {
                    viewModel.AlbumLink = false; // no album with that id, so return albumlink back to false
                }
                
            }

            return View(viewModel); //returns Blog/Details View
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
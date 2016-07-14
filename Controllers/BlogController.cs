using System;
using System.Linq;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Detail;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Index;

namespace NivesBrelihPhotography.Controllers
{
    public class BlogController : Controller
    {
        private NbpContext _db = new NbpContext();
        private int _numberOfBlogs = 6;

        // GET: Blogs
        public ActionResult Index(int categoryId=-1, int pageNumber = 0)
        {

            if (Request.IsAjaxRequest())
            {
                if (pageNumber == 0)  //user selects new category so pageNumber is 0
                {
                    var query =
                        _db.BlogCategories.Where(x => x.CategoryId == categoryId)
                            .OrderBy(x => x.Blog.BlogDate)
                            .Take(_numberOfBlogs)
                            .Select(x => new BlogIndexView() //creates viewmodel with needed data
                            {
                                BlogId = x.Blog.BlogId,
                                BlogDate = x.Blog.BlogDate,
                                BlogTitle = x.Blog.BlogTitle,
                                Description = x.Blog.BlogDescription,
                                BlogCoverPhoto = x.Blog.CoverPhoto.PhotoUrl,
                                Categories = x.Blog.Categories.ToList()
                            }).ToList();


                    ViewBag.CurrentCategory = categoryId; //current category of blogs
                    ViewBag.PageNumber = pageNumber;  //page number

                    //return partial view
                    return PartialView("_blogsContainerIndex",query);
                }
                else  //its continuation of blogs
                {
                 
                    
                    var query = _db.BlogCategories.Where(x=>x.CategoryId==categoryId)
                        .OrderBy(x => x.Blog.BlogDate).Skip(pageNumber*_numberOfBlogs)
                            .Take(_numberOfBlogs)
                            .Select(x => new BlogIndexView() //creates viewmodel with needed data
                            {
                                BlogId = x.Blog.BlogId,
                                BlogDate = x.Blog.BlogDate,
                                BlogTitle = x.Blog.BlogTitle,
                                Description = x.Blog.BlogDescription,
                                BlogCoverPhoto = x.Blog.CoverPhoto.PhotoUrl,
                                Categories = x.Blog.Categories.ToList()
                            }).ToList();


                    ViewBag.PageNumber = pageNumber;  //page number

                    return PartialView("_blogsListIndex",query); //list of blogs

                }
            }

            else
            {
                //takes blogs from database
                var query =
                    _db.Blogs.OrderBy(x => x.BlogDate)
                        .Take(_numberOfBlogs)
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
                                ).ToList();

                ViewBag.CurrentCategory = categoryId;  //send category id to page

                //lists all categories
                ViewBag.Categories =
                    _db.BlogCategories.GroupBy(x => x.CategoryId).Select(x => x.FirstOrDefault().Category).ToList();

                ViewBag.PageNumber = pageNumber;  //page number

                return View(query); //returns blogs index view
            }
            
        }

        //GET: Blog
        public ActionResult ViewBlog(int blogId)
        {
            //finds blog with blogId as PK
            var blog = _db.Blogs.Find(blogId);

            var viewModel = new BlogDetailsView(blog);  //viewmodel blog for details page

            //if link to album exists, need to retrieve album data to show it
            if (viewModel.AlbumLink)
            {
                var photoAlbumQuery = _db.Photos.FirstOrDefault(x => x.PhotoAlbumId == blog.AlbumId);

                viewModel.Album.AlbumPhotoUrl = photoAlbumQuery.PhotoUrl;
                viewModel.Album.AlbumName = photoAlbumQuery.PhotoAlbum.AlbumName;
                viewModel.Album.AlbumDate = photoAlbumQuery.PhotoAlbum.AlbumDate;
                viewModel.Album.PhotoAlbumId = photoAlbumQuery.PhotoAlbum.PhotoAlbumId;
            }

            return View(viewModel); //returns Blog/Details View
        }
    }
}
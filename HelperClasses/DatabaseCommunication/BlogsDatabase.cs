using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.BlogModels;
using NivesBrelihPhotography.Models.BlogModels.ViewModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class BlogsDatabase
    {

        //gets all blogs
        public static async Task<ICollection<AdminBlogsIndexVm>> GetBlogsAsync(NbpContext db)
        {
            var blogsDb = new List<AdminBlogsIndexVm>();

            //get all blogs
            await db.Blogs.OrderBy(x => x.BlogDate).ForEachAsync(x => blogsDb.Add(new AdminBlogsIndexVm()
            {
                Id = x.BlogId,
                Date = x.BlogDate,
                Description = x.BlogDescription,
                Title = x.BlogTitle,
                DateString = x.BlogDate.ToShortDateString()
            }));

            //return blogs
            return blogsDb;
        }

        //get blog using id
        public static async Task<AdminBlogsModify> GetBlogAsync(int id, NbpContext db)
        {
            //blog db model
            var blogDb = await db.Blogs.FindAsync(id);

            //blog returned model
            var blogVm = new AdminBlogsModify()
            {
                Id = blogDb.BlogId,
                CoverPhoto = blogDb.CoverPhotoId,
                AlbumId = blogDb.AlbumId?.ToString() ?? "-1",
                Content = blogDb.Content,
                Description = blogDb.BlogDescription,
                Title = blogDb.BlogTitle,
                Album = blogDb.AlbumLink

            };

            //set categories
            await
                db.BlogCategories.Where(x => x.BlogId == blogDb.BlogId)
                    .ForEachAsync(x => blogVm.Categories.Add(x.CategoryId.ToString()));

            return blogVm;

        }

        //edit blog 
        public static async Task EditBlogAsync(AdminBlogsModify blog, NbpContext db)
        {
            // get db model of blog
            var blogDb = await db.Blogs.FindAsync(blog.Id);

            //throw exception if null
            if (blogDb == null)
            {
                throw new Exception("Blog with such Id does not exist.");
            }
            //set data
            blogDb.BlogTitle = blog.Title;
            blogDb.BlogDescription = blog.Description;
            blogDb.Content = blog.Content;
            blogDb.CoverPhotoId = blog.CoverPhoto;

            //check if its a change with showing an album inside blog
            if (blogDb.AlbumLink != blog.Album)
            {
                //if previous was true, now its false and blogDb album data needs to be set to null
                if (blogDb.AlbumLink)
                {
                    blogDb.AlbumId = null;
                    blogDb.AlbumLink = false;
                }
            }
            //album bool matches
            else
            {
                //if it is true
                if (blogDb.AlbumLink)
                {
                    int albumId;
                    //check if valid int before assign
                    if (int.TryParse(blog.AlbumId, out albumId))
                    {
                        //set albumID
                        blogDb.AlbumId = albumId;

                    }
                    else
                    {
                        throw new Exception("Invalid album Id.");
                    }
                    
                }
            }

            //set blog categories
            var currentCategories = blogDb.Categories.Select(category => category.CategoryId.ToString()).ToList();

            //check those that were added
            foreach (var category in blog.Categories)
            {
                if (!currentCategories.Contains(category))
                {
                    int categoryId; //try to parse categoryid to int

                    if (int.TryParse(category, out categoryId))
                    {
                        blogDb.Categories.Add(new BlogCategory()
                        {
                            CategoryId = categoryId
                        });
                    }
                    else
                    {
                        throw new Exception("Invalid category id.");
                    }
                    
                }
            }

            //remove those that were removed
            foreach (var currentCategory in currentCategories)
            {
                if (!blog.Categories.Contains(currentCategory))
                {
                    // no need to check for parsing error because they were parsed to string before from db
                    var categoryId = int.Parse(currentCategory);

                    //get blog category
                    var blogCategory =
                        await
                            db.BlogCategories.SingleOrDefaultAsync(
                                x => x.CategoryId == categoryId && x.BlogId == blogDb.BlogId);

                    db.Entry(blogCategory).State = EntityState.Deleted;
                    
                }
            }

            //save changes
            await db.SaveChangesAsync();
        }

        //delete blog from db
        public static async Task DeleteBlogAsync(int id, NbpContext db)
        {
            //try to get blog in db
            var blogDb = await db.Blogs.FindAsync(id);
            //catch no blog
            if (blogDb == null)
            {
                throw new Exception("No blog with such id was found in database.");
            }

            //delete blog
            db.Entry(blogDb).State = EntityState.Deleted;
            
            // 2. delete all its categories
            await db.BlogCategories.Where(x => x.BlogId == blogDb.BlogId).ForEachAsync(x =>
            {
                db.Entry(x).State = EntityState.Deleted;
            });


            // save all changes in db
            await db.SaveChangesAsync();

        }

        //create blog 
        public static async Task<AdminBlogsModify> CreateBlogAsync(AdminBlogsModify blog, NbpContext db)
        {

            //create new model blog
            Blog blogDb = new Blog();

            //set data
            blogDb.BlogTitle = blog.Title;
            blogDb.BlogDescription = blog.Description;
            blogDb.Content = blog.Content;
            blogDb.CoverPhotoId = blog.CoverPhoto;
            blogDb.BlogDate = DateTime.Now;


            //set album if it was selected
            if (blog.Album)
            {
                blogDb.AlbumLink = true;

                int number;
                if (int.TryParse(blog.AlbumId, out number))
                {
                    blogDb.AlbumId = number;
                }
                else
                {
                    blogDb.AlbumLink = false;
                }
            }

            

            //await db change to get blog id
            //await db.SaveChangesAsync();

            //set categories
            foreach (var category in blog.Categories)
            {
                int number;
                if (int.TryParse(category, out number))
                {
                    blogDb.Categories.Add(new BlogCategory()
                    {
                        CategoryId = number
                    });
                }
            }

            //add new blog to db
            db.Blogs.Add(blogDb);

            //await db change
            await db.SaveChangesAsync();

            //set returned

            var blogVm = await GetBlogAsync(blogDb.BlogId,db);

            return blogVm;



        } 
    }
}

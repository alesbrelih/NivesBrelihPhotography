using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.DbContexts;
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
    }
}

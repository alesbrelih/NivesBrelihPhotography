using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.CategoryModels;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class CategoriesDatabase
    {


        //return admincategoryVm for all categories
        public static List<AdminCategoryVm> ReturnAllCategoriesViewModels(NbpContext _db)
        {
            try
            {
                var query = _db.Categories.OrderBy(x => x.CategoryId)
                    .Select(
                        x =>
                            new AdminCategoryVm()
                            {
                                CategoryId = x.CategoryId,
                                CategoryName = x.CategoryTitle,
                            })
                    .ToList();

                //if query returns results return query else empty list, because i dont know interaction if 0 elements found in db query
                return query.Count != 0 ? query : new List<AdminCategoryVm>();
            }
            catch
            {
                throw new Exception("Error connecting to database");
            } 

        }

        //creates new category
        public static AdminCategoryVm CreateNewCategory(AdminCategoryCreateVm category, NbpContext db)
        {
            var dbCategory = category.CreateDbModel();

            try
            {
                //get categories and check if there is a category with same name
                var query =
                    db.Categories.ToList()
                        .Where(x => x.CategoryTitle.ToLowerInvariant() == category.CategoryName.ToLowerInvariant());

                if (query.Count() != 0)
                {
                    throw new Exception("Category with the same name already exists");
                }
                else
                {
                    //no categories with same name exist
                    db.Categories.Add(dbCategory);
                    db.SaveChanges();

                    //returns created category
                    return new AdminCategoryVm() {CategoryId = dbCategory.CategoryId,CategoryName = dbCategory.CategoryTitle};
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //deletes category
        public static async void RemoveCategory(int id,NbpContext db)
        {
            //finds category
            var category = await db.Categories.FindAsync(id);

            //sets state to deleted
            db.Entry(category).State = EntityState.Deleted;

            //saves changes
            await db.SaveChangesAsync();
        }

        //edits category
        public static async void EditCategory(AdminCategoryVm category, NbpContext db)
        {
            //find category in db
            var categoryDb = await db.Categories.FindAsync(category.CategoryId);

            //edit category
            categoryDb.CategoryTitle = category.CategoryName;

            //set that it was modified
            db.Entry(categoryDb).State = EntityState.Modified;

            // save changes
            await db.SaveChangesAsync();

        }

    }
}

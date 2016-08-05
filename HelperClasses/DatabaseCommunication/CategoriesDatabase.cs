using System;
using System.Collections.Generic;
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

        private static NbpContext _db = new NbpContext();


        //return admincategoryVm for all categories
        public static List<AdminCategoryVm> ReturnAllCategoriesViewModels()
        {
            var query = _db.Categories.OrderBy(x => x.CategoryId)
                    .Select(
                        x =>
                            new AdminCategoryVm()
                            {
                                CategoryId = x.CategoryId,
                                CategoryName = x.CategoryTitle,
                                Checked = false
                            })
                    .ToList();
            //if query returns results return query else empty list, because i dont know interaction if 0 elements found in db query
            return query.Count != 0 ? query : new List<AdminCategoryVm>();

        }
    }
}

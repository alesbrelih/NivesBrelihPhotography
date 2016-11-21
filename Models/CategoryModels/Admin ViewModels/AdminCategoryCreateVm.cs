using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels
{
    public class AdminCategoryCreateVm
    {
        //category name
        public string CategoryName { get; set; }

        //creates db model and trims empty spaces
        public Category CreateDbModel()
        {
            return new Category() {CategoryTitle = this.CategoryName.Trim()};
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels
{
    public class AdminCategoryVm
    {
        //key
        public int CategoryId { get; set; }

        //category Name
        public string CategoryName { get; set; }

        //checked bool for category checkboxes on photo upload
        public bool Checked { get; set; }
    }
}

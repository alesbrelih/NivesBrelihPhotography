using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.CategoryModels;

namespace NivesBrelihPhotography.Models.BlogModels
{
    public class BlogCategory
    {

        //PK
        public int BlogCategoryId { get; set; }


        //FK CATEGORY
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


        //FK BLOG
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}

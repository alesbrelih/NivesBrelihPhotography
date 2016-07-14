using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NivesBrelihPhotography.Models.CategoryModels;

namespace NivesBrelihPhotography.Models.PhotoModels
{
    public class PhotoCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoCategoryId { get; set; }  //primary key

        
        [ForeignKey("PhotoId")]
        public virtual Photo Photo { get; set; }  //virtual photo to access props
        public int PhotoId { get; set; }  //foreign key for photo

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }  //virtual category to access props
        public int CategoryId { get; set; }  //foreign key for category

        

    }
}
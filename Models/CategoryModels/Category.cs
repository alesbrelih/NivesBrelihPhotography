﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.Models.CategoryModels
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; } //primary key

        [Required]
        public string CategoryTitle { get; set; }

        //albums of selected category
        public virtual ICollection<PhotoAlbum> Albums { get; set; }
    }
}
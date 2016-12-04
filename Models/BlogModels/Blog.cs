using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.Models.BlogModels
{
    public class Blog
    {

        //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BlogId { get; set; }

        //Blog Title
        [StringLength(50,MinimumLength = 1)]
        [Required]
        public string BlogTitle { get; set; }

        //blog date
        [Required]
        public DateTime BlogDate { get; set; }

        //blod description - usage on listing all blogs
        [Required]
        [StringLength(400,MinimumLength = 1)]
        public string BlogDescription { get; set; }

        //blog content
        [Required]
        public string Content { get; set; }

        //display photo on cover

        [ForeignKey("CoverPhoto")]
        public int CoverPhotoId { get; set; }
        public virtual Photo CoverPhoto { get; set; }

        //bool if there should be album link at end
        public bool AlbumLink { get; set; }

        //album FK
        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
        public virtual PhotoAlbum Album { get; set; }

        //virual blogcategory list

        public virtual ICollection<BlogCategory> Categories { get; set; }


        public Blog()
        {
            Categories = new List<BlogCategory>();
        }
    }
}

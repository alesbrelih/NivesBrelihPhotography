using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.CategoryModels;

namespace NivesBrelihPhotography.Models.PhotoModels
{
    public class PhotoAlbum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoAlbumId { get; set; }  //album Id

        [Required]
        public string AlbumName { get; set; }  //album name

        [Required]
        public DateTime AlbumDate{ get; set; }  //album date

        public string AlbumDescription { get; set; } //album description

        //album category // added 26.2.2017
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? CategoryId { get; set; }


        public virtual ICollection<Photo> AlbumPhotos { get; set; }  //all album photos
    }
}

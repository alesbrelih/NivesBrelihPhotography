using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual ICollection<Photo> AlbumPhotos { get; set; }  //all album photos
    }
}

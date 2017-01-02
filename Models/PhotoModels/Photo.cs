using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NivesBrelihPhotography.Models.CommentModels;

namespace NivesBrelihPhotography.Models.PhotoModels
{
    //photo class to represent the photo and what is needed in database
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; } //primary key

        [Required]
        public string PhotoTitle { get; set; }  //photo title
        
        public string PhotoText { get; set; } //photo description

        [Required]
        public string PhotoUrl { get; set; } //photo url

        [Required]
        public DateTime Uploaded { get; set; }  //upload time/date

        [Required]
        public bool CommentsEnabled { get; set; }  //enable comments for picture


        //Added 10.6.2016
        public bool IsOnFrontPage { get; set; }  //if it will be displayed on front page NOT REQUIRED

        //if on home carousel
        public bool HomeCarousel { get; set; }

        [ForeignKey("PhotoAlbumId")]
        public virtual PhotoAlbum PhotoAlbum { get; set; }  //album picture is in NOT REQUIRED
        public int? PhotoAlbumId { get; set; }

        //public bool? IsPhotoAlbumCover { get; set; }  //is album cover 3 max for 1 album

        public virtual ICollection<Comment> Comments { get; set; } //all comments

        public virtual ICollection<PhotoCategory> Categories  { get; set; } //all categories

    }
}
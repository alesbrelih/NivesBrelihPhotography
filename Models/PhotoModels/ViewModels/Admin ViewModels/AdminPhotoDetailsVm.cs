using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoDetailsVm:PhotoView
    {
        //photo id
        [Key]
        public int Id { get; set; }

        //photo description
        [Display(Name = "Photo Description")]
        public string PhotoDescription { get; set; }

        //is on portfolio
        [Display(Name = "Is On Portfolio")]
        public string OnPortfolio { get; set; }

        //album name
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        //all categories
        [Display(Name = "Categories")]
        public List<AdminCategoryVm> Categories { get; set; }

        //default constructor
        public AdminPhotoDetailsVm()
        {
            Categories = new List<AdminCategoryVm>();
        }

        //constructor when creatin VM from db model photo
        public AdminPhotoDetailsVm(Photo photoDb):this()
        {
            Id = photoDb.PhotoId;
            PhotoTitle = photoDb.PhotoTitle;
            PhotoDescription = photoDb.PhotoText;
            PhotoUrl = photoDb.PhotoUrl;
            OnPortfolio = photoDb.IsOnFrontPage ? "Yes" : "No";
            AlbumName = photoDb.PhotoAlbum == null ? "No Album" : photoDb.PhotoAlbum.AlbumName;
            photoDb.Categories.ForEach(x=>Categories.Add(new AdminCategoryVm() {CategoryId = x.CategoryId,CategoryName = x.Category.CategoryTitle}));
        }

    }
}

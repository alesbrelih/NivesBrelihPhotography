using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoCreateVm:PhotoView
    {

        [Display(Name="Photo Description")]
        public string PhotoDescription { get; set; }

        [Display(Name = "Album")]
        public int AlbumId { get; set; }

        public SelectList AllAlbums { get; set; }

        [Display(Name="On Portfolio")]
        public bool IsOnPortfolio { get; set; }

        [Display(Name = "Album Cover")]
        public bool IsAlbumCover { get; set; }

        [Display(Name="Categories")]
        public List<AdminCategoryVm> PhotoCategories { get; set; }

        public AdminPhotoCreateVm()
        {
            PhotoCategories = CategoriesDatabase.ReturnAllCategoriesViewModels();

            AllAlbums = AlbumsDatabase.ReturnAlbumsForSelectList();
        }

        public static Photo CovertToDbModel(AdminPhotoCreateVm photoCreateVm)
        {
            return new Photo()
            {
                PhotoTitle = photoCreateVm.PhotoTitle,
                PhotoText = photoCreateVm.PhotoDescription,
                PhotoUrl = photoCreateVm.PhotoUrl,
                PhotoAlbumId = photoCreateVm.AlbumId,
                IsOnFrontPage = photoCreateVm.IsOnPortfolio,
                IsPhotoAlbumCover = photoCreateVm.IsAlbumCover
                
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NivesBrelihPhotography.HelperClasses;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.CategoryModels.Admin_ViewModels;
using WebGrease.Css.Extensions;
using System.IO;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoCreateVm:PhotoView
    {

        [Display(Name="Photo Description")]
        public string PhotoDescription { get; set; }

        [Display(Name = "Album")]
        public int AlbumId { get; set; }

        public ICollection<AlbumViewBase> AllAlbums { get; set; }

        [Display(Name = "Photo")]
        [Required(ErrorMessage = "Select photo to upload")]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Display(Name="On Portfolio")]
        public bool IsOnPortfolio { get; set; }

        [Display(Name = "Album Cover")]
        public bool IsAlbumCover { get; set; }

        [Display(Name="Categories")]
        public List<AdminCategoryVm> PhotoCategories { get; set; }

        public AdminPhotoCreateVm()
        {
            //PhotoCategories = CategoriesDatabase.ReturnAllCategoriesViewModels();

            //AllAlbums = AlbumsDatabase.ReturnAlbumsForSelectList();
        }

        //contects vm to db model
        public Photo CovertToDbModel()
        {

                var photoDb = new Photo()
                {
                    PhotoTitle = this.PhotoTitle,
                    PhotoText = this.PhotoDescription,
                    PhotoUrl = "/Images/Photos/"+Path.GetFileName(this.PhotoFile.FileName),
                    IsOnFrontPage = this.IsOnPortfolio,
                    IsPhotoAlbumCover = this.IsAlbumCover,
                    Categories = new List<PhotoCategory>(),
                    Uploaded = DateTime.Now


                };

                if (this.AlbumId == -1)
                {
                    photoDb.PhotoAlbumId = null;
                }
                else
                {
                    photoDb.PhotoAlbumId = this.AlbumId;
                }

                this.PhotoCategories.Where(x => x.Checked).ForEach(x => photoDb.Categories.Add(new PhotoCategory() { CategoryId = x.CategoryId }));


                return photoDb;


           
        }

    }
}

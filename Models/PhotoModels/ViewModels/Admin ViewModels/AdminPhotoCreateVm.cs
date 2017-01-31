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


        public int AlbumId { get; set; }

        public HttpPostedFileBase PhotoFile { get; set; }


        public bool IsOnPortfolio { get; set; }


        public bool IsAlbumCover { get; set; }

        public List<string> PhotoCategories { get; set; }

        public bool HomeCarousel { get; set; }


        public AdminPhotoCreateVm()
        {
            PhotoCategories = new List<string>();
        }

        //contects vm to db model
        public Photo CovertToDbModel()
        {

                var photoDb = new Photo()
                {
                    PhotoTitle = this.PhotoTitle,
                    PhotoText = "",
                    IsOnFrontPage = this.IsOnPortfolio,
                    Categories = new List<PhotoCategory>(),
                    Uploaded = DateTime.Now,
                    PhotoUrl = this.PhotoUrl


                };

                if (this.AlbumId == -1)
                {
                    photoDb.PhotoAlbumId = null;
                }
                else
                {
                    photoDb.PhotoAlbumId = this.AlbumId;
                }

                this.PhotoCategories.ForEach(x => photoDb.Categories.Add(new PhotoCategory() { CategoryId = int.Parse(x) }));


                return photoDb;


           
        }

    }
}

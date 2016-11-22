using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoEditVm:PhotoView
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }


        public bool IsOnPortfolio { get; set; }


        public bool IsAlbumCover { get; set; }

        public List<string> PhotoCategories { get; set; }


        public AdminPhotoEditVm()
        {
            PhotoCategories = new List<string>();
        }

        public void ChangeProps(Photo photo,bool isAlbumCover)
        {
            Id = photo.PhotoId;
            AlbumId = photo.PhotoAlbumId ?? -1;
            PhotoTitle = photo.PhotoTitle;
            IsOnPortfolio = photo.IsOnFrontPage;
            photo.Categories.ForEach(x=>PhotoCategories.Add(x.CategoryId.ToString()));
            PhotoUrl = photo.PhotoUrl;
            IsAlbumCover = isAlbumCover;


        }


    }
}

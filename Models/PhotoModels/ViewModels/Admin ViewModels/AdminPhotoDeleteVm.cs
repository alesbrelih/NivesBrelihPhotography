using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoDeleteVm:PhotoView
    {
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Photo Description")]
        public string PhotoDescription { get; set; }

        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        [Display(Name = "On Front Page")]
        public string IsOnPortfolio { get; set; }

        //creates adminphotodeleteVm
        public static AdminPhotoDeleteVm CreateAdminPhotoDeleteVm(Photo photoDb)
        {
            var photoVm = new AdminPhotoDeleteVm()
            {
                PhotoId = photoDb.PhotoId,
                PhotoUrl = photoDb.PhotoUrl,
                PhotoTitle = photoDb.PhotoTitle
            };

            photoVm.PhotoDescription = photoDb.PhotoText==null ? "No Description" : photoDb.PhotoText;
            photoVm.AlbumName = photoDb.PhotoAlbum == null ? "None" : photoDb.PhotoAlbum.AlbumName;
            photoVm.IsOnPortfolio = photoDb.IsOnFrontPage ? "Yes" : "No";

            return photoVm;
        }
    }
}

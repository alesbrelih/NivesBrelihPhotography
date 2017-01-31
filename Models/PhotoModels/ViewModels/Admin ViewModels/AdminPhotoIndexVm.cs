using System;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoIndexVm:PhotoView
    {
        public int PhotoId { get; set; }

        public string Album { get; set; }

        public bool OnPortfolio { get; set; }
        
        public DateTime Uploaded { get; set; }

        public bool HomeCarousel { get; set; }

        public string UploadedString => Uploaded.ToShortDateString();
    }
}

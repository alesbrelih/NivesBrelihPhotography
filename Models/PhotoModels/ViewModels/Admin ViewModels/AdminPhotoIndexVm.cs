using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels
{
    public class AdminPhotoIndexVm:PhotoView
    {
        public int PhotoId { get; set; }

        public string Album { get; set; }

        public bool OnPortfolio { get; set; }
        
        public DateTime Uploaded { get; set; }

        public string UploadedString => Uploaded.ToShortDateString();
    }
}

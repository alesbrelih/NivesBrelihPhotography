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
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Album Name")]
        public string Album { get; set; }

        [Display(Name = "Show on Portfolio")]
        public bool OnPortfolio { get; set; }

        
        public DateTime Uploaded { get; set; }

        [Display(Name = "Upload Date")]
        public string UploadedString => Uploaded.ToShortDateString();
    }
}

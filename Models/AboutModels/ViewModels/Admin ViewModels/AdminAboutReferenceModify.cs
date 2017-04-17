using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels
{
    public class AdminAboutReferenceModify
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? LeadPhotoId { get; set; }

        public ICollection<string> ReferencePhotos { get; set; }

        //empty constructor
        public AdminAboutReferenceModify()
        {
            ReferencePhotos = new List<string>();
        }

    }
}

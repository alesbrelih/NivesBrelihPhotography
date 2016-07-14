using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;


namespace NivesBrelihPhotography.Models.AboutModels.ViewModels
{
    public class ReferenceDetailsView:ReferenceView
    {

        public string Description { get; set; }

        public List<PhotoView> ReferencePhotos { get; set; }

        public ReferenceDetailsView()
        {
            ReferencePhotos = new List<PhotoView>();
        }

        public ReferenceDetailsView(Reference referenceDb):this()
        {
            ReferenceId = referenceDb.ReferenceId;
            Title = referenceDb.ReferenceTitle;
            Description = referenceDb.ReferenceDescription;
            foreach (var photoRef in referenceDb.Photos.OrderBy(x=>x.PhotoId).Take(10))
            {
                ReferencePhotos.Add(new PhotoView() {PhotoTitle = photoRef.Photo.PhotoTitle,PhotoUrl = photoRef.Photo.PhotoUrl});
            }
        }

    }
}

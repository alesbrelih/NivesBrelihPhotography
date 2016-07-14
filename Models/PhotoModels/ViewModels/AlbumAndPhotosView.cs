using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{
    public class AlbumAndPhotosView
    {
        public AlbumView AlbumDescription{ get; set; }  //album desription with title/date/description

        public IEnumerable<PhotoView> AlbumPhotos { get; set; }  //album photos

        public AlbumAndPhotosView(AlbumView albumDesc, IEnumerable<PhotoView> photos)
        {
            AlbumDescription = albumDesc;
            AlbumPhotos = photos;
        }
    }
}

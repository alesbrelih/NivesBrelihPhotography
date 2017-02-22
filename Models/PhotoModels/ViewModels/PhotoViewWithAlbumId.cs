using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{
    public class PhotoViewWithAlbumId:PhotoView
    {
        public int? AlbumId { get; set; }
    }
}

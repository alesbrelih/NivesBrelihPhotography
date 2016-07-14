using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{
    //PhotoView for JSON GET so object returned is small enough
    public class PhotoView
    {
        //photo url
        public string PhotoUrl { get; set; }

        //photo title
        public string PhotoTitle { get; set; }
    }
}

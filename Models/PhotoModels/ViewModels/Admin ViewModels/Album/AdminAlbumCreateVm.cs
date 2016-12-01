using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels.Album
{
    public class AdminAlbumCreateVm
    {


        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int AlbumCover { get; set; }
        public List<string> Photos { get; set; }

        public AdminAlbumCreateVm()
        {
            Photos = new List<string>();
        }
    }
}

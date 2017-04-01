using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels.Album
{
    public class AdminAlbumModifyVm
    {


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public int? CoverPhoto { get; set; }

        public List<string> Photos { get; set; }


        //empty constructor
        public AdminAlbumModifyVm()
        {
            Photos = new List<string>();
        }


    }
}

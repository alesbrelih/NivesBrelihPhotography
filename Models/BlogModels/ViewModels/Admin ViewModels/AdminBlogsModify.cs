using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.BlogModels.ViewModels.Admin_ViewModels
{
    public class AdminBlogsModify
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public int CoverPhoto { get; set; }

        public bool Album { get; set; }

        public string AlbumId { get; set; }

        public List<string> Categories { get; set; }

        public AdminBlogsModify()
        {
            Categories = new List<string>();
        }
    }
}

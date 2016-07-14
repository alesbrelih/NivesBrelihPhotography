using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{
    public class AlbumView
    {
        public int AlbumId { get; set; }

        public string AlbumName { get; set; } //album name

        public string AlbumDate{ get; set; }  //album date

        public string AlbumText { get; set; }  //album text

        public AlbumView(int id,string name, DateTime date, string text)
        {
            AlbumId = id;
            AlbumDate = date.ToShortDateString();
            AlbumName = name;
            AlbumText = text;
        }

    }
}

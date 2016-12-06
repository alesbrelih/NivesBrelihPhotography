using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{

    public class AlbumViewBase
    {
        public int AlbumId { get; set; }

        public string AlbumName { get; set; } //album name

        public string AlbumDate { get; set; }  //album date

        public AlbumViewBase()
        {
            
        }

        public AlbumViewBase(int id, string name, DateTime date):this()
        {
            AlbumId = id;
            AlbumName = name;

            //current short date format
            DateTimeFormatInfo currentDateFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
            AlbumDate = date.ToString(currentDateFormat.ShortDatePattern);
        }
    }

    public class AlbumView:AlbumViewBase
    {
       



        public string AlbumText { get; set; }  //album text
        public string AlbumCover { get; set; }  //cover photo for album

        public AlbumView(int id,string name, DateTime date, string text,string albumCover):base(id,name, date)
        {
            
            AlbumText = text;
            AlbumCover = albumCover;
        }

    }
}

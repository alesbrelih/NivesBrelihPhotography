using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels.ViewModels
{
    /// <summary>
    ///view class for albums index page, to show album cover, name and date of the album
    /// </summary>
    public class PhotoAlbumView
    {
        //photo album id
        public int PhotoAlbumId { get; set; }

        //photo album name
        public string AlbumName { get; set; }

        //album photo src
        public string AlbumPhotoUrl { get; set; }

        //album shooting start
        public DateTime AlbumDate { get; set; }

        //album date month as string
        public string AlbumDateMonthAsString { get {
                switch (AlbumDate.Month)
                {
                    case 1:
                        return "January " + AlbumDate.Year;
                    case 2:
                        return "February " + AlbumDate.Year;
                    case 3:
                        return "March " + AlbumDate.Year;
                    case 4:
                        return "April " + AlbumDate.Year;
                    case 5:
                        return "May " + AlbumDate.Year;
                    case 6:
                        return "June " + AlbumDate.Year;
                    case 7:
                        return "July " + AlbumDate.Year;
                    case 8:
                        return "August " + AlbumDate.Year;
                    case 9:
                        return "September " + AlbumDate.Year;
                    case 10:
                        return "October " + AlbumDate.Year;
                    case 11:
                        return "November " + AlbumDate.Year;
                    case 12:
                        return "December " + AlbumDate.Year;
                }
            return "";
        } }


        //return album date string (converts to may/maj)
        public string ReturnAlbum()
        {
            if (AlbumDate != null)
            {
                switch (AlbumDate.Month)
                {
                    case 1:
                        return "January " + AlbumDate.Year;
                    case 2:
                        return "February " + AlbumDate.Year;
                    case 3:
                        return "March " + AlbumDate.Year;
                    case 4:
                        return "April " + AlbumDate.Year;
                    case 5:
                        return "May " + AlbumDate.Year;
                    case 6:
                        return "June " + AlbumDate.Year;
                    case 7:
                        return "July " + AlbumDate.Year;
                    case 8:
                        return "August " + AlbumDate.Year;
                    case 9:
                        return "September " + AlbumDate.Year;
                    case 10:
                        return "October " + AlbumDate.Year;
                    case 11:
                        return "November " + AlbumDate.Year;
                    case 12:
                        return "December " + AlbumDate.Year;
                }
            }
            return "";
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;
using WebGrease.Css.Extensions;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class AlbumsDatabase
    {


        //returns select list to select album on photo create/edit
        public static ICollection<AlbumViewBase> ReturnAlbumsForSelectList(NbpContext _db)
        {
            //selectlist items from db
            var albums = new List<AlbumViewBase>() {new AlbumViewBase() { AlbumName = "", AlbumId = -1, AlbumDate  = DateTime.Now.ToShortDateString()}};

            _db.PhotoAlbums.OrderBy(x => x.AlbumName).ForEach(x=>albums.Add(new AlbumViewBase(x.PhotoAlbumId,x.AlbumName,x.AlbumDate)));



            return albums;
        }

    }
}

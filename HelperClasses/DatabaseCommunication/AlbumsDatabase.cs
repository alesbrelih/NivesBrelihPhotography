using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NivesBrelihPhotography.DbContexts;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class AlbumsDatabase
    {
        private static NbpContext _db = new NbpContext();

        //returns select list to select album on photo create/edit
        public static SelectList ReturnAlbumsForSelectList()
        {
            //selectlist items from db
            var selectListItems =
                _db.PhotoAlbums.OrderBy(x => x.AlbumName)
                    .Select(x => new SelectListItem() {Text = x.AlbumName, Value = x.PhotoAlbumId.ToString()})
                    .ToList();

            //selectlist items in list
            var selectList = new SelectList(selectListItems,"Value","Text");

            return selectList;
        }
    }
}

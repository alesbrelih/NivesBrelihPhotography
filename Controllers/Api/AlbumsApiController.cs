using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.HelperClasses.DatabaseCommunication;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels.Admin_ViewModels.Album;

namespace NivesBrelihPhotography.Controllers.Api
{
    [RoutePrefix("Albums")]
    public class AlbumsController : ApiController
    {
        private NbpContext _db = new NbpContext();

        [HttpGet]
        //gets list of albums
        public async Task<HttpResponseMessage> GetAlbums()
        {
            try
            {
                var query = await AlbumsDatabase.ReturnAlbumsForSelectList(_db);
                return Request.CreateResponse(HttpStatusCode.OK, query);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
            

        }

        [HttpGet]
        //get single album by its id
        public async Task<HttpResponseMessage> GetAlbum([FromUri] int id)
        {
            try
            {
                //try to get album vm from db
                var albumVm = await AlbumsDatabase.GetAlbumByIdAsync(id, _db);
                return Request.CreateResponse(HttpStatusCode.OK, albumVm);
            }
            catch (Exception ex)
            {
                //return exception
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        //edits existing album data in db
        public async Task<HttpResponseMessage> EditAlbum([FromBody] AdminAlbumModifyVm album )
        {
            //check if album null
            if (album == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No data was send to the server.");
            }
            try
            {
                await AlbumsDatabase.EditAlbumAsync(album, _db);
                return Request.CreateResponse(HttpStatusCode.OK, "Album edited successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Selected cover photo is invalid")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        //creates album in db
        public async Task<HttpResponseMessage> CreateAlbum(AdminAlbumCreateVm album)
        {
            //check if there is any data in body post
            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No data was recieved to the server.");
            }
            try
            {
                //try to create album
                var albumDb = await AlbumsDatabase.CreateAlbumAsync(album, _db);
                return Request.CreateResponse(HttpStatusCode.OK, albumDb);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        //deletes album from db
        //delete photos represents if its photos should be deleted aswell
        public async Task<HttpResponseMessage> DeleteAlbum([FromUri] int? id, [FromUri] bool deletePhotos=false)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No album id provided");
            }
            try
            {
                await AlbumsDatabase.DeleteAlbumAsync((int)id, deletePhotos, _db);
                if (deletePhotos)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Album and its photos successfully deleted");
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Album successfully deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //dispose override
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}

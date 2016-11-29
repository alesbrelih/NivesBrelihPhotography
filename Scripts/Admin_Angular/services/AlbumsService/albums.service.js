// ----- ALBUMS SERVICE ------ //

(function(angular) {
    
    //register main module
    var app = angular.module("adminApp.services");

    //factory controller
    function albumsFactoryController($http,toastr) {
        
        //returned factory
        var albumsFactory = {};

        // --- privates --- //
        var _albums = [];

        //---- properties ----- //
        albumsFactory.Albums = _albums;



        // ---- methods ---- //

        //gets all albums
        albumsFactory.GetAlbums = function (promise) {

        
           var q = $http.get("/api/albums")
                .then(function (success) {

                    //clear if refresh
                    if (_albums.length > 0) {
                        while (_albums.length>0) {
                            _albums.pop();
                        }
                    }

                success.data.forEach(function(item) {
                    _albums.push(item);
                });
            }, function(err) {
                    if (err) {
                        console.log(err.data);
                    }
            });

            if (promise) {
                return q;
            }
        };

        //delete album
        albumsFactory.DeleteAlbum = function (album, deletePhotos) {

            //api call
            $http.delete("/api/albums", {
                    params: {
                        "id": album.Id,
                        "deletePhotos": deletePhotos
                    }
                })
                .then(function() {
                    toastr.success("Album successfully deleted");
                    var albumIndex = _albums.indexOf(album);
                    _albums.splice(albumIndex);
                }).catch(function (err) {
                toastr.error(err.data, "Error");
                console.log(err);
            });
        }

        return albumsFactory;
    }

    albumsFactoryController.$inject = ["$http","toastr"];
    //register factory on angular module
    app.factory("AlbumsService", albumsFactoryController);

})(window.angular);
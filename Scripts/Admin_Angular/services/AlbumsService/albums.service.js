// ----- ALBUMS SERVICE ------ //

(function(angular) {
    
    //register main module
    var app = angular.module("adminApp.services");

    //factory controller
    function albumsFactoryController($http,toastr,$state) {
        
        //returned factory
        var albumsFactory = {};

        // --- privates --- //
        var albums = [];


        //---- properties ----- //
        albumsFactory.Albums = albums;

        // ---- methods ---- //

        //gets all albums
        albumsFactory.GetAlbums = function (promise) {

        
           var q = $http.get("/api/albums")
                .then(function (success) {

                    //clear if refresh
                    if (albums.length > 0) {
                        while (albums.length>0) {
                            albums.pop();
                        }
                    }

                success.data.forEach(function(item) {
                    albums.push(item);
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

        //get single album and returns promise for resolve edit
        albumsFactory.GetAlbum = function(id) {
            return $http.get("/api/albums", {
                params: {
                    "id": id
                }
            });
        }

        //creates album
        albumsFactory.CreateAlbum = function (album) {

            //post data to api
            $http.post("/api/albums", album)
                .then(function (success) {

                    //on success show toastr, add album to albusm array and redirect to main albums page if needed
                    toastr.success("Album successfully created", "Success");
                    
                    if ($state.current.name === "albums-add") {
                        $state.go("albums");
                    } else {
                        albums.push(success.data);
                    }

                },
                function (err) {
                    //catch err and show it
                    toastr.error(err.data, "Error");
                    console.log(err.data);
                });
        }

        //edits album
        albumsFactory.EditAlbum = function (album) {

            //call api with put request

            $http.put("/api/albums", album)
                .then(function () {
                    //album successfully edited -> show toastr and change state to albums if on albums-edit
                    toastr.success("Album successfully edited");
                    if ($state.current.name === "albums-edit") {
                        $state.go("albums");
                    }
                }, function (err) {
                    //err catch
                    toastr.error(err.data, "Error");
                });
        }

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
                    var albumIndex = albums.indexOf(album);
                    albums.splice(albumIndex);
                }).catch(function (err) {
                toastr.error(err.data, "Error");
                console.log(err);
            });
        }


        //return singleton
        return albumsFactory;
    }

    albumsFactoryController.$inject = ["$http", "toastr", "$state"];


    //register factory on angular module
    app.factory("AlbumsService", albumsFactoryController);

})(window.angular);
// ----- ALBUMS SERVICE ------ //

(function(angular) {
    
    //register main module
    var app = angular.module("adminApp.services");

    //factory controller
    function albumsFactoryController($http) {
        
        //returned factory
        var albumsFactory = {};

        // --- privates --- //
        var _albums = [];

        //---- properties ----- //
        albumsFactory.Albums = _albums;



        // ---- methods ---- //

        //gets all albums
        albumsFactory.GetAlbums = function() {
            $http.get("/api/albums")
                .then(function(success) {
                success.data.forEach(function(item) {
                    _albums.push(item);
                });
            }, function(err) {
                    if (err) {
                        console.log(err.data);
                    }
                });
        };

        return albumsFactory;
    }

    albumsFactoryController.$inject = ["$http"];
    //register factory on angular module
    app.factory("AlbumsService", albumsFactoryController);

})(window.angular);
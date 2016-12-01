// -- admin albums add component moodule -- //
(function(angular) {

    //main app / module
    var app = angular.module("adminApp");

    //controller function
    function albumsAddController(PhotosService,AlbumsService,$uibModal) {

        //current scope
        var vm = this;

        //get photos
        PhotosService.GetPhotos();

        //set photos
        vm.Photos = PhotosService.Photos;

        //current album
        vm.Album = {
            Name: "",
            Description: "",
            AlbumCover: null,
            Photos: []
        };


        //creates album
        vm.CreateAlbum = function () {

            //set modal type
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "upload";
                    },
                    entry: function () {
                        return "album";
                    }
                }
            });

            //depends on modal result to create album
            modal.result.then(function() {
                AlbumsService.CreateAlbum(vm.Album);
            });
            
        }


    }

    albumsAddController.$inject = ["PhotosService", "AlbumsService","$uibModal"];

    //register component
    app.component("adminAlbumsAdd", {
        controller: albumsAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-albums/add/admin.albums.add.component.html"
    });

})(window.angular);
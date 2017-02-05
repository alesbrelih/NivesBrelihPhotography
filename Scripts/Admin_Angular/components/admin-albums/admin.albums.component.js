// --- ADMIN ALBUMS SECTION MODULE --- //
(function(angular) {
    
    //reference to main app / module
    var app = angular.module("adminApp");

    //component controller
    function adminAlbumsController(AlbumsService,$uibModal) {
        
        //current scope
        var vm = this;

        //pagination properties
        vm.currentPage = 1;
        vm.pageSize = 10;

        //refreshes albums
        AlbumsService.GetAlbums(true).then(function() {
            //sets vm to albums list
            vm.Albums = AlbumsService.Albums;
            vm.allPages = Math.ceil(vm.Albums.length / vm.pageSize);
        });

        //delete selected album from db
        vm.DeleteAlbum = function(album) {


            //modal to accept deletition of model
            var modal = $uibModal.open({
                component: "abModalView",
                size: "md",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "album";
                    }
                }
            });

            //modal accepted
            modal.result.then(function (deletePhotos) {
                AlbumsService.DeleteAlbum(album, deletePhotos);
            });


        }


    }

    //inject service
    adminAlbumsController.$inject = ["AlbumsService","$uibModal"];

    //register component
    app.component("adminAlbums", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-albums/admin.albums.component.html",
        controller: adminAlbumsController,
        controllerAs: "vm"
    });

})(window.angular);
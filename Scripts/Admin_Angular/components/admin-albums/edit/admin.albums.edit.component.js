// ---- ADMIN ALBUM EDIT MODULE --- //
(function(angular) {
    
    // reference to main app / module
    var app = angular.module("adminApp");

    // admin album edit controller
    function albumEditComponentController(AlbumsService,$q,PhotosService,CategoriesService) {
        
        //current scope
        var vm = this;

        //function that gets all data
        function getAllData() {
            var p1 = vm.album; //album promise data
            var p2 = PhotosService.GetPhotos();

            $q.all([p1, p2]).then(function (success) {
                var album = success[0].data;
                var photos = PhotosService.Photos;

                vm.Album = album;
                vm.Photos = photos;
                
                if (vm.Album.CategoryId && vm.Album.CategoryId != null) {
                    vm.Album.CategoryId = vm.Album.CategoryId.toString();
                }

                },
            function(err) {
                console.log(err);
            });
        }
        vm.$onInit = function () {
            vm.Categories = CategoriesService.Categories;
            //get data
            getAllData();
            
        }
        

        //edit album
        vm.EditAlbum = function() {
            AlbumsService.EditAlbum(vm.Album);
        }

        //cb function for when adding photo
        vm.CloseAddPhoto = function() {
            vm.addPhoto = false;
        }


    }

    albumEditComponentController.$inject = ["AlbumsService", "$q","PhotosService","CategoriesService"];

    //register component
    app.component("adminAlbumsEdit", {
        controller: albumEditComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-albums/edit/admin.albums.edit.component.html",
        bindings: {
            album:"="
        }
});


})(window.angular);
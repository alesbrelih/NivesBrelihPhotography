// -- admin albums add component moodule -- //
(function(angular) {

    //main app / module
    var app = angular.module("adminApp");

    //controller function
    function albumsAddController(PhotosService,AlbumsService,CategoriesService,$uibModal) {

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

        vm.$onInit = function() {
            vm.Categories = CategoriesService.Categories;
        }


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

        //cb function for when adding photo, to close add photo component
        vm.CloseAddPhoto = function() {
            vm.addPhoto = false;
        }

        //create category
        vm.CreateCategory = function () {

            //hide new category input if category created.
            CategoriesService.CreateCategory(vm.Category, true).then(function (success) {
                vm.newCategoryForm = false;
            }, function (err) {
                console.log(err);
            });
        }


    }

    albumsAddController.$inject = ["PhotosService", "AlbumsService","CategoriesService","$uibModal"];

    //register component
    app.component("adminAlbumsAdd", {
        controller: albumsAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-albums/add/admin.albums.add.component.html"
    });

})(window.angular);
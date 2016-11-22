﻿// ------------- PHOTOS ADD ADMIN COMPONENT ------- //

(function(angular) {

    //main module / app
    var app = angular.module("adminApp");

    //photos add admin controller
    function photosAdminAddController(PhotosService,AlbumsService,CategoriesService,$scope,$uibModal) {

        //current scope
        var vm = this;

        // get all albums
        AlbumsService.GetAlbums();

        //get all categories
        CategoriesService.GetCategories();

        // ---- properties ---- //
        vm.Albums = AlbumsService.Albums;
        vm.Categories = CategoriesService.Categories;
        vm.Category = {
            CategoryName:""
            };
        vm.newCategoryForm = false;


        //photo
        vm.Photo = {
            PhotoTitle: "",
            IsOnPortfolio: false,
            AlbumId: -1,
            IsAlbumCover: false,
            PhotoFile: null,
            PhotoCategories:[]
            
        }

        //upload photo
        vm.UploadPhoto = function () {

            //modal for confirmation about uploading to db
            var modalUpload = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "upload";
                    },
                    entry: function () {
                        return "photo";
                    }
                }
            });

            modalUpload.result.then(function (success) {

                //confirmation accepted
                if (vm.Photo.IsAlbumCover) { //if overrides curent album cover

                    //another modal for user to accept if he didnt see
                    var modalAlbumCover = $uibModal.open({
                        component: "abModalView",
                        size: "sm",
                        resolve: {
                            type: function() {
                                return "changeAlbumCover";
                            }
                        }
                    });
                    modalAlbumCover.result.then(function (success) {
                        //both modals accepted
                        PhotosService.UploadPhoto(vm.Photo);
                    }, function(err) {
                        console.log(err);
                    });
                } else {

                    //wasnt album cover and first modal was accepted
                    PhotosService.UploadPhoto(vm.Photo);
                }
            });

        }

        //create category
        vm.CreateCategory = function () {

            //hide new category input if category created.
            CategoriesService.CreateCategory(vm.Category,true).then(function(success) {
                vm.newCategoryForm = false;
            },function(err) {
                console.log(err);
            });
        }



        //watch for selection change on albums dropdown
        //and uncheck album cover if selected album in null
        $scope.$watch("vm.Photo.AlbumId", function() {
            if (vm.Photo.AlbumId == "-1") {
                vm.Photo.IsAlbumCover = false;
            }
        });
    }

    //inject service
    photosAdminAddController.$inject = ["PhotosService","AlbumsService","CategoriesService","$scope","$uibModal"];

    //register component
    app.component("adminPhotosAdd", {
        controller: photosAdminAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/add/admin.photos.add.component.html"
    });

})(window.angular);
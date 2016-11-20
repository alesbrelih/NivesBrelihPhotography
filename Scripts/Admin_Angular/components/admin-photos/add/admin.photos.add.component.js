// ------------- PHOTOS ADD ADMIN COMPONENT ------- //

(function(angular) {

    //main module / app
    var app = angular.module("adminApp");

    //photos add admin controller
    function photosAdminAddController(PhotosService,AlbumsService,CategoriesService) {

        //current scope
        var vm = this;

        // get all albums
        AlbumsService.GetAlbums();

        //get all categories
        CategoriesService.GetCategories();

        // ---- properties ---- //
        vm.Albums = AlbumsService.Albums;
        vm.Categories = CategoriesService.Categories;


        //photo
        vm.Photo = {
            Title: "Test photo",
            OnPortfolio: false,
            AlbumId: 4
        }

        //upload photo
        vm.UploadPhoto = function() {
            PhotosService.UploadPhoto(vm.Photo);
        }
        vm.CheckAlbums = function() {
            console.log(vm.Albums);
            console.log(vm.Categories);
        }
    }

    //inject service
    photosAdminAddController.$inject = ["PhotosService","AlbumsService","CategoriesService"];

    //register component
    app.component("adminPhotosAdd", {
        controller: photosAdminAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/add/admin.photos.add.component.html"
    });

})(window.angular);
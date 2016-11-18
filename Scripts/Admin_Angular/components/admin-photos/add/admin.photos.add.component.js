// ------------- PHOTOS ADD ADMIN COMPONENT ------- //

(function(angular) {

    //main module / app
    var app = angular.module("adminApp");

    //photos add admin controller
    function photosAdminAddController(PhotosService) {

        //current scope
        var vm = this;

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
    }


    //inject service
    photosAdminAddController.$inject = ["PhotosService"];

    //register component
    app.component("adminPhotosAdd", {
        controller: photosAdminAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/add/admin.photos.add.component.html"
    });

})(window.angular);
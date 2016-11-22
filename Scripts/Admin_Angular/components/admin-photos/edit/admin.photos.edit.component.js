// ------ ADMIN PHOTOS EDIT COMPONENT ----- //
(function(angular) {

    //reference to main module/app
    var app = angular.module("adminApp");

    function adminEditComponentController(PhotosService) {

        var vm = this;

        vm.Photo = PhotosService.CurrentPhoto;

        vm.testPhoto = function() {
            console.log(vm);
        }

    }

    //register component
    app.component("adminPhotosEdit", {
        templateUrl:"/Scripts/Admin_Angular/templates/components/admin-photos/edit/admin.photos.edit.component.html",
        controller: adminEditComponentController,
        controllerAs: "vm"

    });

})(window.angular);
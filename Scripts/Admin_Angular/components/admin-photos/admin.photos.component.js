// admin photos component 

(function(angular) {

    //get main angular module
    var app = angular.module("adminApp");

    //admin photos controller
    function adminPhotosController(PhotosService) {
        
        //current instance of controller
        var vm = this;

        PhotosService.GetPhotos();

        //get all photos
        vm.Photos = PhotosService.Photos;

        vm.showPhotos = function() {
            console.log(vm.Photos);
        }

        //vm.RemovePhoto = function(x) {
        //    PhotosService.RemovePhoto(x);
        //}


    }

    //injecting dependencies for possible minification of javascipt files
    adminPhotosController.$inject = ["PhotosService"];

    //register component on angular module
    app.component("adminPhotos", {
        controller: adminPhotosController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/admin.photos.component.html"
    });

})(window.angular);
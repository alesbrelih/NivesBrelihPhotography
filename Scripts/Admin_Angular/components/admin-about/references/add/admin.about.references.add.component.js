// ---- ADMIN ABOUT REFERENCES ADD COMPONENT MODULE --- //

(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //admin about reference add controller
    function aboutReferenceAddController(ProfileService) {
        //Current scope
        var vm = this;
        vm.pageSize = 5;
        vm.searchText = "";
        vm.addPhoto = false;

        //reference model data will be bound to
        vm.Reference = {
            Title: "",
            Description: "",
            ReferencePhotos : []
        }



        //gets photos
        ProfileService.PhotosForReferencePage(vm.pageSize).then(function(success) {
            vm.Photos = success.photos;
        });

        vm.CreateReference = function() {
            ProfileService.CreateReference(vm.Reference);
        }

        vm.test = function() {
            console.log(vm.Reference);
        }

    }
    //inject needed services
    aboutReferenceAddController.$inject = ["ProfileService"];

    //register component
    app.component("adminAboutReferencesAdd", {
        controller: aboutReferenceAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/references/add/admin.about.references.add.component.html"
    });

})(window.angular);
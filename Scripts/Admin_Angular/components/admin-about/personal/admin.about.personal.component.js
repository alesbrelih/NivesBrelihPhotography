// ------------- ADMIN PERSONAL COMPONENT MODULE --------- //


(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //admin personal controller
    function adminPersonalController(ProfileService) {
        var vm = this;

        //get current profile information
        ProfileService.RefreshProfile()
            .then(function() {
                vm.Profile = ProfileService.GetProfile();
            console.log(vm.Profile);
                if (vm.Profile.ProfilePicture) {
                    vm.previewPhoto = vm.Profile.ProfilePicture;
                }
            })
            .catch(function (err) {
            console.log(err);
            });

        //updates profile
        vm.UpdateProfile = function () {
            if (vm.Profile) {
                ProfileService.UpdatePersonalInformation(vm.Profile);
            }
            
        }
    }

    adminPersonalController.$inject = ["ProfileService"];

    //register component
    app.component("adminAboutPersonal", {
        controller: adminPersonalController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/personal/admin.about.personal.controller.html"
    });

})(window.angular);
(function(angular) {

    //app
    var app = angular.module("adminApp");

    //controller
    function componentController(WorkingWithService) {

        var vm = this;

        vm.$onInit = function() {
            vm.workingWithList = WorkingWithService.List;

        }

    }

    componentController.$inject = ["WorkingWithService"];

    //register component
    app.component("adminAboutWorkingWith", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/working-with/admin.about.working.with.component.html",
        controller: componentController,
        controllerAs: "vm"
    });

})(window.angular);
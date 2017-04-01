
(function (angular) {
    //main app ref
    var app = angular.module("adminApp");

    //app controller
    function workingWithAddController(WorkingWithService) {

        var vm = this;

        //props
        var importanceRange = 10;

        //init
        vm.$onInit = function() {

            //set importance range array
            vm.ImportanceRange = [];
            for (var i = 0; i < importanceRange; i++) {
                vm.ImportanceRange.push(i + 1);
            }
        }

        //upload working with
        vm.AddWorkingWith = function() {
            WorkingWithService.Add(vm.WorkingWith);
        }


    }

    workingWithAddController.$inject = ["WorkingWithService"];

    //register component
    app.component("adminAboutWorkingWithAdd", {
        controller: workingWithAddController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/working-with/add/admin.about.working.with.add.component.html"
        
    })
})(window.angular);

(function (angular) {
    //main app ref
    var app = angular.module("adminApp");

    //app controller
    function workingWithEditController(WorkingWithService) {

        var vm = this;

        //props
        var importanceRange = 10;

        //init
        vm.$onInit = function () {     

            //set importance range array
            vm.ImportanceRange = [];
            for (var i = 0; i < importanceRange; i++) {
                vm.ImportanceRange.push(i + 1);
            }

            vm.item.Importance = vm.item.Importance.toString(); //need to convert int to string else its ignored
            vm.WorkingWith = vm.item;
            vm.previewPhoto = vm.WorkingWith.PhotoUrl;

        }

        //edit working with
        vm.EditWorkingWith = function () {
            WorkingWithService.Edit(vm.WorkingWith);
        }




    }

    workingWithEditController.$inject = ["WorkingWithService"];

    //register component
    app.component("adminAboutWorkingWithEdit", {
        controller: workingWithEditController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/working-with/edit/admin.about.working.with.edit.component.html",
        bindings: {
            item:"<"
        }
    });
})(window.angular);
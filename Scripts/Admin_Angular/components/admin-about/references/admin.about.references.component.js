// -- ADMIN ABOUT REFERENCES MODULE COMPONENT --- //
(function(angular) {

    //reference to main app / module
    var app = angular.module("adminApp");

    //admin references controller
    function adminReferencesController(ProfileService,$uibModal) {

        //current scope
        var vm = this;
        vm.References = [];

        // table helpers
        vm.orderBy = "Title";
        vm.pageSize = 5;
        vm.allPages = null;
        vm.currentPage = 1;

        //gets references from service
        ProfileService.RefreshReferences()
            .then(function() {
                vm.References = ProfileService.GetReferences();
                vm.allPages = Math.ceil(vm.References.length / vm.pageSize);
            })
            .catch(function(err) {
            //catch err
            console.log(err);
            });

        // Deletes reference
        vm.DeleteReference = function (reference) {

            //modal to confirm deletition
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "reference";
                    }
                }
            });

            //action depending on modal result
            modal.result.then(function() {
                    ProfileService.DeleteReference(reference);
                })
                .catch(function(err) {
                    console.log(err);
                });
        };

        //changes current order
        vm.changeOrder = function(order) {
            if (order === vm.orderBy) {
                vm.orderBy = "-" + order;
            } else {
                vm.orderBy = order;
            }
        }
    }

    adminReferencesController.$inject = ["ProfileService", "$uibModal"];

    //register component
    app.component("adminAboutReferences", {
        controller: adminReferencesController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/references/admin.about.references.component.html"
    });


})(window.angular);
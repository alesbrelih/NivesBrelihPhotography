(function (angular) {
    //reference to main app / module
    var app = angular.module("adminApp");

    //component controller
    function adminTermsConditionsController(TermsConditionsService, $uibModal) {

        //current scope
        var vm = this;

        vm.terms = {
            Content: ''
        };

        // init
        vm.$onInit = function () {
            TermsConditionsService.GetTermsAndConditions()
                .then(function (res) {
                    console.log("RES: ",res);
                    if (res) {
                        vm.terms = res;
                    }
                    
                });
        }
        

        vm.updateTerms = function () {
            TermsConditionsService.SetTermsAndConditions(vm.terms);
        }



    }

    //inject service
    adminTermsConditionsController.$inject = ["TermsConditionsService", "$uibModal"];

    //register component
    app.component("adminTermsConditions", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-terms-conditions/admin.terms-conditions.component.html",
        controller: adminTermsConditionsController,
        controllerAs: "vm"
    });
})(window.angular);
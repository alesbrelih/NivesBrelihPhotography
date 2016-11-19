// ---- MODAL WINDOW (ui.bootstrap) COMPONENT ----- //
(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //controller for modal
    function modalWindowController() {
        var vm = this;

        //control modal accept/reject
        vm.confirmModal = function() {
            vm.modalInstance.close("Accepted");
        };
        vm.declineModal = function() {
            vm.modalInstance.dismiss("Dismissed");
        }
    }

    //register modal view component
    app.component("abModalView", {
        controller: modalWindowController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/modal-view/modal.view.component.html",
        bindings: {
            modalInstance:"<"
        }
    });

})(window.angular);
// ---- MODAL WINDOW (ui.bootstrap) COMPONENT ----- //
(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //controller for modal
    function modalWindowController() {

        var vm = this;
        vm.type = vm.resolve.type;
        vm.entry = vm.resolve.entry;

        // function to set all texts
        function setTexts(type) {
            switch (type) {

                case "upload":
                    vm.titleText = "Save " + vm.resolve.entry;
                    vm.bodyText = "Do you wish to save this " + vm.resolve.entry + " ?";
                    vm.acceptText = "Save";
                    break;
                case "delete":
                    vm.titleText = "Delete " + vm.resolve.entry;
                    vm.bodyText = "Do you wish to delete this " + vm.resolve.entry + "?";
                    vm.acceptText = "Delete";
                    break;
                case "changeAlbumCover":
                    vm.titleText = "Change Album Cover";
                    vm.bodyText = "Saving this will change current album cover photo. Do you wish to continue?";
                    vm.acceptText = "Continue";
                    break;
                case "edit":
                    vm.titleText = "Edit "+vm.resolve.entry;
                    vm.bodyText = "Do you wish to edit this " + vm.resolve.entry + "?";
                    vm.acceptText = "Edit";
                    break;
                 
                default:
            }
        }

        //texts on modal if not set (resolve.type incorrect // failsave)
        vm.titleText = "Title";
        vm.bodyText = "Body";


        vm.checked = false;

        vm.acceptText = "Accepted";
        vm.rejectText = "Cancel";

        //sets texts depending on binds
        setTexts(vm.resolve.type);
        

        //control modal accept/reject
        vm.confirmModal = function () {
            if (vm.entry == "album" && vm.type == "delete") {
                //return if he want to delete photos aswell
                vm.modalInstance.close(vm.checked);

            } else {
                vm.modalInstance.close();
            }
            
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
            modalInstance:"<",
            resolve:"<"
        }
    });

})(window.angular);
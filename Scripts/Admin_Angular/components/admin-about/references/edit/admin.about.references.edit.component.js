// ------ REFERENCE EDIT COMPONENT MODULE ------ //
(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //component controller
    function editReferenceComponentController(ProfileService, $q) {
        var vm = this;

        function getReferenceData() {
            var promisePhotos = ProfileService.PhotosForReferencePage(vm.pageSize);
            $q.all([promisePhotos, vm.editReference]).then(function (success) {
                
                vm.CurrentReference = success[1].data;
                vm.Photos = success[0].photos;

                console.log(vm.CurrentReference);

            });

        }

        //current scope
        
        vm.pageSize = 10;

        //gets data for reference edit
        getReferenceData();

        //edits reference
        vm.EditReference = function() {
            ProfileService.EditReference(vm.CurrentReference);
        }


        

    }

    //inject services
    editReferenceComponentController.$inject = ["ProfileService","$q"];


    //register component
    app.component("adminAboutReferenceEdit", {
        controller: editReferenceComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/references/edit/admin.about.references.edit.component.html",
        bindings: {
            editReference: "<"
        }
    });


})(window.angular);
// --- ul photos checkable for reference about admin ---- //

(function(angular, $) {


    //reference to main app / module
    var app = angular.module("adminApp");

    function directiveFunction() {

        //link function
        function linkFnc(scope, el, attr, ngModel) {

            scope.CheckIfSelected = function (photo) {
                if (ngModel.$viewValue.indexOf(photo.PhotoId.toString()) != -1) {
                    photo.checked = true;
                    return true;
                }
                
            }

            //no ng model
            if (ngModel === null) {
                console.log("Please set ng-model.");
                return;
            }

            //delegate change event
            el.on("change", "input[type='checkbox']", function(e) {
                //get photo id
                var parent = $(this).closest(".photo-container");
                var id = $(parent).attr("data-id");

                //check state (true/false)
                var state = $(this).is(":checked");

                //if checked
                if (state) {
                    //and value isnt in ng-model already, insert id
                    if (ngModel.$modelValue.indexOf(id) === -1) {
                        ngModel.$modelValue.push(id);
                    }
                } else {

                    //checkbox unchecked
                    var idIndex = ngModel.$modelValue.indexOf(id);

                    //if id was checked before (exists in array)
                    if (idIndex !== -1) {
                        ngModel.$modelValue.splice(idIndex, 1);
                    }
                }
            });
        }


        return {
            restrict: "E",
            require: "ngModel",
            templateUrl: "/Scripts/Admin_Angular/templates/directives/elements/ab-photo-ul-checkable.directive.html",
            link: linkFnc,
            scope: {
                photos: "=",
                search: "="
            }
        }

    }

    //directive
    app.directive("abPhotoUlCheckable", directiveFunction);

})(window.angular, window.$);
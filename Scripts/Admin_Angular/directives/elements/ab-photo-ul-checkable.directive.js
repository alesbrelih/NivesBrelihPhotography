// --- ul photos checkable for reference about admin ---- //

(function (angular, $) {


    //reference to main app / module
    var app = angular.module("adminApp");

    function directiveFunction() {

        //link function
        function linkFnc(scope, el, attr, ngModel) {

            //no ng model
            if (ngModel === null) {
                console.log("Please set ng-model.");
                return;
            }

            //checks if selected on init
            scope.CheckIfSelected = function (photo) {
                if (ngModel.$modelValue.indexOf(photo.PhotoId.toString()) != -1) {
                    photo.checked = true;
                    return true;
                } else {
                    photo.checked = false;
                }

            }

            
            //sets view model on change
            scope.SetModelValue = function (photo) {
                var photoId = photo.PhotoId.toString();
                if (photo.checked) {
                    if (ngModel.$modelValue.indexOf(photoId) === -1) {
                        ngModel.$modelValue.push(photoId);
                    }
                } else {
                    //checkbox unchecked
                    var idIndex = ngModel.$modelValue.indexOf(photoId);

                    //if id was checked before (exists in array)
                    if (idIndex !== -1) {
                        ngModel.$modelValue.splice(idIndex, 1);
                    } 
                }
            }

          
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
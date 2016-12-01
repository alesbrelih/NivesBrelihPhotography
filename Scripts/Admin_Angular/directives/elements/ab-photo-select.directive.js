// ---- AB PHOTO SELECT DIRECTIVE MODULE ----- //

(function(angular) {
    
    //main app / module
    var app = angular.module("adminApp");

    //function controller
    function photoSelectDirectiveController() {

        //link fnc
        function linkFunc(scope, el, attr, ngCtrl) {

            scope.showPhotos = false;
            scope.selected = null;
            scope.searchText = "";

            //selects photo and sets ngModel
            scope.selectPhoto = function(photo) {
                scope.selected = photo;
                ngCtrl.$setViewValue(photo.PhotoId);
                scope.showPhotos = false;
            };


            scope.checkIfCover = function (photo) {

                if (photo.PhotoId == ngCtrl.$viewValue) {
                    
                    scope.selected = photo;


                }
            };
            //scope.setCover = function () {
            //    console.log("started");
            //    scope.photos.forEach(function(item) {
            //        if (item.PhotoId == ngCtrl.$viewValue) {
            //            scope.selected = item;
            //        }
            //    });
            //}

        }

        return {
            restrict: "E", //el
            require: "ngModel", //requires ng-model
            link: linkFunc, //link function
            templateUrl: "/Scripts/Admin_Angular/templates/directives/elements/ab-photo-select.directive.html", //directive template
            scope: {
                photos: "="
                //selectedCove:"="// photos to be selected from
                //selected:"="
            }
        }
    }
    
    //register directive
    app.directive("abPhotoSelect", photoSelectDirectiveController);


})(window.angular);
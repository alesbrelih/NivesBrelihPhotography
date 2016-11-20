// ----- MAIN ANGULAR MODULE / APP --------//
(function angularModule(angular) {

    //register angular module
    angular.module("adminApp", ["ngAnimate", "adminApp.services", "ui.router", "ui.bootstrap", "toastr"]);


})(window.angular);
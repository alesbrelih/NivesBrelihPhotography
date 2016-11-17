// angular admin view component

(function(angular) {
    
    //angular admin module
	var app = angular.module("adminApp");

    //component controller

    function adminViewController() {
        var vm = this;

    }

    app.component("adminView", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-view/admin.view.component.html",
        controller: adminViewController,
        controllerAs: "vm"
});

})(window.angular);
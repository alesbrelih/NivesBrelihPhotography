// angular admin view component

(function(angular) {
    
    //angular admin module
	var app = angular.module("adminApp");

    //component controller

    function adminViewController() {
        var vm = this;

        vm.adminSections = [
            {
                state: "photos",
                title: "Photos",
                body: "Add, modify and delete existing photos."
            },
            {
                state: "albums",
                title: "Albums",
                body: "Add, modify and delete existing albums."
            },
            {
                state: "blogs",
                title: "Blogs",
                body: "Add, modify and delete existing blogs."
            },
            {
                state: "categories",
                title: "Categories",
                body: "Add, modify and delete existing photos"
            },
            {
                state: "about-main",
                title: "About",
                body: "Edit your personal information and social links. Modify your references and reviews."
            }
        ];

    }

    app.component("adminView", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-view/admin.view.component.html",
        controller: adminViewController,
        controllerAs: "vm"
});

})(window.angular);
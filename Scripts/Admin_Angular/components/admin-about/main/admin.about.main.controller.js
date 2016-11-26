/////////////////////////////////////////////////////////
//  ----------- ABOUT MAIN CONTROLLER MODULE --------- //
/////////////////////////////////////////////////////////

(function(angular) {

    //reference to main app/module
    var app = angular.module("adminApp");

    //component controller
    function aboutMainController() {

        var vm = this;

        //avaliable sections for admin about
        vm.sections = [
            {
                state: "about-personal",
                title:"Personal Information",
                body:"Personal information section, where you can change your personal information display"
            },
            {
                state:"about-social-links",
                title: "Social Links",
                body: "Social links section, where you can set social link URLs and select which one to display on page"
            },
            {
                state:"about-references",
                title: "References",
                body: "Reference section, where you can add,modify or delete references"
            },
            {
                state:"about-reviews",
                title: "Reviews",
                body: "Reviews section, where you can add,modify or delete reviews"
            }
        ];

    }

    //register component
    app.component("adminAboutMain", {
        controller: aboutMainController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/main/admin.about.main.controller.html"
    });

})(window.angular);
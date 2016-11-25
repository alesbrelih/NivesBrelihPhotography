// ---- SOCIAL LINKS COMPONENT MODULE ---- //

(function(angular) {
    
    // reference to main app/module
    var app = angular.module("adminApp");

    // social links controller
    function socialLinksController() {
        var vm = this;
    }


    //register component on app
    app.component("adminAboutSocialLinks", {
        controller: socialLinksController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/social-links/admin.about.social.links.component.html"
    });

})(window.angular);
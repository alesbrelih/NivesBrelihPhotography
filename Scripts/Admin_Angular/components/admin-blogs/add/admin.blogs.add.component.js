// ---- BLOGS ADD COMPONENT MODULE --- //
(function(angular) {

    //main app / module
    var app = angular.module("adminApp");

    //add component controller
    function addBlogComponentController(BlogsService,CmsService) {
        
    }

    //inject needed services
    addBlogComponentController.$inject = ["BlogsService", "CmsService"];
    
    //register component
    app.component("adminBlogsAdd", {
        controller: addBlogComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-blogs/add/admin.blogs.add.component.html"
    });


})(window.angular);
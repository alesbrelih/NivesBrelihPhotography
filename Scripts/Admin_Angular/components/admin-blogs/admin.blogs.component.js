// --- ADMIN BLOGS COMPONENT MODULE --- //
(function(angular) {

    //register main app / module
    var app = angular.module("adminApp");

    // blogs component controller
    function blogsComponentController(BlogsService,$uibModal) {

        //current scope
        var vm = this;

        //paging props
        vm.pageSize = 5;
        vm.currentPage = 1;


        //--- scope init -- //

        //refresh blogs and then set them as scope property
        BlogsService.RefreshBlogs().then(function () {

            vm.Blogs = BlogsService.GetBlogs();
            vm.allPages = Math.ceil(vm.Blogs.length / vm.pageSize);

        });

        // --- METHODS --- //
        vm.DeleteBlog = function(blog) {
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "blog";
                    }
                }
            });

            //if confirmation is accepted
            modal.result.then(function () {
                console.log("DELETED NOOO!O!O!OO!O!O");
                //delete blog using blog service
                BlogsService.DeleteBlog(blog);
            });
        }


    }
    //inject needed services
    blogsComponentController.$inject = ["BlogsService","$uibModal"];

    // register component
    app.component("adminBlogs", {
        controller: blogsComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-blogs/admin.blogs.component.html"
    });

})(window.angular);
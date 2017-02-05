// --- EDIT BLOG COMPONENT MODULE --- //
(function(angular) {
    
    //main app / module reference
    var app = angular.module("adminApp");

    // edit blog controller
    function editBlogController(BlogsService,PhotosService,CategoriesService,AlbumsService,CmsService,$q) {
        
        //current scope
        var vm = this;

        //gets all data
        function getData() {
            var p1 = PhotosService.GetPhotos();
            var p2 = CategoriesService.GetCategories(true);
            var p3 = AlbumsService.GetAlbums(true);
            var p4 = vm.blogdb;

            $q.all([p1, p2, p3, p4]).then(function(success) {
                vm.photos = PhotosService.Photos;
                vm.categories = CategoriesService.Categories;
                vm.albums = AlbumsService.Albums;
                vm.Blog = success[3].data;
                CmsService.EditContent(vm.Blog.Content);
            });
        }

        //get all data needed
        getData();

        // --- ACTIONS --- //

        //edit blog action
        vm.EditBlog = function () {
            if (vm.Blog) {
                BlogsService.EditBlog(vm.Blog);
            }
            
        }


    }
    //inject needed services
    editBlogController.$inject = ["BlogsService", "PhotosService", "CategoriesService", "AlbumsService", "CmsService", "$q"];

    //register component to app
    app.component("adminBlogsEdit", {
        controller: editBlogController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-blogs/edit/admin.blogs.edit.component.html",
        bindings: {
            blogdb:"="
        }
    });

})(window.angular);
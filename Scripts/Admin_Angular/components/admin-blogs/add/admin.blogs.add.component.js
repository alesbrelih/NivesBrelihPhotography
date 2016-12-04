// ---- BLOGS ADD COMPONENT MODULE --- //
(function(angular) {

    //main app / module
    var app = angular.module("adminApp");

    //add component controller
    function addBlogComponentController(BlogsService, PhotosService, AlbumsService, CategoriesService, CmsService) {

        //current scope
        var vm = this;

        //blog prop
        vm.Blog = {};

        //set catgories
        vm.categories = CategoriesService.Categories;
        CategoriesService.GetCategories();

        //set albums
        vm.albums = AlbumsService.Albums;
        AlbumsService.GetAlbums();

        //set photos
        vm.photos = PhotosService.Photos;
        PhotosService.GetPhotos();


        vm.test = function() {
            console.log(vm.categories, vm.albums, vm.photos);
        }

        // --- ACTIONS --- //

        //create blog
        vm.CreateBlog = function() {
            var content = CmsService.GetContent();
            vm.Blog.content = content;
            console.log(vm.Blog);

        }

    }

    //inject needed services
    addBlogComponentController.$inject = ["BlogsService","PhotosService","AlbumsService","CategoriesService", "CmsService"];
    
    //register component
    app.component("adminBlogsAdd", {
        controller: addBlogComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-blogs/add/admin.blogs.add.component.html"
    });


})(window.angular);
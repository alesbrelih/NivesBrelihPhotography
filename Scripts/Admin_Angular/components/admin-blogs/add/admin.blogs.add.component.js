// ---- BLOGS ADD COMPONENT MODULE --- //
(function(angular) {

    //main app / module
    var app = angular.module("adminApp");

    //add component controller
    function addBlogComponentController(BlogsService, PhotosService, AlbumsService, CategoriesService, CmsService,$uibModal) {

        //current scope
        var vm = this;

        //blog prop
        vm.Blog = null;

        //set catgories
        vm.categories = CategoriesService.Categories;
        CategoriesService.GetCategories();
        vm.Category = {
            CategoryName: ""
        };

        //set albums
        vm.albums = AlbumsService.Albums;
        AlbumsService.GetAlbums();

        //set photos
        vm.photos = PhotosService.Photos;
        PhotosService.GetPhotos();

        // --- ACTIONS --- //

        //creates blog function
        vm.CreateBlog = function() {
            BlogsService.CreateBlog(vm.Blog);
        }

        //create category
        vm.CreateCategory = function () {

            //hide new category input if category created.
            CategoriesService.CreateCategory(vm.Category, true).then(function (success) {
                vm.newCategoryForm = false;
            }, function (err) {
                console.log(err);
            });
        }

    }

    //inject needed services
    addBlogComponentController.$inject = ["BlogsService","PhotosService","AlbumsService","CategoriesService", "CmsService","$uibModal"];
    
    //register component
    app.component("adminBlogsAdd", {
        controller: addBlogComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-blogs/add/admin.blogs.add.component.html"
    });


})(window.angular);
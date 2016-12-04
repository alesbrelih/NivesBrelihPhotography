// ---- BLOGS SERVICE MODULE --- //
(function (angular) {

    // reference to services module
    var services = angular.module("adminApp.services");

    //blogs factory
    function blogsFactoryController($http, toastr) {

        //register returned factory
        var blogsFactory = {};

        //privates
        var blogs = [];

        // --- factory methods --- //

        //get blogs function
        blogsFactory.GetBlogs = function () {

            return blogs;
        }

        //refresh blogs promise
        blogsFactory.RefreshBlogs = function () {
            return $http.get("/api/blogs")
                .then(function (success) {

                    blogs = success.data;
                },
                function (err) {
                    console.log(err);
                });
        }


        //delete blog function
        blogsFactory.DeleteBlog = function (blog) {
            //remove blog from db using api call
            $http.delete("/api/blogs", {
                params: {
                    "id": blog.Id
                }
            })
                .then(function () {
                    //remove blog from current blogs
                    var blogIndex = blogs.indexOf(blog);
                    blogs.splice(blogIndex, 1);
                }, function (err) {
                    //notify user about err
                    toastr.error(err.data, "Error");
                    console.log(err);
                });
        }


        return blogsFactory;
    }

    //inject needed services
    blogsFactoryController.$inject = ["$http", "toastr"];


    //register blogs factory/service
    services.factory("BlogsService", blogsFactoryController);


})(window.angular);
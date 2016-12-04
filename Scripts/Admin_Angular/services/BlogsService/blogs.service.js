﻿// ---- BLOGS SERVICE MODULE --- //
(function (angular) {

    // reference to services module
    var services = angular.module("adminApp.services");

    //blogs factory
    function blogsFactoryController($http, toastr,$state) {

        //register returned factory
        var blogsFactory = {};

        //privates
        var blogs = [];

        // --- factory methods --- //

        //get blogs function
        blogsFactory.GetBlogs = function () {

            return blogs;
        }

        //get blog function
        blogsFactory.GetBlog = function(id) {
            return $http("/api/blogs/",{params: {
                "id":id
            }});

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

        //creates blog funciton
        blogsFactory.CreateBlog = function (blog) {
            //creates blog using api
            $http.post("/api/blogs", blog)
                .then(function (success) {
                    //success

                    //add blog to blogs list
                    blogs.push(success.data);

                    //notify about success
                    toastr.success("Blog successfully created.", "Success");

                    //change state if needed
                    if ($state.current.name == "blogs-add") {
                        $state.go("blogs");
                    }
                }, function (err) {
                    //catch err
                    console.log(err);
                    toastr.error(err.data, "Error");
                });
        }

        //edits blog
        blogsFactory.EditBlog = function(blog) {
            //call api
            $http.put("/api/blogs", blog)
                .then(function() {
                    //success

                    //notify user
                    toastr.success("Blog successfully edited.", "Success");

                    //change state if needed
                    if ($state.current.name == "blogs-edit") {
                        $state.go("blogs");
                    }
                }, function(err) {
                    //catch err
                    console.log(err);
                    toastr.error(err.data, "Error");
                });
        }

        return blogsFactory;
    }

    //inject needed services
    blogsFactoryController.$inject = ["$http", "toastr","$state"];


    //register blogs factory/service
    services.factory("BlogsService", blogsFactoryController);


})(window.angular);
// ---- BLOGS SERVICE MODULE --- //
(function (angular) {

    // reference to services module
    var services = angular.module("adminApp.services");

    //blogs factory
    function blogsFactoryController($http, toastr,$state,$uibModal,CmsService) {

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
            return $http.get("/api/blogs/", {
                params: {
                    "id": id
                }
            });

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

        //add confirmation
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

                //remove blog from db using api call
                $http.delete("/api/blogs", {
                    params: {
                        "id": blog.Id
                    }
                })
                    .then(function () {
                        //notify user
                    toastr.success("Blog successfully deleted.", "Success");

                        //remove blog from current blogs
                        var blogIndex = blogs.indexOf(blog);
                        blogs.splice(blogIndex, 1);
                    }, function (err) {
                        //notify user about err
                        toastr.error(err.data, "Error");
                        console.log(err);
                    });
                
            });

        }

        //creates blog funciton
        blogsFactory.CreateBlog = function (blog) {

            //set content
            blog.content = CmsService.GetContent();

            //modal to confirm
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "upload";
                    },
                    entry: function () {
                        return "blog";
                    }
                }
            });

            //create if user accepted the modal
            modal.result.then(function () {

                //user accepted

                //creates blog using api
                $http.post("/api/blogs", blog)
                    .then(function(success) {
                        //success

                        //add blog to blogs list
                        blogs.push(success.data);

                        //notify about success
                        toastr.success("Blog successfully created.", "Success");

                        //change state if needed
                        if ($state.current.name == "blogs-add") {
                            $state.go("blogs");
                        }
                    }, function(err) {
                        //catch err
                        console.log(err);
                        toastr.error(err.data, "Error");
                    });
            });


        }

        //edits blog
        blogsFactory.EditBlog = function (blog) {
            //set content
            blog.content = CmsService.GetContent();

            //modal to confirm
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "edit";
                    },
                    entry: function () {
                        return "blog";
                    }
                }
            });

            //user confirmed
            modal.result.then(function() {
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
            });


        }

        return blogsFactory;
    }

    //inject needed services
    blogsFactoryController.$inject = ["$http", "toastr","$state","$uibModal","CmsService"];


    //register blogs factory/service
    services.factory("BlogsService", blogsFactoryController);


})(window.angular);
// ---- CATEGORIES SERVICE MODULE REGISTER ---- //
(function(angular) {

    //get services module to register factory on
    var services = angular.module("adminApp.services");

    //factory controller
    function categoriesFactoryController($http,toastr) {

        //returned singleton
        var categoriesFactory = {};

        // --- privates --- //
        var categories = [];

        // --- properties --- //
        categoriesFactory.Categories = categories;
        
        // --- methods --- //
        categoriesFactory.GetCategories = function (returnPromise) {

            //clear categories before adding new
            while (categories.length > 0) {
                categories.pop();
            }

            var promise = $http.get("/api/categories")
                .then(function (success) {
                    success.data.forEach(function (category) {
                        categories.push(category);
                    });
                },
                    function (err) {
                        console.log(err);
                    });

            if (returnPromise) {
                return promise;
            }
            

        };

        //creates new category
        categoriesFactory.CreateCategory = function(category,promise,cb) {
           var requestPromise =  $http.post("/api/categories", category)
                .then(function (success) {
                    //add category to categories list
                    categories.push(success.data);

                    //show toastr that category was successfully added
                    toastr.success("Category successfuly created.", "Success");

                    //if cb exist call cb after
                    if (cb) {
                        cb();
                    }
                    
                }, function (err) {
                    //err 
                    toastr.error(err.data, "Error");
                });

            //if promise is true return promise
            if (promise) {
                return requestPromise;
            }
        }

        //deletes category
        categoriesFactory.DeleteCategory = function (category) {

            //because of default api routing next param is id
            $http.delete("/api/categories/" + category.CategoryId)
                .then(function(success) {
                    //category deleted

                    toastr.success("Category deleted successfully.", "Success");

                    //remove category from categories list
                    var catIndex = categories.indexOf(category);
                    categories.splice(catIndex, 1);


            }, function(err) {
                    //err occured
                console.log(err);
                toastr.error(err.data, "Error");
            });
        }
        
        //edit category
        categoriesFactory.EditCategory = function(category,promise) {
            var promiseReq = $http.put("/api/categories", category)
                .then(function(success) {

                    //category edited

                    var changedCat = categories.find(function(cat) {
                        return cat.CategoryId == category.CategoryId;
                    });
                    changedCat.CategoryName = category.CategoryName;

                    toastr.success("Category edited successfully", "Success");


            }, function(err) {

                console.log(err);
                toastr.error(err.data, "Error");

            });

            //return promise if needed
            if (promise) {
                return promiseReq;
            }
        }


        //return singleton
        return categoriesFactory;
    }

    //inject dependencies
    categoriesFactoryController.$inject = ["$http","toastr"];

    //register factory
    services.factory("CategoriesService", categoriesFactoryController);

})(window.angular);
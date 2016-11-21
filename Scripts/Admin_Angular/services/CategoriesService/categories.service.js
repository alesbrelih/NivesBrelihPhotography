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
        categoriesFactory.GetCategories = function() {

            $http.get("/api/categories")
                .then(function(success) {
                    success.data.forEach(function(category) {
                        categories.push(category);
                    });
                },
                    function(err) {
                        console.log(err);
                    });

        };

        //creates new category
        categoriesFactory.CreateCategory = function(category,promise) {
           var requestPromise =  $http.post("/api/categories", category)
                .then(function (success) {
                    //add category to categories list
                    categories.push(success.data);

                    //show toastr that category was successfully added
                    toastr.success("Category successfuly created.", "Success");

                    
                    
                }, function (err) {
                    //err 
                    toastr.error(err.data, "Error");
                });

            //if promise is true return promise
            if (promise) {
                return requestPromise;
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
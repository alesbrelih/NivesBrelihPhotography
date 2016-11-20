// ---- CATEGORIES SERVICE MODULE REGISTER ---- //
(function(angular) {

    //get services module to register factory on
    var services = angular.module("adminApp.services");

    //factory controller
    function categoriesFactoryController($http) {

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
        


        //return singleton
        return categoriesFactory;
    }

    //inject dependencies
    categoriesFactoryController.$inject = ["$http"];

    //register factory
    services.factory("CategoriesService", categoriesFactoryController);

})(window.angular);
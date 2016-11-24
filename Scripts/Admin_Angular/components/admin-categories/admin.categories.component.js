// ----- admin categories component module ------- //
(function(angular) {

    // reference to main app/module
    var app = angular.module("adminApp");

    // controller function
    function categoriesComponentController(CategoriesService) {

        //reference to scope
        var vm = this;

        //gets all categories
        CategoriesService.GetCategories()
            .then(function(success) {
            vm.Categories = CategoriesService.Categories;
        }).catch(function(err) {
                console.log(err);
            });

        // ----- PUBLIC METHODS ------ //
        vm.EditCategory = function(category) {
            
        }
        vm.DeleteCategory = function(category) {
            
        }
        vm.CreateCategory = function() {
            CategoriesService.CreateCategory(vm.newCategory);
        }

    }

    categoriesComponentController.$inject = ["CategoriesService"];

    app.component("adminCategories", {
        controller: categoriesComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-categories/admin.categories.component.html"
    });

})(window.angular);
// ----- admin categories component module ------- //
(function(angular) {

    // reference to main app/module
    var app = angular.module("adminApp");

    // controller function
    function categoriesComponentController(CategoriesService,$uibModal) {

        //reference to scope
        var vm = this;
        vm.pageSize = 5;
        vm.currentPage = 0;
        vm.selected = null;

        //gets all categories
        CategoriesService.GetCategories(true)
            .then(function(success) {
                vm.Categories = CategoriesService.Categories;
                vm.allPages = Math.ceil(vm.Categories.length / vm.pageSize)-1;

        }).catch(function(err) {
                console.log(err);
        });

        // ----- PUBLIC METHODS ------- //

        //creates new object for new stuff to be inserted to
        vm.selectForAdd = function() {
            vm.selected = {
                CategoryId: null,
                CategoryName: ""
            }
        }

        //creates "copy" of selected item in vm.selected
        vm.selectForEdit = function(category) {
            vm.selected = category;
        }

        //resets selected
        vm.resetSelected = function() {
            vm.selected = null;
        }


        // ----- SERVICE METHODS ------ //

        //edit category

        vm.EditCategory = function () {

            //creates modal first
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "edit";
                    },
                    entry: function () {
                        return "category";
                    }
                }
            });

            //depending on modal result -> actions
            modal.result.then(function () {

                CategoriesService.EditCategory(vm.selected,true).then(function(success) {

                    vm.resetSelected();

                }).catch(function (err) {
                    //catch err
                    console.log(err);
                });

            }).catch(function (err) {

                console.log(err);

            });

            
        }

        //delete category
        vm.DeleteCategory = function (category) {

            //creates modal first
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "category";
                    }
                }
            });

            modal.result.then(function() {

                CategoriesService.DeleteCategory(category);

            }).catch(function (err) {

                //catch err
                console.log(err);

            });
   
        }

        //create category
        vm.CreateCategory = function() {
            CategoriesService.CreateCategory(vm.selected, false,function() {
                vm.resetSelected();
            });

        }


        vm.nextPage = function() {
            vm.currentPage++;
        }
        vm.previousPage = function() {
            vm.currentPage--;
        }
    }

    //inject needed services
    categoriesComponentController.$inject = ["CategoriesService","$uibModal"];

    app.component("adminCategories", {
        controller: categoriesComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-categories/admin.categories.component.html"
    });

})(window.angular);
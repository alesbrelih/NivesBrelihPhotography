// ------ ADMIN PHOTOS EDIT COMPONENT ----- //
(function (angular,window) {

    //reference to main module/app
    var app = angular.module("adminApp");

    function adminEditComponentController(PhotosService, $rootScope, CategoriesService, AlbumsService, $q) {

        //function that sets up edit enviroment using promises
        function setCategoriesAndAlbums() {

            //deffer object
            var deffered = $q.defer();


            // get all albums
            var p1 = AlbumsService.GetAlbums();

            //get all categories
            var p2 = CategoriesService.GetCategories();

            //get profile info
            var p3 = vm.photoPromise;


            //wait for all promises to be resolved
            window.Promise.all([p1, p2, p3]).then(function () {

                // ---- properties ---- //
                var albums = AlbumsService.Albums;
                var categories = CategoriesService.Categories;

                var returnObject = {
                    albums: albums,
                    categories: categories
                }

                //resolved successfull
                deffered.resolve(returnObject);

            }).catch(function (reason) {
                //reject promise
                deffered.reject(reason);
            });

            return deffered.promise;
        }

        //html scope
        var vm = this;

        //props for category
        vm.Category = {
            CategoryName: ""
        };
        vm.newCategoryForm = false;

        //create category btn click
        vm.CreateCategory = function () {

            //hide new category input if category created.
            CategoriesService.CreateCategory(vm.Category, true).then(function () {
                vm.newCategoryForm = false;
            }, function (err) {
                console.log(err);
            });
        }

        
        //set enviroment
        setCategoriesAndAlbums()
            .then(function(success) {
                
                //set albums/categories
                vm.Albums = success.albums;
                vm.Categories = success.categories;

                // current photo
                vm.Photo = PhotosService.GetCurrentPhoto();

            console.log("promise done");
        }).catch(function(err) {
            
        });

        //edit photo accept btn click
        vm.EditPhoto = function() {
            PhotosService.EditPhoto(vm.Photo);
        }

    }

    //inject for uglification
    adminEditComponentController.$inject = ["PhotosService", "$rootScope", "CategoriesService", "AlbumsService", "$q"];

    //register component
    app.component("adminPhotosEdit", {
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/edit/admin.photos.edit.component.html",
        controller: adminEditComponentController,
        controllerAs: "vm",
        bindings: {
            photoPromise:"<"
        }

    });

})(window.angular,window);
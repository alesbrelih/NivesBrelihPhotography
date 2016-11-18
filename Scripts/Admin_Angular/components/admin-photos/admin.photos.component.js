// admin photos component 

(function(angular) {

    //get main angular module
    var app = angular.module("adminApp");

    //admin photos controller
    function adminPhotosController(PhotosService,$scope,$uibModal) {

        //current instance of controller
        var vm = this;

        // ------ PROPERTIES ------ //
        vm.currentPage = 1;
        vm.allPages = 0;
        vm.pageSize = 20;

        // increases number of pages if more pictures loaded on ajax call
        function increaseNumberOfPages() {
            vm.allPages += 1;
            
            //first page, page just loaded
            if (vm.currentPage == 1) {
                //slice array from start, next slicing will take care of $watch function on vm.currentPage
                vm.Photos = _photos.slice(0);
            }
        }


       
        // loads photos from DB in ajax way
        PhotosService.GetPhotos(vm.pageSize,increaseNumberOfPages);

        //get all photos
        var _photos = PhotosService.Photos;

        //if current page changes, slice the array properly
        $scope.$watch("vm.currentPage", function () {

            vm.Photos = _photos.slice((vm.currentPage-1)*vm.pageSize);

        });

        //search filter
        $scope.$watch("vm.searchText", function() {
            vm.currentPage = 1;
            vm.Photos = _photos.filter(function(item) {
                for (var key in item) {
                    if (typeof item[key] == "string") {
                        if (item[key].toLowerCase().includes(vm.searchText.toLowerCase())) {
                            return item;
                        }
                    }
                }
            });
            vm.allPages = Math.ceil(vm.Photos.length / vm.pageSize);
        });

        // ----- METHODS -------//

        // -- navigation -- //

        // previous page
        vm.previousPage = function() {
            vm.currentPage -= 1;
        }

        // next page
        vm.nextPage = function() {
            vm.currentPage += 1;
            console.log(_photos);
            console.log(vm.currentPage * vm.pageSize);
            console.log(vm.Photos);
        }

        // -- photo actions -- //

        vm.deletePhoto = function(photo) {
            PhotosService.deletePhoto(photo);
        };


    }

    //injecting dependencies for possible minification of javascipt files
    adminPhotosController.$inject = ["PhotosService","$scope","$uibModal"];

    //register component on angular module
    app.component("adminPhotos", {
        controller: adminPhotosController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/admin.photos.component.html"
    });

})(window.angular);
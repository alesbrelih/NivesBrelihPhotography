// admin photos component 

(function(angular) {

    //get main angular module
    var app = angular.module("adminApp");

    //admin photos controller
    function adminPhotosController(PhotosService,$scope,$uibModal,toastr) {

        //current instance of controller
        var vm = this;

        // ------ PROPERTIES ------ //
        vm.currentPage = 1;
        vm.allPages = 0;
        vm.pageSize = 20;
        vm.searchText = "";

        // -------- PRIVATE FUNCTIONS -------- //

        // increases number of pages if more pictures loaded on ajax call
        function increaseNumberOfPages() {
            vm.allPages += 1;
            
            //first page, page just loaded
            if (vm.currentPage == 1) {
                //slice array from start, next slicing will take care of $watch function on vm.currentPage
                vm.Photos = _photos.slice(0);
            }
        }

        //filters photos to search criteria
        //page is page that will be set after deletition
        function filterPhotosToSearchCriteria(page) {


            vm.Photos = _photos.filter(function (item) {
                if (item !== null) {
                    for (var key in item) {

                        if (key == "Album" || key == "PhotoTitle" || key == "UploadedString") {
                            if (item[key] !== null) {

                                if (item[key].toLowerCase().includes(vm.searchText.toLowerCase())) {
                                    return item;
                                }
                            }

                        }


                    }
                }

            });
            vm.currentPage = page;
        }


       
        // loads photos from DB in ajax way
        PhotosService.GetPhotos(vm.pageSize,increaseNumberOfPages);

        //get all photos
        var _photos = PhotosService.Photos;

        // ---- WATCHING FOR CHANGES ---- ////

        //if current page changes, slice the array properly
        $scope.$watch("vm.currentPage", function () {

            vm.Photos = _photos.slice((vm.currentPage-1)*vm.pageSize);

        });

        //search filter
        $scope.$watch("vm.searchText", function() {
            filterPhotosToSearchCriteria(1);
            vm.allPages = Math.ceil(vm.Photos.length / vm.pageSize);
        });

        // ----- SCOPE METHODS -------//

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
        vm.deletePhoto = function (photo) {
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type:function() {
                        return "delete";
                    },
                    entry:function() {
                        return "photo";
                    }
                }
            });


            // modal accepted to delete the photo
            modal.result.then(function() {

                PhotosService.RemovePhoto(photo).then(function (success) {
                    var photoIndex = _photos.indexOf(photo);

                    _photos.splice(photoIndex, 1);

                    

                    //photo was removed 
                    filterPhotosToSearchCriteria(vm.currentPage);

                    toastr.success("Photo successfully deleted", "Success");
                },function(err) {
                    toastr.error("Error deleting photo.", "Error");
                });
            }, function() {
                console.log("modal rejected");
            });
            //
        };


       
    }

    //injecting dependencies for possible minification of javascipt files
    adminPhotosController.$inject = ["PhotosService","$scope","$uibModal","toastr"];

    //register component on angular module
    app.component("adminPhotos", {
        controller: adminPhotosController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-photos/admin.photos.component.html"
    });

})(window.angular);
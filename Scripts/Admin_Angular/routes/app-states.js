(function(angular) {
    
    //search for angular module
    var app = angular.module("adminApp");

    app.config([
        "$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
            $stateProvider
                //--- ADMIN DEFAULT VIEW --- //
                .state("view", {
                    url: "/",
                    template: "<admin-view></admin-view>"
                })

                // --- ADMIN PHOTOS SECTION --- //
                .state("photos", {
                    //list
                    url: "/photos",
                    template: "<admin-photos></admin-photos>"
                })
                .state("photos-add", {
                    //add
                    url: "/photos/add",
                    template: "<admin-photos-add></admin-photos-add>"
                })
                .state("photos-edit", {
                    //edit
                    url: "/photos/edit/:id",
                    template: "<admin-photos-edit photo-promise='$resolve.photo'></admin-photos-edit>",
                    resolve: {
                        //resolves photo from id in url param
                        photo: [
                            "$stateParams", "PhotosService", function($stateParams, PhotosService) {
                                return PhotosService.GetUserForEdit($stateParams.id);
                            }
                        ]
                    }
                })

                // --- ADMIN CATEGORIES SECTION --- //
                .state("categories", {
                    //list 
                    url: "/categories",
                    template: "<admin-categories></admin-categories>"
                });
                

            $urlRouterProvider.otherwise("/");
        }
    ]);

})(window.angular);
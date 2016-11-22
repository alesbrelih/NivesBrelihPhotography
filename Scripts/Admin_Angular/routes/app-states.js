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
                    url: "/photos",
                    template: "<admin-photos></admin-photos>"
                })
                .state("photos-add", {
                    url: "/photos/add",
                    template: "<admin-photos-add></admin-photos-add>"
                })
                .state("photos-edit", {
                    url: "/photos/edit",
                    template: "<admin-photos-edit></admin-photos-edit>",
                    resolve: {
                        photo: ["$stateParams","PhotosService","$state",function($stateParams,PhotosService,$state) {
                            PhotosService.GetUserForEdit(55);
                        }]
                    }
                });
                

            $urlRouterProvider.otherwise("/");
        }
    ]);

})(window.angular);
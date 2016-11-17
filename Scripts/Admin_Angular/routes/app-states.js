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
                .state("photos.add", {
                    url: "/add",
                    template: "to be inserted"
                })
                .state("photos.edit", {
                    url: "/edit",
                    template: "to be inserted"
                })
                .state("photos.remove", {
                    url: "/remove",
                    template: "TBI"
                });

            $urlRouterProvider.otherwise("/");
        }
    ]);

})(window.angular);
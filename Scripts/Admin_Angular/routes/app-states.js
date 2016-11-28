(function (angular) {

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
                })

                // --- ADMIN ABOUT ALL SECTION --- //
                .state("about-main", {
                    url: "/about",
                    template: "<admin-about-main></admin-about-main>"
                })

                // --- ADMIN ABOUT PERSONAL SECTION ---- /
                .state("about-personal", {
                    url: "/about/personal",
                    template: "<admin-about-personal></admin-about-personal>"
                })

                // --- ADMIN SOCIAL LINKS SECTION ---- //
                .state("about-social-links", {
                    url: "/about/social-links",
                    template: "<admin-about-social-links></admin-about-social-links>"
                })
                // --- ADMIN REVIEWS SECTION ---- //
                .state("about-reviews", {
                    url: "/about/reviews",
                    template: "<admin-about-reviews></admin-about-reviews>"
                })
                // -- ADMIN REFERENCES SECTION --- //
                .state("about-references", {
                    url: "/about/references",
                    template: "<admin-about-references></admin-about-references>"
                })
                .state("about-references-add", {
                    url: "/about/references/add",
                    template: "<admin-about-references-add></admin-about-references-add>"
                })
                //admin references edit from id in url
                .state("about-references-edit", {
                    url: "/about/references/edit/:id",
                    template: "<admin-about-reference-edit edit-reference='$resolve.editReference'></admin-about-reference-edit>",
                    resolve: {
                        editReference: [
                            "$stateParams", "ProfileService", function($stateParams, ProfileService) {
                                console.log($stateParams.id);
                                return ProfileService.GetSingleReference($stateParams.id);
                            }
                        ]
                    }
                });


            $urlRouterProvider.otherwise("/");
        }
    ]);

})(window.angular);
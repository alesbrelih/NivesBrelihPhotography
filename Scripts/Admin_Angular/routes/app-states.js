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
                //reference add
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
                                return ProfileService.GetSingleReference($stateParams.id);
                            }
                        ]
                    }
                })

                // ---- ADMIN ALBUMS SECTION ---- //
                .state("albums", {
                    url: "/albums",
                    template: "<admin-albums></admin-albums>"
                })
                // -- ADMIN ALBUM ADD SECTION --- //
                .state("albums-add", {
                    url: "/albums/add",
                    template: "<admin-albums-add></admin-albums-add>"
                })
                //-- ADMIN ALBUM EDIT SECTION --- //
                .state("albums-edit", {
                    url: "/albums/edit/:id",
                    template: "<admin-albums-edit album='$resolve.album'></admin-albums-edit>",
                    resolve: {
                        album: [
                            "AlbumsService", "$stateParams", function(AlbumsService, $stateParams) {
                                //return promise with selected album from params id
                                return AlbumsService.GetAlbum($stateParams.id);
                            }
                        ]
                    }
                })

                // --- ADMIN BLOGS SECTION ---- //
                .state("blogs", {
                    url: "/blogs",
                    template: "<admin-blogs></admin-blogs>"
                })
                // --- ADMIN BLOGS ADD SECTION --- //
                .state("blogs-add", {
                    url: "/blogs/add",
                    template: "<admin-blogs-add></admin-blogs-add>"
                })
                // ---- ADMIN BLOGS EDIT SECTION --- //
                .state("blogs-edit", {
                    url: "/blogs/edit/:id",
                    template: "<admin-blogs-edit blogdb='$resolve.blog'></admin-blogs-edit>",
                    resolve: {
                        blog: [
                            "BlogsService", "$stateParams", function(BlogsService, $stateParams) {
                                return BlogsService.GetBlog($stateParams.id);
                            }
                        ]
                    }
                });
            
            $urlRouterProvider.otherwise("/");
        }
    ]);

})(window.angular);
// ---------- PROFILE FACTORY MODULE -------- //
(function (angular) {

    var app = angular.module("adminApp");

    //profile factory / service
    function profileFactoryController($http, toastr, $state,PhotosService,$q) {

        //service singleton
        var profileFactory = {};

        // --- privates ---- //

        var profileObject = null;

        var socialLinks = null;

        var reviews = null;

        var references = null;



        // ---- publics ---- //


        //returns profile information
        profileFactory.GetProfile = function () {
            return profileObject;
        }


        //refreshes profile / gets data from api
        profileFactory.RefreshProfile = function () {
            return $http.get("/api/profile")
                .then(function (success) {
                    profileObject = success.data;
                }, function (err) {
                    toastr.error(err.data, "Error");
                    console.log(err);
                });
        }

        // updates profile information
        profileFactory.UpdatePersonalInformation = function (profile) {

            //create multipart form data
            var formData = new FormData();
            for (var prop in profile) {
                if (profile[prop] != null) {
                    formData.append(prop, profile[prop]);
                }
            }

            //put request
            $http.put("/api/profile", formData, {
                    transformRequest: angular.identity,
                    headers: { "Content-Type": undefined }
                })
                .then(function () {

                    //success
                    toastr.success("Profile successfulyl updated.", "Success");

                },
                function (err) {
                    
                    //err
                    toastr.error(err.data, "Error");

                });
        }




        //returns profile social links
        profileFactory.GetSocialLinks = function() {
            return socialLinks;
        }

        //retrieves profile social links
        profileFactory.RefreshSocialLinks = function() {

            return $http.get("/api/socials")
                .then(function(success) {

                    //set social links array
                    socialLinks = success.data;

                },
                    function(err) {
                        console.log(err);
                    });

        }

        //update profile social link info
        profileFactory.UpdateSocialLink = function(link) {
            $http.put("/api/socials",link)
                .then(function(success) {
                    toastr.success(success.data, "Success");
                    $state.go($state.current, {}, { reload: true });
                    },
                    function(err) {
                        console.log(err);
                        toastr.error(err.data,"Error")
                    });
        }


        // ---- REVIEWS ---- //

        //get reviews
        profileFactory.GetReviews = function() {
            return reviews;
        }

        //refresh reviews
        profileFactory.RefreshReviews = function() {
            return $http.get("/api/reviews")
                .then(function(success) {

                        //reviews got successfully
                        reviews = success.data;
                    },
                    function(err) {

                        //err getting reviews
                        toastr.error(err.data, "Error");
                        console.log(err);
                    });
        }

        //update review
        profileFactory.UpdateReview = function(review) {
            $http.put("/api/reviews", review)
                .then(function (success) {
                    toastr.success("Review successfully updated.", "Success");
                    $state.go("about-reviews",{},{reload:true}); //reload page to see new reviews
                },
                    function(err) {
                        toastr.error(err.data, "Error");
                    });
        }

        //create review
        profileFactory.CreateReview = function(review) {
            $http.post("/api/reviews", review)
                .then(function(success) {
                    toastr.success("Review added successfuly.", "Success");
                    reviews.push(success.data);
                },
                    function(err) {
                        toastr.error(err.data, "Error");
                    });
        }

        //delete review
        profileFactory.DeleteReview = function(review) {
            $http.delete("/api/reviews", { params: { id: review.PhotoShootReviewId } })
                .then(function (success) {

                    //show toastr
                    toastr.success("Review successfully deleted.", "Success");

                    //remove review from current reviews
                    var reviewIndex = reviews.indexOf(review);
                    reviews.splice(reviewIndex, 1);
                },
                    function (err) {
                        //catch err 
                        toastr.error(err.data, "Error");
                    });
        }


        // ---- REFERENCES ---- //

        //gets private variable which contains references
        profileFactory.GetReferences = function() {
            return references;
        }

        //refreshes references from db and returns promise
        profileFactory.RefreshReferences = function() {
            return $http.get("/api/references")
                .then(function(success) {
                    references = success.data;
                }, function(err) {
                    console.log(err);
                });
        }

        //deletes reference from db
        profileFactory.DeleteReference = function(reference) {
            $http.delete("/api/references", {
                    params: {
                        id: reference.Id
                    }
            }).then(function () {

                    //show toastr
                    toastr.success("Reference successfully deleted.", "Success");

                    //remove deleted reference from references array
                    var referenceIndex = references.indexOf(reference);
                    references.splice(referenceIndex, 1);
                })
                .catch(function (err) {
                    //show toastr with err
                toastr.error(err.data, "Error");
            });
        }

        //get photos for reference page
        profileFactory.PhotosForReferencePage = function (pageSize) {

            var deferred = $q.defer();


            //get photos list
            PhotosService.GetPhotos(pageSize, null, function () {


                //no photos
                if (PhotosService.Photos.length == 0) {
                    deferred.reject();
                }

                ////get photos with less info for reference page
                //var photos = PhotosService.Photos.map(function(item) {
                //    return {
                //        id: item.PhotoId,
                //        title: item.PhotoTitle,
                //        url: item.PhotoUrl

                //    };
                    
                //});

                //resolve with photos
                deferred.resolve({ photos: PhotosService.Photos });


            });
            return deferred.promise;
        }

        //create reference
        profileFactory.CreateReference = function(reference) {
            $http.post("/api/references", reference)
                .then(function(success) {
                    toastr.success("Reference saved successfully.");
                    if ($state.current.name === "about-references-add") {
                        $state.go("about-references");
                    }

                },
                    function(err) {
                        toastr.error(err.data, "Error");
                    });
        }

        //getsinglereference
        profileFactory.GetSingleReference = function (id) {
            return $http.get("/api/references/" + id);
        }

        //edit reference
        profileFactory.EditReference = function(reference) {
            $http.put("/api/references", reference)
                .then(function () {
                    //success
                    toastr.success("Reference successfully changed.", "Success");
                    if ($state.current.name === "about-references-edit") {
                        $state.go("about-references");
                    }
                },
                    function (err) {
                        //catch err
                        toastr.Error(err.data, "Error");
                    });
        }

        //return singleton
        return profileFactory;
    }

    profileFactoryController.$inject = ["$http", "toastr","$state","PhotosService","$q"];

    //register factory
    app.factory("ProfileService", profileFactoryController);

})(window.angular);
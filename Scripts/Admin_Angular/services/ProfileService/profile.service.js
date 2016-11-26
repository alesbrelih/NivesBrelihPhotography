// ---------- PROFILE FACTORY MODULE -------- //
(function (angular) {

    var app = angular.module("adminApp");

    //profile factory / service
    function profileFactoryController($http, toastr, $state) {

        //service singleton
        var profileFactory = {};

        // --- privates ---- //

        var profileObject = null;

        var socialLinks = null;

        var reviews = null;



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

        //return singleton
        return profileFactory;
    }

    profileFactoryController.$inject = ["$http", "toastr","$state"];

    //register factory
    app.factory("ProfileService", profileFactoryController);

})(window.angular);
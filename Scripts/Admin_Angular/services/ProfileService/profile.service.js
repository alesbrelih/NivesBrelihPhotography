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

        //return singleton
        return profileFactory;
    }

    profileFactoryController.$inject = ["$http", "toastr","$state"];

    //register factory
    app.factory("ProfileService", profileFactoryController);

})(window.angular);
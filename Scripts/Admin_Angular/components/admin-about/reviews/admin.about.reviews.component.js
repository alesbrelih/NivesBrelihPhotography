// ----- admin about reviews component ---- //
(function (angular) {

    //reference to main module
    var app = angular.module("adminApp");

    //component controller
    function aboutReviewsController(ProfileService) {

        var vm = this;

        ProfileService.RefreshReviews()
            .then(function () {
                //sets reviews if those were retrieved successfully
                vm.Reviews = ProfileService.GetReviews();
            },
                function (err) {
                    //console log er
                    console.log(err);
                });

    }

    //inject needed services
    aboutReviewsController.$inject = ["ProfileService"];


    //register component
    app.component("adminAboutReviews", {
        controller: aboutReviewsController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/reviews/admin.about.reviews.component.html"
    });

})(window.angular);
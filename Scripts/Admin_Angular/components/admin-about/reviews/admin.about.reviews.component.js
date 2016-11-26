// ----- admin about reviews component ---- //
(function (angular) {

    //reference to main module
    var app = angular.module("adminApp");

    //component controller
    function aboutReviewsController(ProfileService,$uibModal) {

        //this scope
        var vm = this;

        //paging props
        vm.currentPage = 1;
        vm.pageSize = 5;

        //order reviews prop
        vm.orderBy = "ReviewerName";


        //current review user is working on
        vm.currentReview = null;
        

        //gets all reviews
        ProfileService.RefreshReviews()
            .then(function () {
                //sets reviews if those were retrieved successfully
                vm.Reviews = ProfileService.GetReviews();

                vm.allPages = Math.ceil(vm.Reviews.length / vm.pageSize);
            },
                function (err) {
                    //console log er
                    console.log(err);
                });


        //starts new review
        vm.StartNewReview = function() {

            vm.currentReview = {
                ReviewerName: "",
                Review: ""
            };

        };

        //creates review
        vm.CreateReview = function () {

            //modal to confirm creation of review
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "upload";
                    },
                    entry: function () {
                        return "review";
                    }
                }
            });

            //action depends on modal result
            modal.result.then(function () {

                    //user confirmed modal
                    ProfileService.CreateReview(vm.currentReview);

                })
                .catch(function (err) {

                    //err catch
                    console.log(err);

                });
    
        };


        //creates copy of object we want to edit
        vm.SelectToEdit = function (review) {

            vm.currentReview = {
                PhotoShootReviewId: review.PhotoShootReviewId,
                ReviewerName: review.ReviewerName,
                Review: review.Review
                
            };
            
        }

        //updates review
        vm.UpdateReview = function () {

            //new modal to confirm action
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "edit";
                    },
                    entry: function () {
                        return "review";
                    }
                }
            });

            //if user agrees to edit then edit
            modal.result.then(function() {

                ProfileService.UpdateReview(vm.currentReview);

            })
                .catch(function (err) {
                console.log(err);
            });
            
        };

        //deletes review
        vm.DeleteReview = function (review) {

            //new modal to confirm delete
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "review";
                    }
                }
            });

            //if user agrees to delete review on modal then delete it
            modal.result.then(function() {

                ProfileService.DeleteReview(review);

            })
                .catch(function (err) {
                console.log(err);
            });
      
        }


        //changes order in which reviews are displayed in table
        vm.changeOrder = function (order) {

            //clicked on same order, then order it in other direction
            if (order == vm.orderBy) {
                vm.orderBy = "-" + order;
            } else {
                // not same order
                vm.orderBy = order;
            }
        }


    }

    //inject needed services
    aboutReviewsController.$inject = ["ProfileService","$uibModal"];


    //register component
    app.component("adminAboutReviews", {
        controller: aboutReviewsController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/reviews/admin.about.reviews.component.html"
    });

})(window.angular);
// ---- SOCIAL LINKS COMPONENT MODULE ---- //

(function(angular) {
    
    // reference to main app/module
    var app = angular.module("adminApp");

    // social links controller
    function socialLinksController(ProfileService) {
        var vm = this;
        vm.selected = {};

        // set socials links
        ProfileService.RefreshSocialLinks()
            .then(function() {
                vm.SocialLinks = ProfileService.GetSocialLinks();
                console.log(vm.SocialLinks);
            })
            .catch(function(err) {
            console.log(err);
            });


        // set selected social link
        vm.SelectLink = function (link) {
            
            //hide if clicked on same btn
            if (vm.selected.ProfileLinkId && vm.selected.ProfileLinkId == link.ProfileLinkId) {
                vm.ResetEdit();
            } else {
                //show other input
                vm.selected.ProfileLinkId = link.ProfileLinkId;
                vm.selected.LinkUrl = link.LinkUrl;
                vm.selected.LinkDescription = link.LinkDescription;
                vm.selected.ShownOnProfile = link.ShownOnProfile;
            }
           
        }

        // updates social link info
        vm.UpdateSocialLink = function() {
            ProfileService.UpdateSocialLink(vm.selected);
        }

        vm.ResetEdit = function() {
            vm.selected = {};
        }
    }

    //inject services
    socialLinksController.$inject = ["ProfileService"];


    //register component on app
    app.component("adminAboutSocialLinks", {
        controller: socialLinksController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/admin-about/social-links/admin.about.social.links.component.html"
    });

})(window.angular);
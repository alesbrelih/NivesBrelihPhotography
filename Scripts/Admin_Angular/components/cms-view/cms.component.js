// ---- CMS COMPONENT MODULE --- //
(function(angular) {
    
    //ref to main app
    var app = angular.module("adminApp");

    // cms controller function
    function cmsComponentController($sce,$scope,CmsService) {
        
        //current scope
        var vm = this;

        // --- PROPERTIES --- //

        vm.props = {
            content: "",
            selected: null,
            lastElement: "",
            elementsAdded: 0
        };



        //set / reset content for cms service
        CmsService.SetContent(vm.props);


        // set photos list reference
        //CmsService.SetPhotos(vm.photos);


        //displayed html
        vm.safe = "";

        //show preview window
        vm.showPreview = false;

        vm.utilities = [
            {
                classes: "btn btn-info",
                text: "new line",
                type: "line"
            },
            {
                classes: "btn btn-info",
                text: "new paragraph",
                type: "paragraph"
            },
            {
                classes: "btn btn-info",
                text: "bold",
                type: "strong"
            },
            {
                classes: "btn btn-info",
                text: "italic",
                type: "italic"
            },
            {
                classes: "btn btn-info",
                text: "H1",
                type: "heading-1"
            },
            {
                classes: "btn btn-info",
                text: "H2",
                type: "heading-2"
            },
            {
                classes: "btn btn-info",
                text: "H3",
                type: "heading-3"
            },
            {
                classes: "btn btn-info",
                text: "h - line",
                type: "horizontal-line"
            },
            {
                classes: "btn btn-info",
                text: "photo",
                type: "insert-photo"
            },
            {
                classes: "btn btn-info",
                text: "row",
                type: "row"
            },
            {
                classes: "btn btn-info",
                text: "1/2 column",
                type: "half-column"
            },
            {
                classes: "btn btn-info",
                text: "1/3 column",
                type: "third-column"
            },


        ];


        //inserts element
        vm.addElement = function(type){
            if (type == "insert-photo") {
                vm.selectPhoto = !vm.selectPhoto;
            }
            CmsService.Tools.Insert(type);
        };

        //inserts photo
        vm.addPhoto = function (photoUrl){
            CmsService.Tools.InsertPhoto(photoUrl);
            vm.selectPhoto = false; //hide photo select wrapper
        };



        // --- WATCH --- //

        //watch and set
        $scope.$watch("vm.props.content", function () {

            //trust inserted html
            vm.safe = $sce.trustAsHtml(vm.props.content);

        });


    }

    //inject cms service
    cmsComponentController.$inject = ["$sce","$scope","CmsService"];

    //register component
    app.component("abCms", {
        controller: cmsComponentController,
        controllerAs: "vm",
        templateUrl: "/Scripts/Admin_Angular/templates/components/cms-view/cms.component.html",
        bindings: {
            photos:"="
        }
    });

})(window.angular);
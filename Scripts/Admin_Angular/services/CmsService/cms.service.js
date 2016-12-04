// --- CMS SERVICE MODUEL --- //
(function(angular) {
    
    //services app module
    var services = angular.module("adminApp.services");

    //service
    function cmsServiceFactory() {
        var cmsFactory = {};


        return cmsFactory;
    }

    //register service
    services.factory("CmsService", cmsServiceFactory);

})(window.angular);
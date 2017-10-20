(function (angular) {
	
	//get services module to register factory on
	var services = angular.module("adminApp.services");

	function termsConditionsController($http, toastr) {
		
		// returned singleton
		var termsConditionsFactory = {};

		// get existing
		termsConditionsFactory.GetTermsAndConditions = function () {
            return $http.get("/api/termsconditions")
				.then(function (res) {
					return res.data;
				})
				.catch(function (err) {
					toastr.error(err.data, "Error");
				});
		}
		// set 
		termsConditionsFactory.SetTermsAndConditions = function (termsConditions) {
			
            return $http.post("/api/termsconditions", termsConditions)
				.then(function (res) {
					toastr.success(res.data, "Success");
				})
				.catch(function (err) {
					toastr.error(err.data, "Error");
				});
		}

		return termsConditionsFactory;

	}

	// inject dependencies for the minification
	termsConditionsController.$inject = ['$http', 'toastr'];

	services.factory("TermsConditionsService", termsConditionsController)

})(window.angular);
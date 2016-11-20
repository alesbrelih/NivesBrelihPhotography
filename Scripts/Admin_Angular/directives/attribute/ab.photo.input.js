// ------ photo upload directive for input[type="file"] ----- //

(function(angular) {
	
	//main module / app reference
	var app = angular.module("adminApp");

	//directive controller
	function photoInputController() {

		function linkFunction(scope, el, attr, ngCtrl) {
			//if no ngCtrl don't do anything (ng-model)
		    if (!ngCtrl) {
		    	return;
		    }


			// ----- VALIDATORS ------ //

			//valid type
		    ngCtrl.$validators.validType = function(modelValue, viewValue) {
		        var value = modelValue || viewValue;

		        if (!value) {
		            return true; //no file was uploaded so its valid type
		        }
		        if (value.type.startsWith("image")) {
		            return true; // valid types
		        }
		        return false;
		    }

			//CHECK IF ALL VALIDATION RETURNS TRUE AND SET IMG PREVIEW
		    ngCtrl.$viewChangeListeners.push(function () {
				//if el is valid
				if (ngCtrl.$valid) {
				    
					//set fileReader
					var reader = new FileReader();
					reader.onload = function (event) {
						//if successfully read, set the preview img prop
				        scope.previewImg = event.target.result;
					};

					//read file
				    reader.readAsDataURL(ngCtrl.$modelValue);


				}
		    });




		    function onElChangeFnc() {

				//currently supports only single file input
		        var file = el[0].files[0];
		        ngCtrl.$setViewValue(file); //runs validators
		    }

		    el.on("change", onElChangeFnc);
		    el.on("$destroy", function() {
		        el.off("change", onElChangeFnc);
		    });
		}

	    return {
	    	restrict: "A", //restrict to attribute
	    	require: "ngModel", //requires ngModel attr
			scope:{
				previewImg:"=" //directive that will have scope prop that will point to preview picture
			},
			link:linkFuction //link function
	    };
	}

	//registers directive
    app.directive("abPhotoInput", photoInputController);

})(window.angular);
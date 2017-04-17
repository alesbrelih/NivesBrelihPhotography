(function(angular) {

    var app = angular.module("adminApp");

    //working with factory
    function workingWithFactoryService($http,toastr,$state,$uibModal) {
        
        // -- privates -- //
        var workingWithList = [];

        //returned singleton
        var workingWithSingleton = {};

        //publics

        //get list
        workingWithSingleton.List = workingWithList;

        //refresh
        workingWithSingleton.Refresh = function() {
            return $http.get("/api/workingwith")
                .then(function(success) {

                    //clear previous
                    while (workingWithList.length > 0) {
                        workingWithList.pop();
                    }

                    for (var i = 0; i < success.data.length; i++) {
                        workingWithList.push(success.data[i]);
                    }


                }, function(err) {
                toastr.error(err.data, "Error"); //show err toastr
            });
        }

        //get single
        workingWithSingleton.Get = function(id) {
            return $http.get("/api/workingwith", {
                params: {
                    id: id
                }
            }).then(function(success) {
                return success.data;
            }, function(err) {
                toastr.error(err.data, "Error");
            });
        }

        //add new
        workingWithSingleton.Add = function (item) {

            var modalUpload = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "upload";
                    },
                    entry: function () {
                        return "working-with";
                    }
                }
            });

            modalUpload.result.then(function() {
                //create form data for multitype form upload
                var multiForm = new FormData();
                for (var prop in item) {
                    console.log(prop, item[prop]);
                    multiForm.append(prop, item[prop]);
                }


                return $http.post("/api/workingwith", multiForm, {
                        transformRequest: angular.identity,
                        headers: { "Content-Type": undefined }
                    })
                    .then(function() {
                        toastr.success("Succesfully added", "Success");
                        $state.go("about-working-with");
                    }, function(err) {
                        toastr.error(err.data, "Error");
                    });
            });
        }

        //edit
        workingWithSingleton.Edit = function (item) {
            console.log(item);
            var multiForm = new FormData();
            for (var prop in item) {
                multiForm.append(prop, item[prop]);
            }
            return $http.put("/api/workingwith", multiForm, {
                        transformRequest: angular.identity,
                        headers: { "Content-Type": undefined }
                    })
                .then(function() {
                    toastr.success("Succesfully edited", "Success");
                    $state.go("about-workingwith");
            }, function(err) {
                toastr.error(err.data, "Error");
            });
        }

        //remove
        workingWithSingleton.Remove = function (id) {
            var modal = $uibModal.open({
                component: "abModalView",
                size: "sm",
                resolve: {
                    type: function () {
                        return "delete";
                    },
                    entry: function () {
                        return "working-with";
                    }
                }
            });
            modal.result.then(function() {
                return $http.remove("/api/workingwith", {
                    params: {
                        id: id
                    }
                }).then(function() {
                    toastr.success("Item deleted successfuly.", "Success");
                    $state.go("about-working-with");
                }, function(err) {
                    toastr.error(err.data, "Error");
                });
            });
        }

        return workingWithSingleton;
    }
    //needed modules
    workingWithFactoryService.$inject = ["$http", "toastr","$state","$uibModal"];

    //register factory
    app.factory("WorkingWithService", workingWithFactoryService);


})(window.angular);
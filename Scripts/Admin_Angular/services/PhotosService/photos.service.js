(function(angular) {
    
    //get main module
    var app = angular.module("adminApp.services");

    //returned factory
    function PhotosServiceFactory($http, toastr, $state) {

        // ----- PRIVATES -------- //
        var _photos = [];

        var currentPhoto = null;



        //returned singleton
        var photosFactory = {};


        // ------ PROPS -----------//

        //all photos
        photosFactory.Photos = _photos;
        


        //------- METHODS --------//

        //delete photo from DB and returns promise

        photosFactory.RemovePhoto = function (photo) {
            //calls api to delete selected photo
            console.log("PHOTO SERVICE: ");
            console.log(photo);
            var promise = $http.delete("/api/photos", {
                params: { "id": photo.PhotoId }
            });

            return promise;

        }

        //gets photos list from api
        photosFactory.GetPhotos = function (pageSize,cb,finishCb) {

            //reset paging and photo aray

            //paging
            var _page = 0;

            //array
            while (_photos.length !== 0) {
                _photos.pop();
            }

            //recursive function
            function getPhotosFromApi(page) {

                $http.get("/api/photos", { params: { "page": page, "pagesize":pageSize } }).then(function (success) {

                    

                    if (success.status === 200) {
                        //add each returned item to list
                        success.data.forEach(function(el) {
                            _photos.push(el);
                        });

                        //increase page
                        page = ++page;

                        //if cb exists call it
                        if (cb) {
                            cb();
                        }
                        

                        //recursive call untill end of list

                        getPhotosFromApi(page);
                    }
                    else if (success.status === 204) {

                        //when no more data, check for finish cb
                        if (finishCb) {
                            finishCb();
                        }
                    }

                    


                }, function (err) {
    
                    //no more items or err connecting to db
                    console.log(err);
                });
            }


            //gets all photos from
            getPhotosFromApi(_page);

        }

        // gets single photo for photo edit
        photosFactory.GetUserForEdit = function (id) {

            //returns promise
            return $http.get("/api/photos", {
                params: {
                    "id": id
                }
            }).then(function (success) {
                currentPhoto = success.data;
            }, function (err) {
                $state.go("photos");
                console.log(err);
            });
        }

        // saves changes for edited photo in db
        photosFactory.EditPhoto = function(photo) {
            console.log(photo);
            $http.put("/api/photos", photo)
                .then(function() {

                    toastr.success("Photo information changed successfully.", "Success");

                    //change item in list array if exists (avoid extra calls to server)

                    
                    
                    $state.go("photos",{},{reload:true});

            }, function(err) {

                toastr.error(err.data, "Error");
            });

        }
        
        //adds new photo to DB
        photosFactory.UploadPhoto = function (photo) {

            //create form data for multitype form upload
            var multiForm = new FormData();
            for (var prop in photo) {
                multiForm.append(prop, photo[prop]);
            }
            
            //request for api with photo content appended - set it multitype form
            $http.post("/api/photos", multiForm, {
                transformRequest: angular.identity,
                headers: { "Content-Type": undefined }
            })
                .then(function(success) {

                    toastr.success("Photo successfully uploaded.", "Success");

                    //change state only if current photo-add
                    if ($state.current.name === "photos-add") {
                        $state.go("photos");
                    } else {
                        //add phto to db
                        _photos.push(success.data);
                    }
                    

                    //reset photo container
                    photo = null;

                },
                    
                    function(err) {
                        console.log(err);
                        toastr.error(err.data,"Error");
                    });
        }

        
        //gets current photo
        photosFactory.GetCurrentPhoto = function() {
            return currentPhoto;
        }




        return photosFactory;
        

    }

    PhotosServiceFactory.$inject = ["$http","toastr","$state"];

    //register factory
    app.factory("PhotosService", PhotosServiceFactory);

})(window.angular);
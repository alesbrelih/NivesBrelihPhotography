(function(angular) {
    
    //get main module
    var app = angular.module("adminApp.services");

    //returned factory
    function PhotosServiceFactory($http,toastr,$state) {

        //returned singleton
        var photosFactory = {};



        // ----- PRIVATES -------- //
        var _photos = [];

        var currentPhoto = {
            Id:null,
            PhotoTitle: "",
            AlbumId: "",
            IsAlbumCover:false,
            PhotoUrl: "",
            IsOnPortfolio: false,
            PhotoCategories:[]

        };


        function setCurrentPhoto(photo) {
            currentPhoto.Id = photo.Id;
            currentPhoto.PhotoTitle = photo.PhotoTitle;
            currentPhoto.AlbumId = photo.AlbumId;
            currentPhoto.IsAlbumCover = photo.IsAlbumCover;
            currentPhoto.PhotoUrl = photo.PhotoUrl;
            currentPhoto.IsOnPortfolio = photo.IsOnPortfolio;
            console.log(currentPhoto);
            photo.PhotoCategories.forEach(function(item) {
                currentPhoto.PhotoCategories.push(item);
            });

        }

       



        // ------ PROPS -----------//

        //all photos
        photosFactory.Photos = _photos;
        photosFactory.CurrentPhoto = currentPhoto;





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
        photosFactory.GetPhotos = function (pageSize,cb) {

            //paging
            var _page = 0;

            //recursive function
            function getPhotosFromApi(page) {
                $http.get("/api/photos", { params: { "page": page, "pagesize":pageSize } }).then(function (success) {

                    //add each returned item to list
                    success.data.forEach(function(el) {
                        _photos.push(el);
                    });
                    
                    //increase page
                    page = ++page;

                    //if cb exists call it
                    cb();

                    //recursive call untill end of list

                    getPhotosFromApi(page);


                }, function (err) {
                    console.log(_photos);
                    //no more items or err connecting to db
                    console.log(err);
                });
            }


            //gets all photos from
            getPhotosFromApi(_page);

        }

        // gets single photo for photo edit
        photosFactory.GetUserForEdit = function(id) {
            $http.get("/api/photos", {
                params: {
                    "id": id
                }
            }).then(function (success) {
                console.log(success.data);
                setCurrentPhoto(success.data);
            },function(err) {
                console.log(err);
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

                    toastr.success("Photo successfully uploaded.","Success");
                    $state.go("photos");
                },
                    
                    function(err) {
                        console.log(err);
                        toastr.error(err.data,"Error");
                    });
        }

        return photosFactory;

    }

    PhotosServiceFactory.$inject = ["$http","toastr","$state"];

    //register factory
    app.factory("PhotosService", PhotosServiceFactory);

})(window.angular);
(function(angular) {
    
    //get main module
    var app = angular.module("adminApp");

    //returned factory
    function PhotosServiceFactory($http) {

        //returned singleton
        var photosFactory = {};



        // ----- PRIVATES -------- //
        var _photos = [];

       



        // ------ PROPS -----------//

        //all photos
        photosFactory.Photos = _photos;





        //------- METHODS --------//

        photosFactory.RemovePhoto = function() {
            console.log("GONNA REMOVE THE PHOTO YO!");
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

        

        return photosFactory;

    }

    PhotosServiceFactory.$inject = ["$http"];

    //register factory
    app.factory("PhotosService", PhotosServiceFactory);

})(window.angular);
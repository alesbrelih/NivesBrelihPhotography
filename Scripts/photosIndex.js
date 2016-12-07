
function SetMasonry() {
    $($("#photos").imagesLoaded(function () {
        $('.masonry-layout').masonry({
            // options
            itemSelector: '.masonry-image',
            columnWidth: '.grid-sizer'
            //percentPosition: true
        });
    }));

    //delegating hover to display discription even on dynamically added photos
    $(".masonry-layout").on("mouseenter mouseleave", ".masonry-image", function () {
        $(this).find(".img-description").toggleClass("show-description"); //shows description
    });

}

//DOM on READY function
///////////////////////

$(function () {

    //set scroll for category navigation
        $("#category-navigation").niceScroll();

    //function that sets masonry on photos
    

    //currentPageofPhotos
    var pageNm = 0;

    //var to set it to true when ajax doesnt return any more photos,
    //stops extra functions on page
    var photosEnd = false;

    //masonry initialize
    SetMasonry();

    //enable photo enlargement on click
    EnablePhotoZoom("#photo-container", ".masonry-image");

    //function to dynamically load json pictures 
    function loadPictures() {
        //route to controller that returns json
        $.get("/Photos/LoadPhotos", { pageNumber: pageNm + 1 }, function (data) {
            //checking if returned json is null and stopping function
            if (jQuery.isEmptyObject(data)) {

                //we reached all photos in that category
                photosEnd = true;
                return;
            }
            //html to be inserted
            var html = "";
            $.each(data, function (i, item) {
                var row = '<div class="masonry-image img-container col-xs-12 col-sm-6 col-md-4">' +
                        '<img src="' + item.PhotoUrl + '" class="img-responsive"/>' +
                        '<div class="img-description"><span>' + item.PhotoTitle + '</span></div>' +
                        '</div>';
                html += row;
            });
            //changing html to json so masonry appended can be called on
            var jqueryHtml = jQuery(html);
            //addin masonry to new photos
            var photoContainer = $("#photos");
            photoContainer.append(jqueryHtml);
            photoContainer.imagesLoaded(function() {
                photoContainer.masonry('appended', jqueryHtml);
            });
            pageNm++;
        });
    }
    //when scroled to bottom trying to load more images if wanted to
    $(window).scroll(function () {

        //if we havent reached the end of photos already we try loading more
        if (!photosEnd) {
            if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                loadPictures();
            }
        }
    });

    $("#category-selector").children().on("click", function () {
        //when clicked on random category page is reseted 
        //and the if that allows checking for new photos is reseted
        photosEnd = false;
        pageNm = 0;


        //when clicked on category slide the category selector back in
        $("#category-navigation").slideToggle();

        //clicked on button it toggles visual enchanchement on it to see it was clicked 
        //(gray bg, black color)
        $("#category-button").toggleClass("category-button-active");



    });

    //click on category fixed
    $("#category-button").click(function () {
        //slide the category selector div
        $("#category-navigation").slideToggle();

        //toggle button state 
        $(this).toggleClass("category-button-active");
    });


}
       );
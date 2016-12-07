// functional scoping, to avoid global variables

(function (jQuery) {
    var $ = jQuery;

    $(function () {

        //set up nicescroll
        $(".album-view-description .description-wrapper .text-wrapper .description").niceScroll();

        var $albumId = $(".album-view-description").attr("data-album-id");

        var pageControl = {
            pageNumber: 0,
            endScroll: false
        }
        //load more photos
        function loadContent() {

            $.get("/Albums/Album", { albumId: $albumId, pageNumber: pageControl.pageNumber + 1 }, function (data) {

                if (jQuery.isEmptyObject(data)) {

                    //if nothing is returned we end scroll function that loads more photos
                    pageControl.endScroll = true;
                    return;
                }

                var $html = "";

                $.each(data, function (index, item) {
                    var row = '<div class="masonry-image img-container col-xs-12 col-sm-6 col-md-4">' +
                        '<img src="' + item.PhotoUrl + '" class="img-responsive"/>' +
                        '<div class="img-description"><span>' + item.PhotoTitle + '</span></div>' +
                        '</div>';
                    $html += row;
                });

                //changing html to json so masonry appended can be called on
                var jqueryHtml = jQuery($html);

                //addin masonry to new photos
                var container = $("#photos");
                container.append(jqueryHtml);
                container.imagesLoaded(function () {
                    container.masonry('appended', jqueryHtml);
                    pageControl.pageNumber++;
                });


            });

        }

        //show background on hover for better visibiltiy :D
        EnableShowBackgroundOnHover("#content-container", ".masonry-image", "photos");

        //enable enlarge photo
        EnablePhotoZoom("#content-container", ".masonry-image");

        //setMasonry
        $($("#content-container").imagesLoaded(function () {
            $('.masonry-layout').masonry({
                // options
                itemSelector: '.masonry-image',
                columnWidth: '.grid-sizer'
                //percentPosition: true
            });
        }));

        //on scroll
        $(window).scroll(function () {

            if (!pageControl.endScroll) {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {  //check for new albums/photos once we reach bottom

                    loadContent();

                }
            }
        });
    });

})(window.jQuery);



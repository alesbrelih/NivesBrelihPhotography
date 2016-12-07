/*fuck global scoping*/
(function(enableShowBackgroundOnHover, enablePhotoZoom, $) {
    $(function () {

        //page control
        var pageControl = {
            pageNumber: 0,
            endScroll: false
        }

        //enable show photo titles on hoves
        enableShowBackgroundOnHover("#selected-reference-photos", ".img-container", "photos");
        //enable enlarge photo on click
        enablePhotoZoom("#selected-reference-photos", ".img-container");

        //function that loads more photos on scroll down if they exist
        function loadMorePhotos() {

            //getting reference id
            var $referenceId = $(".selected-reference-description").attr("data-reference-id");

            $.get("/About/Reference", { referenceId: $referenceId, pageNumber: pageControl.pageNumber + 1 }, function (data) {

                //reached end of photos - no return data
                if (jQuery.isEmptyObject(data)) {
                    pageControl.endScroll = true;
                    return;

                }

                //html to be appended with photo data
                var html = "";
                $.each(data, function (index, item) {
                    var $row = '<div class="img-container">' +
                        '<img src="' + item.PhotoUrl + '" class="img-responsive"/>' +
                        '<div class="img-description">' +
                        '<span>' + item.PhotoTitle + '</span>' +
                        '</div>' +
                        '</div>';
                    html += $row;
                });

                var $html = $(html);
                $("#selected-reference-photos").append($html);
                pageControl.pageNumber++;
            });

        }


        //function on scroll
        $(document).scroll(function () {
            if (!pageControl.endScroll) {

                if ($(document).height() == $(window).scrollTop() + $(window).height()) {

                    loadMorePhotos();
                }
            }
        });

    });

})(window.EnableShowBackgroundOnHover, window.EnablePhotoZoom, window.jQuery);


(function(jQuery) {
    var $ = jQuery;

    $(function () {
        var $ = jQuery;
        var pageNm = 1; //starting page number
        var scrollEnd = false;


        /*Masonry function*/
        function setMasonry() {
            $($("#content-container").imagesLoaded(function () {
                $('.masonry-layout').masonry({
                    // options
                    itemSelector: '.masonry-image',
                    columnWidth: '.grid-sizer'
                    //percentPosition: true
                });
            }));
        }

        //set show description
        $(".masonry-layout").on("mouseenter mouseleave", ".masonry-image", function () {
            $(this).find(".album-background").toggleClass("show-background");
        });

        function loadContent() {
            //get new albums on controller with next pageNumber
            $.get("/Projects/"+pageNm, function (data) {

                //check if it didnt return anything (end of photos/albums)
                if (jQuery.isEmptyObject(data)) {
                    scrollEnd = true;
                    return;
                }

                //html which will be added to dom
                var $html = "";


                $.each(data, function (index, item) {

                    //create proper html markings for each of the new albums
                    var $row = "<div class='masonry-image img-container col-xs-12 col-sm-6 col-md-4'>" +
                        '<div class="img-wrapper">'+
                            "<a href='/Projects/Project/" + item.PhotoAlbumId + "'>" +
                                "<img alt='" + item.AlbumName + "' src='/Images/Photos/MIN/" + item.AlbumPhotoUrl + "' class='img-responsive' data-album-id='" + item.PhotoAlbumId + "'/>" +
                                "<div class='album-background'>" +
                                "<span class='album-name'>" + item.AlbumName + "<span class='album-date'>" + item.AlbumDateMonthAsString + "</span></span>" +
                                "</div>" +
                            "</a>"+
                        '</div>'+
                        "</div>";

                    //append it to whole html which will be added
                    $html += $row;

                });


                //make jquery object so masonry can use appended method
                var jqueryHtml = $($html);

                //hide before append
                jqueryHtml.hide();

                //append to photos and run masonry
                var photoContainer = $("#photos");
                photoContainer.append(jqueryHtml);
                photoContainer.imagesLoaded(function () {

                    //show new content after photos rendered
                    jqueryHtml.show();
                    photoContainer.masonry("appended", jqueryHtml);
                });

                //increase page number
                pageNm++;

            });
        }

        setMasonry();

        //on scroll
        $(window).scroll(function () {

            if (!scrollEnd) {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) { //check for new albums/photos once we reach bottom
                    loadContent();
                }
            }
        });

    });

})(window.jQuery);
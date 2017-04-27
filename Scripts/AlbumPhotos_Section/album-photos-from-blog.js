// functional scoping, to avoid global variables
$("#move-to-bottom").bgLoaded(
{
    afterLoaded: function () {
        $(".description-wrapper").animate({
            width: "30%" 
            }, 4000);
    }

});

(function (jQuery,lightbox, document) {
    var $ = jQuery;

    function fixHeight() {
        var maxHeight = 0;
        $.each($(".owl-item"),function(index, item) {
            if ($(item).height() > maxHeight) {
                maxHeight = $(item).height();
            }
        });
        $(".owl-stage, .owl-stage-outer").css("height", maxHeight);
    }

    $(window).load(function () {
        $(".owl-carousel.album-body").owlCarousel({
            autoWidth: true,
            autoplay: true,
            loop: true,
            margin: 3,
            center: true,
            dots: true,
            autoplayHoverPause: true,
            smartSpeed: 2000,
            navSpeed: 2000,
            autoplaySpeed: 2000,
            navText: ['<span class="glyphicon glyphicon-chevron-left"></span>', '<span class="glyphicon glyphicon-chevron-right"></span>'],
            onInitialized: function(ev) {
                fixHeight();
            },
            onResized:function(ev) {
                fixHeight();
            }

        });
    });
    

    $(function () {

        //set up nicescroll
        $(".album-view-description .description-wrapper .text-wrapper .description").niceScroll();

        var $albumId = $(".album-view-description").attr("data-album-id");

        var pageControl = {
            pageNumber: 1,
            endScroll: false
        }
        //load more photos
        function loadContent() {

            $.get("/Projects/Project/"+$albumId+"/"+pageControl.pageNumber, function (data) {

                if (jQuery.isEmptyObject(data)) {

                    //if nothing is returned we end scroll function that loads more photos
                    pageControl.endScroll = true;
                    return;
                }

                var $html = "";

                $.each(data, function (index, item) {
                    var row = '<div class="masonry-image img-container col-xs-12 col-sm-6 col-md-4">' +
                        '<div class="img-wrapper">'+
                            '<a href="/Images/Photos/MID/'+item.PhotoUrl+'" data-lightbox="'+item.AlbumId+'">'+
                                '<img alt="'+item.PhotoTitle+'" src="/Images/Photos/MIN/' + item.PhotoUrl + '" class="img-responsive"/>' +
                                '<div class="img-description"><span>' + item.PhotoTitle + '</span></div>' +
                            '</a>'+
                        '</div>'+
                        '</div>';
                    $html += row;
                });

                //changing html to json so masonry appended can be called on
                var jqueryHtml = jQuery($html);

                //hide before append to body
                jqueryHtml.hide();

                //addin masonry to new photos
                var container = $("#photos");
                container.append(jqueryHtml);
                container.imagesLoaded(function () {

                    //show before masonry append
                    jqueryHtml.show();
                    container.masonry('appended', jqueryHtml);
                    pageControl.pageNumber++;
                });


            });

        }

        //show background on hover for better visibiltiy :D
        EnableShowBackgroundOnHover("#content-container", ".masonry-image", "photos");

        //enable enlarge photo
        //EnablePhotoZoom("#content-container", ".masonry-image");
        
        $("#move-to-bottom").on("click", function (ev) {
            var $topOff = $("#photos").offset().top;
            $("body").animate({
                scrollTop: $topOff

            }, 1500);
        });
        lightbox.option({
            showImageNumberLabel: false
        });

       

        //on scroll
        $(window).scroll(function () {

            var arrow = $(".background-img>div.glyphicon");
            if ($(window).scrollTop() == 0) {
                
                if ($(arrow).hasClass("hide")) {
                    $(arrow).fadeIn(200);
                    $(arrow).removeClass("hide");
                }
               

            }
            if ($(window).scrollTop() != 0) {
                if (!$(arrow).hasClass("hide")) {
                    $(arrow).fadeOut(250);
                    $(arrow).addClass("hide");

                }
            }

            if (!pageControl.endScroll) {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {  //check for new albums/photos once we reach bottom

                    loadContent();

                }
            }
        });
    });

})(window.jQuery,window.lightbox);



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

        //because buggy carousel gets initialized event before images shown
        if ($(".owl-carousel.owl-loaded").length != 0) {
            $.each($(".owl-item"), function (index, item) {
                if ($(item).height() > maxHeight) {
                    maxHeight = $(item).height();
                }
            });
            $(".owl-stage-outer").css("height", maxHeight);
        }
        
    }


        

    

    $(function () {
        $(window).load(function () {
            $(".owl-carousel.album-body").owlCarousel({
                autoWidth: true,
                autoplay: true,
                loop: true,
                margin: 3,
                lazyLoad: true,
                center: true,
                dots: true,
                autoplayHoverPause: true,
                smartSpeed: 2000,
                navSpeed: 2000,
                autoplaySpeed: 2000,
                navText: ['<span class="glyphicon glyphicon-chevron-left"></span>', '<span class="glyphicon glyphicon-chevron-right"></span>'],
                onInitialized: function (ev) {
                    fixHeight();
                },
                onInitialize: function (ev) {
                    fixHeight();
                },
                onResized: function (ev) {
                    fixHeight();
                },
                onLoadLazy: function (ev) {
                    fixHeight();
                }

            });
        })
        

        //set up nicescroll
        $(".album-view-description .description-wrapper .text-wrapper .description").niceScroll();

        var $albumId = $(".album-view-description").attr("data-album-id");

        var pageControl = {
            pageNumber: 1,
            endScroll: false
        }
 

        //show background on hover for better visibiltiy :D
        EnableShowBackgroundOnHover("#content-container", ".masonry-image", "photos");

        //enable enlarge photo
        //EnablePhotoZoom("#content-container", ".masonry-image");
        
        $("#move-to-bottom").on("click", function (ev) {
            console.log("CLICK")
            var $topOff = $("#photos").offset().top;
            $("html, body").animate({
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
        });
    });

})(window.jQuery,window.lightbox);



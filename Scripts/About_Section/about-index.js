$(function () {

    $("#social-links-container li").hover(function () {
        $(this).find("img").toggleClass("social-link-img-hovered");
    });
    $(document).imagesLoaded(function () {
        $(".masonry-layout").masonry({
            itemSelector: ".masonry-content",
            columnWidth: ".grid-sizer"
        });
    });

});
/*------------------------*/
/* WELCOME CAROUSEL STYLES*/
/*------------------------*/

$(function () {
    $(".carousel-inner .item").first().addClass("active");

    $(".continue").on("mouseenter", function() {
        $(".enter-page-title").slideDown();
    });
    $(".continue").on("mouseleave", function() {
        $(".enter-page-title").slideUp();
    });
});

$(document).ready(function () {
    setBodyTopPaddingOfNavbar();
});
$(window).resize(function () {
    setBodyTopPaddingOfNavbar();
    var width = document.documentElement.clientWidth;
});

function setBodyTopPaddingOfNavbar() {
    var $navbar = $(".navbar");
    var $navOptions = $(".navbar-collapse.collapse:not(.in)");
    if ($navbar.hasClass('navbar-fixed-top')) {
        //if fixed then body needs proper top padding
        var $navHeight = $navbar.height();
        var $navOptionsHeight = 0;
        if (window.innerWidth > 768) {
            $navOptionsHeight = $navOptions.height();
        }
        var $totalHeight = $navHeight + $navOptionsHeight;
        $("body").css("padding-top", $totalHeight);
    }
}


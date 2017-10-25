(function ($) {

    var scrollInProgress = false;

    $(".working-with-information").on("mousemove", function () {
        var currentEl = $(this);
        if (scrollInProgress != true) {
            scrollInProgress = true;
            currentEl.transition({ x: '0px' }, 500, function () {
                currentEl.find(".anchor-right").fadeOut();
            });
        }

    }).on("mouseleave", function () {
        var currentEl = $(this);

        currentEl.transition({ x: '-80%' }, 500, function () {
            currentEl.find(".anchor-right").fadeIn();
            scrollInProgress = false;
        });

    });

    $(".working-with-container").on("click", function () {
        window.location = $(this).find("a").attr('href');
    });
})(window.jQuery);
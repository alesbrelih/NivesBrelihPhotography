/*fuck global scoping*/
(function(enablePhotoZoom, $) {
    
    //Enable photo zoom on .blog-content img <- blog pictures
    enablePhotoZoom(".blog-description", ".blog-content img");

    //adds horizontal lines for pictures or extra margin if nested
    function addHorizontalLineOrMargin(selector) {
        $(selector).each(function () {
            if ($(this).prev().is("p")) {
                $(this).before("<hr/>");

            }

            //if next element is paragraph add horizontal line bellow
            if ($(this).next().is("p")) {
                $(this).after("<hr/>");

            }

                //if next element is another image add some extra margin on the bottom side
            else if ($(this).next().is("img") || ($(this).next().is(".two-photo-group"))) {
                $(this).addClass("img-bottom-margin");
            }
        });
    }

    //two different container for pictures
    addHorizontalLineOrMargin(".two-photo-group");
    addHorizontalLineOrMargin(".blog-content > img");
    
})(window.EnablePhotoZoom, window.jQuery);


//Enable photo zoom on .blog-content img <- blog pictures
EnablePhotoZoom(".blog-description", ".blog-content img");

//adds horizontal lines for pictures or extra margin if nested
function AddHorizontalLineOrMargin(selector) {
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
AddHorizontalLineOrMargin(".two-photo-group");
AddHorizontalLineOrMargin(".blog-content > img");
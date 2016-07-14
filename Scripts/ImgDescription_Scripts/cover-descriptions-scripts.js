//enable show background on hover
function EnableShowBackgroundOnHover(itemsContainer, itemSelector,typeOfImg) {
    //if displayed items are album covers
    if (typeOfImg === "albums") {
        $(itemsContainer).on("mouseenter mouseleave", itemSelector, function() {
            $(this).find(".album-background").toggleClass("show-background");
        });
    }
    else if (typeOfImg === "photos") { //if displayed items are photos
        $(itemsContainer).on("mouseenter mouseleave", itemSelector, function () {
            $(this).find(".img-description").toggleClass("show-description"); //shows description

        });
    }
}
// avoid globals
(function ($) {

    $(document).ready(function () {
        
        //get references content wrapper
        var refferencesWrapper = $(".references-wrapper");

        //if scroll size not big enough
        if (refferencesWrapper[0].scrollWidth == refferencesWrapper[0].offsetWidth) {
            $("div[class$='-anchor-container'").hide();
        } else {
            //currently scrolling flag
            var scrolling = false;



            //move view function
            //move forward argument is bool that indicates +x instead of -x
            function moveReferencesView(moveForward) {

                if (scrolling === false) {

                    //set indicator scrolling is happening
                    scrolling = true;

                    //check if view range is bigger than scroll range
                    if (refferencesWrapper[0].scrollWidth === refferencesWrapper[0].offsetWidth) {
                        return;
                    }

                    //move right
                    if (moveForward != null && moveForward === true) {
                        //animate
                        $(refferencesWrapper[0]).animate({
                            scrollLeft: "+=800"
                        }, 500, function () {
                            scrolling = false;
                        });

                    }
                        //move left
                    else if (moveForward != null && moveForward === false) {

                        $(refferencesWrapper[0]).animate({
                            scrollLeft: "-=800"
                        }, 500, function () {
                            scrolling = false;
                        });
                    }
                }




            }

            //bind left and right arrows
            //left arrow
            $(".references-content").on("click", "#left-arrow", function () {
                moveReferencesView(false);
            });

            //right arrow
            $(".references-content").on("click", "#right-arrow", function () {
                moveReferencesView(true);
            });

            
        }
    });
    
   

})(window.jQuery);
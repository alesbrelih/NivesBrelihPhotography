
//function that shows enlarged photo
function EnlargePicture(photoLink, photoTitle, photosContainer) {

    //check if containing div is added to DOM yet
    if ($("#photo-enlarge").length == 0) {

        //div isn't added yet

        //div markup
        var $html = "<div id='photo-enlarge'>" +
            "<label id='close-photo-enlarge' class='glyphicon glyphicon-remove'></label>" +
            "<div id='photo'>" +
            "<img src='" + photoLink + "'/>";

        //if there is photo title add it
        if (photoTitle.length > 0) {
            $html += "<span id='photo-description'>" +
                photoTitle +
                "</span>";
        }
        //close code
        $html += "</div>" +
         "</div>";

        //append div to #photo-container
        $(photosContainer).append($html);


    } else {
        //change attributes of previous displayed picture
        $("#photo>img").attr("src", photoLink);  //picture link

        if (photoTitle.length > 0) { //photo title exists
            if ($("#photo-description").length == 0) { //if previous pictures didnt have title

                //add title markup
                var $photoTitleMarkUp = "<span id='photo-description'>" +
                photoTitle +
                "</span>";

                //append title to photo
                $("#photo").append($photoTitleMarkUp);
            }
            $("#photo-description").text(photoTitle);  //picture description
        }
        else { //there is no photo title

            if ($("#photo-description").length > 0) {  //title markup exists from previous enlargements
                $("#photo").remove("#photo-description");   //remove markup
            }
        }
    }

    //remove scrolls on body when picture enlarged
    $("body").addClass("overflow-hidden");


    //fade in picture
    $("#photo-enlarge").fadeIn();
}

//function that enables click to enlarge photos
//photoContainer - outside container of photos
//photoSelector - selector for photos
function EnablePhotoZoom(photoContainer, photoSelector) {

    //click on image triggers event to open enlarged image
    $(photoContainer).on("click", photoSelector, function () {


        //photo link of clicked photo
        var $photoLink = $(this).find("img").attr("data-href");
        if (!$photoLink || $photoLink.length == 0) {  //photoselector is photo itself
            $photoLink = $(this).attr("data-href");
        }

        //photo description of clicked photo
        var $photoDescription = "";

        var $photoDescriptionContainer = $(this).find("span");

        //find if there is photo title
        if ($photoDescriptionContainer.length > 0) {
            $photoDescription = $photoDescriptionContainer.text();
        }


        //call function to enlarge photo with text and link of image
        EnlargePicture($photoLink, $photoDescription, photoContainer);

    });

    //Delegate function so it works on dynamically added elements
    $(photoContainer).on("click", "#close-photo-enlarge", function () {

        //add scrollbars to body
        $("body").removeClass("overflow-hidden");

        //fades the photo enlarge out
        $("#photo-enlarge").fadeOut();

    });
    //Delegate function so it works on dynamically added elements
    $(photoContainer).on("click", "#photo-enlarge", function () {

        //add scrollbars to body
        $("body").removeClass("overflow-hidden");

        //fades the photo enlarge out
        $("#photo-enlarge").fadeOut();

    });
    $(photoContainer).on("click", "img", function (ev) {

        ev.preventDefault();
        console.log("img clicked");
        ev.stopPropagation();

    });

    ////////////////////////////
    //Photo Enlarge DIV - SHOW Trigger - END



}
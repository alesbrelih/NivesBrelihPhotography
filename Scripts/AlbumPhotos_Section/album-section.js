

$(function () {
    var pageNm = 0;  //starting page number
    var scrollEnd = false;


    /*Masonry function*/
    function SetMasonry() {
        $($("#content-container").imagesLoaded(function () {
            $('.masonry-layout').masonry({
                // options
                itemSelector: '.masonry-image',
                columnWidth: '.grid-sizer'
                //percentPosition: true
            });
        }));



        /*set on hover depending on which element is displayed - album or photos*/
        if ($(".masonry-layout").attr("data-photo-type") === "albums") {

            //if displayed items are album covers
            $(".masonry-layout").on("mouseenter mouseleave", ".masonry-image", function () {
                $(this).find(".album-background").toggleClass("show-background");
            });

        } else {
            //displayed items are photos, so we need to show description on hover
            $(".masonry-layout").on("mouseenter mouseleave", ".masonry-image", function () {
                $(this).find(".img-description").toggleClass("show-description");
            });
        }


    }


    //loads content
    function LoadContent() {

        //if we load content on albums page
        if ($(".masonry-layout").attr("data-photo-type") === "albums") {

            //get new albums on controller with next pageNumber
            $.get("/Photos/Albums", { pageNumber: pageNm + 1 }, function (data) {

                //check if it didnt return anything (end of photos/albums)
                if (jQuery.isEmptyObject(data)) {
                    scrollEnd = true;
                    return;
                }

                //html which will be added to dom
                var $html = "";



                $.each(data, function (index, item) {

                    //create proper html markings for each of the new albums
                    var $row = "<div class='masonry-image img-container col-xs-12 col-sm-6 col-md-4'>" +
                        "<img alt='" + item.AlbumName + "' src='" + item.AlbumPhotoUrl + "' class='img-responsive' data-album-id='" + item.PhotoAlbumId + "'/>" +
                        "<div class='album-background'>" +
                        "<span class='album-name'>" + item.AlbumName + "<span class='album-date'>" + item.AlbumDateMonthAsString + "</span></span>" +
                        "</div>" +
                        "</div>";

                    //append it to whole html which will be added
                    $html += $row;

                });


                //make jquery object so masonry can use appended method
                var jqueryHtml = $($html);

                //append to photos and run masonry
                var photoContainer = $("#photos");
                photoContainer.append(jqueryHtml);
                photoContainer.imagesLoaded(function() {
                    photoContainer.masonry("appended", jqueryHtml);
                });

                //increase page number
                pageNm++;

            });


        } else { //else return photos

            //albumId
            var $albumId = $(".selected-album-description").attr("data-album-id");


            //route to controller that returns json
            $.get("/Photos/AlbumPhotos", { albumId: $albumId, pageNumber: pageNm + 1 }, function (data) {
                //checking if returned json is null and stopping function
                if (jQuery.isEmptyObject(data)) {

                    //we reached all photos in that category
                    scrollEnd = true;
                    return;


                }
                //html to be inserted
                var html = "";
                $.each(data, function (i, item) {
                    var row = '<div class="masonry-image img-container col-xs-12 col-sm-6 col-md-4">' +
                            '<img src="' + item.PhotoUrl + '" class="img-responsive"/>' +
                            '<div class="img-description"><span>' + item.PhotoTitle + '</span></div>' +
                            '</div>';
                    html += row;
                });
                //changing html to json so masonry appended can be called on
                var jqueryHtml = jQuery(html);
                //addin masonry to new photos
                var photoContainer = $("#photos");
                photoContainer.append(jqueryHtml);
                photoContainer.imagesLoaded(function() {
                    photoContainer.masonry('appended', jqueryHtml);
                });
                pageNm++;
            });
        }


    }

    SetMasonry();
    

    $("#content-container").on("click", ".masonry-image", function () {

        //check what pictures are loaded albums/photos
        //if albums
        if ($(".masonry-layout").attr("data-photo-type") === "albums") {

            //toggle div to move back to albums
            $("#back-to").slideToggle("slow");

            var $albumId = $(this).find("img").attr("data-album-id");

            $("#content-container").hide();

            $("#content-container").empty();

            $.get("/Photos/AlbumPhotos", { albumId: $albumId }, function (data) {
                var $albumDesc = data.AlbumDescription; //easier access to album description data


                var $html =     "<div class='selected-album-description' data-album-id="+$albumId+">" +
                                "<h3> "+$albumDesc.AlbumName+" </h3>" +
                                "<div class='selected-album-date'>" +
                                $albumDesc.AlbumDate+
                                "</div>" +
                                "<div class='row'>"+
                                "<div class='col-md-8 selected-album-text'>" +
                                $albumDesc.AlbumText+
                                "<hr>"+
                                "</div>" +
                                "</div>"+
                                "</div>";

                $html +="<div id='photos' class='masonry-layout' data-photo-type=photos>"+
                    "<div class='grid-sizer col-xs-12 col-sm-6 col-md-4'></div>";

                //clear masonry layout to insert album photos inside
                

                $.each(data.AlbumPhotos, function(index, item) {

                    var $row = "<div class='masonry-image img-container col-xs-12 col-sm-6 col-md-4'>" +
                        "<img src='" + item.PhotoUrl + "' class='img-responsive'/>" +
                        "<div class='img-description'> <span>" + item.PhotoTitle + "</span>" +
                        "</div>"+
                        "</div>";
                    $html += $row;
                });
                $html += "</div>";
                var jqueryHtml = $($html);
                var container = $("#content-container").append(jqueryHtml);
                container.imagesLoaded(function() {
                    SetMasonry();
                    $("#content-container").fadeIn("slow");

                    //reset load pictures because we are on new album photos
                    pageNm = 0;
                    scrollEnd = false;
                });


            });
        } else {//if album photos zoom picture
         
            //photo link of clicked photo
            var $photoLink = $(this).find("img").attr("src");

            //photo description of clicked photo
            var $photoDescription = $(this).find("span").text();

            //call function to enlarge photo with text and link of image
            EnlargePicture($photoLink, $photoDescription,"#content-container");

        }

        
    });

    //click back to albums
    $("#back-to").on("click", function () {

        //hide current container so user doesnt see masonry in action
        $("#back-to").slideUp("slow");
        $("#content-container").hide();
        //$("#content-container, #back-to").fadeOut("slow");

        //get partial view
        $.get("/Photos/Albums",{pageNumber:0}, function (data) {

            //insert partial view into #content-container
            var container = $("#content-container").html(data);

            //wait for pics to load
            container.imagesLoaded(function() {
                //set masonry on images
                SetMasonry();

                //fade in content container with masonry images
                $("#content-container").fadeIn("slow");

                //slide up back to albums on this page
                //$("#back-to").slideToggle();

                //reset pagenum and photos end because we are back on albums section
                pageNm = 0;
                scrollEnd = false;
            });

        });
    });



    //on scroll
    $(window).scroll(function () {

        if (!scrollEnd) {
            if ($(window).scrollTop() + $(window).height() == $(document).height()) {  //check for new albums/photos once we reach bottom

                LoadContent();

            }
        }
    });
});
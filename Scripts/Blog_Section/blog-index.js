/*REMOVE GLOBAL SCOPING*/
(function($) {
    
    var pageControl = {
        pageNum: 0,  //page Number
        scrollStop: false  //end scroll bool
    };


    //FUNCTIONS SECTION

    //loads more blogs when user scrolls
    function loadMoreBlogs() {

        var categoryId = $("#blogs-container").attr("data-blogs-category"); //get current category id

        //get ajax call
        $.get("/Blog/Index", { categoryId: categoryId, pageNumber: pageControl.pageNum + 1 }, function (data) {

            if (!data || data.trim().length === 0) { //check if returned string is empty/null

                //end scroll
                pageControl.scrollStop = true;
                return;


            }

            $("#blogs-container").append(data); //append data

            pageControl.pageNum++; //increase page

        });

    }

    //function that loads blogs depending on category selected
    //action - examp. click/hover/..
    //selector - selector for the category change examp. <a>, <dl>
    //containerOfElements - container in which new blogs will be loaded
    //pageCon - object that involves pageNumber and scrollEnd
    function loadCategoryOfBlogsOnClickAjax(action, selector, attribute, containerOfElements, pageCon) {

        $(document).on(action, selector, function (ev) {

            ev.preventDefault(); //prevents default action if JS enabled

            var $link = $(this).attr(attribute);  //link contained in attribute

            $.get($link, function (data) {  //ajax get

                $(containerOfElements).replaceWith(data);  //replace the container with new partial view

                pageCon.pageNum = 0;  //if needed set pageNumber to 0
                pageCon.scrollStop = false;  //if needed set scroll end to false

            });

        });


    }

    $(function () {  //on document ready

        //load blogs based on category when clicking on categories inside blog posts
        loadCategoryOfBlogsOnClickAjax("click", ".blog-category-nav a", "href", "#blogs-container", pageControl);

        //load blogs based on category when selecting it from dropdown menu

        loadCategoryOfBlogsOnClickAjax("click", ".blog-category-selector a", "href", "#blogs-container", pageControl);



        $(window).scroll(function () {
            if (!pageControl.scrollStop) {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {

                    loadMoreBlogs();

                }
            }
        });
    });


})(window.jQuery);


// --- CMS SERVICE MODUEL --- //
(function(angular) {
    
    //services app module
    var services = angular.module("adminApp.services");

    //service
    function cmsServiceFactory() {

        //returned factory
        var cmsFactory = {};

        // --- PRIVATES --- //
        var props = null;

        // private function that inserts substring to a string
        function insert(element){

            //private helper functions to insert elements (start mark and end mark)
            function insertElement(open,close){

                //if no start or end then append element at end
                if(props.selected.start == 0 && props.selected.end==0){
                    props.content = props.content + "\n"+open+close+"\n";
                }
                //if start and end are same then put it at that location
                else if(props.selected.start == props.selected.end){
                    props.content = props.content.substring(0,props.selected.end)+"\n"+open+close+"\n"+props.content.substring(props.selected.end,props.content.length);
                }
                else{
                    //if different then put it  around selection
                    props.content = props.content.substring(0,props.selected.end)+"\n"+close+"\n"+props.content.substring(props.selected.end,props.content.length);
                    props.content = props.content.substring(0,props.selected.start)+"\n"+open+"\n"+props.content.substring(props.selected.start,props.content.length);

                }

                //tracking changes for selected text directive so it selects appended element
                props.lastElement=open+close;
                props.elementsAdded++;

            }

            //inserts element with no closing tag
            function insertSingle(el) {

                //if start and end are 0 then append at end
                if(props.selected.start == 0 && props.selected.end==0){
                    props.content = props.content + "\n"+el+ "\n";
                }
                else{
                    //else put it at start location of selection
                    props.content = props.content.substring(0,props.selected.start)+"\n"+el+"\n"+props.content.substring(props.selected.start,props.content.length);

                }

                //tracking changes for selected text directive so it selects appended element
                props.lastElement = el;
                props.elementsAdded++;

                //TODO: INSERT TOASTR TO SAY THAT IT WILL BE INSERTED AT START
            }

            //inserts container
            function insertContainer(type){

                switch (type){
                    case "row":
                        insertElement("<div class='row'>","</div>");
                        break;
                    case "half-column":
                        insertElement("<div class='col-md-6'>","</div>");
                        break;
                    case "third-column":
                        insertElement("<div class='col-md-4'>","</div>");
                        break;
                }
            }

            //switch function to insert element depending on stuff
            switch(element){
                case "line":
                    insertSingle("<br>");
                    break;
                case "paragraph":
                    insertElement("<p>","</p>");
                    break;
                case "strong":
                    insertElement("<strong>","</strong>");
                    break;
                case "italic":
                    insertElement("<i>","</i>");
                    break;
                case "horizontal-line":
                    insertSingle("<hr>");
                    break;
                case "heading-1":
                    insertElement("<h1>","</h1>");
                    break;
                case "heading-2":
                    insertElement("<h2>","</h2>");
                    break;
                case "heading-3":
                    insertElement("<h3>","</h3>");
                    break;
                case "row":
                    insertContainer("row");
                    break;
                case "half-column":
                    insertContainer("half-column");
                    break;
                case "third-column":
                    insertContainer("third-column");
                    break;

            }


        }

        //inserts photo
        function insertPhoto(imgUrl){
            if(imgUrl){ //url needs to exist

                var photo = "<img class='img-responsive' src='"+imgUrl+"'></img>"; //photo markup
                
                //if start and end == 0 then append at end
                if(props.selected.start == 0 && props.selected.end==0){
                    props.content = props.content +"\n"+ photo+"\n";
                }
                else{

                //else put at start of selection
                    props.content = props.content.substring(0,props.selected.start)+"\n"+photo+"\n"+props.content.substring(props.selected.start,props.content.length);

                }

                //track changes
                props.lastElement = photo;
                props.elementsAdded++;
            }
        }



        // ---- PUBLICS ---- //

        //set content variable
        cmsFactory.SetContent = function (contentVar){
            props = contentVar;
        };

        //gets cmstool service props (needed in abselectedtext directive)
        cmsFactory.GetProps = function () {
            return props;
        };

        //returns content with no linebreaks
        cmsFactory.GetContent = function () {

            //return content with no linebreaks;

            var noLineBreaks = props.content;
            while (noLineBreaks.indexOf("\n") != -1) {
                //remove linebreak
                noLineBreaks = noLineBreaks.replace("\n", "");

            }

            //return no linebreaks content
            return noLineBreaks;
        };



        // --- TOOLS --- //

        // object for returned tools
        cmsFactory.Tools = {};

        // insert element
        cmsFactory.Tools.Insert = function (element){
            if(element){ //element type exists
                insert(element);
            }
        };

        //inserts photo
        cmsFactory.Tools.InsertPhoto = function (imgUrl){
            if(imgUrl){//img src exists
                insertPhoto(imgUrl);
            }

        };


        return cmsFactory;
    }

    //register service
    services.factory("CmsService", cmsServiceFactory);

})(window.angular);
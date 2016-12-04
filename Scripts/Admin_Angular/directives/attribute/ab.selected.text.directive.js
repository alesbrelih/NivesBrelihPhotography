// ---- SELECTED TEXT DIRECTIVE ON TEXTAREA --- //
(function(angular) {
    
    //main app ref
    var app = angular.module("adminApp");

    function selectedTextDirectiveController() {
        
        // link function
        function linkFnc(scope, el) {
            
            //gets CmsService prop reference for future work with them
            scope.CmsService = CmsService.GetProps();

            //init set selected text values
            scope.abSelectedText = {
                selection: "",
                start: 0,
                end: 0
            };

            //on mouseup set selection
            el.on("mouseup", function (e) {
                //left mouse button
                if (e.which === 1) {

                    //using math.max because i want start always the smaller index

                    //selection start index
                    var selectionStart = Math.min(el[0].selectionStart, el[0].selectionEnd);
                    //selection end index
                    var selectionEnd = Math.max(el[0].selectionEnd, el[0].selectionStart);

                    //selected text
                    var selection = el[0].value.substring(selectionStart, selectionEnd);

                    //set scope var
                    scope.abSelectedText.selection = selection;
                    scope.abSelectedText.start = selectionStart;
                    scope.abSelectedText.end = selectionEnd;
                }

            });

            //if user types then selection follows
            el.on("keyup", function () {

                scope.abSelectedText.start = el[0].selectionStart;
                scope.abSelectedText.end = el[0].selectionEnd;
                scope.abSelectedText.selection = "";
            });

            //watch when new element was appended to text area and change selection
            scope.$watch("CmsService.elementsAdded", function () {

                //focus textarea
                el[0].focus();

                //if nothing was selected
                //move cursor at end when addign element
                if (scope.CmsService.selected.start == 0 && scope.CmsService.selected.end == 0) {
                    el[0].selectionStart = el[0].value.length;
                    el[0].selectionEnd = el[0].value.length;
                }
                    //bigger selection
                else if (scope.CmsService.selected.start != scope.CmsService.selected.end) {

                    var newStart = scope.CmsService.selected.start;
                    // +3 because of new line signs
                    var newEnd = scope.CmsService.selected.end + scope.CmsService.lastElement.length + 3;

                    //text area selection
                    el[0].selectionStart = newStart;
                    el[0].selectionEnd = newEnd;

                    //Set cms service props
                    scope.CmsService.selected.start = newStart;
                    scope.CmsService.selected.end = newEnd;


                }
                else {
                    //start and end index are same

                    el[0].selectionStart = scope.CmsService.selected.start;
                    el[0].selectionEnd = scope.CmsService.selected.end;
                }

            });

        }

        //direcive definition
        return {
            restrict: "A",
            link: linkFnc,
            scope: {
                abSelectedText: "="
            }
        };
    }

    //register directive
    app.directive("abSelectedText", selectedTextDirectiveController);

})(window.angular);
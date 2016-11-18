// --------- STARTS WITH FILTER FOR NG-REPEAT (FOR PAGINATION) ---------------- //
// --------- RETURNS ARRAY FROM THE STARTING INDEX TO END --------------------- //

(function(angular) {
    
    //get main app/module
    var app = angular.module("adminApp");

    //filter controller
    function startsWithFilter() {

        //gets input (array) and starting index
        return function(input, startingIndex) {

            console.log("slicing");
            console.log(input);
            //gets array and returns it sliced from starting index
            return input.slice(startingIndex);

        }
    }

    //register filter
    app.filter("startsWith", startsWithFilter);

})(window.angular);
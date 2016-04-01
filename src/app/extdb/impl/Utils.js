// Convert the database tuple object into an array of attribute pairs
// for TypeScript to handle it better.
"use strict";
var Utils = (function (){
    function Utils() {
    }
    Utils.prototype.rowTupleToArray = function(columnTuple) {

        var array = [];
        for (var el in columnTuple) {
            if (columnTuple.hasOwnProperty(el)) {
                array.push ({name: el, value: columnTuple[el]});
            }
        }
        return array;
    };
    return Utils;
}());
exports.Utils = Utils;

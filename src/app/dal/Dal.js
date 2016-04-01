///<reference path="./interface/IDal.ts"/>
var Dal;
(function (Dal_1) {
    var Dal = (function () {
        function Dal() {
        }
        Dal.prototype.createAccount = function (account) {
            return 0;
        };
        Dal.prototype.fetchAccount = function (key, callback) {
            return true;
        };
        Dal.prototype.modifyAccount = function (accountID, account) {
            return true;
        };
        Dal.prototype.deleteAccount = function (accountID) {
            return true;
        };
        return Dal;
    }());
    Dal_1.Dal = Dal;
})(Dal || (Dal = {}));

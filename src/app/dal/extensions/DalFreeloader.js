/**
 Created by jimmypesola on 2016-04-01.
*/
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
///<reference path="../interface/IFreeloader.ts"/>
///<reference path="../Dal.ts"/>
var Dal;
(function (Dal) {
    var DalFreeloader = (function (_super) {
        __extends(DalFreeloader, _super);
        function DalFreeloader() {
            _super.call(this);
        }
        DalFreeloader.prototype.createPlayer = function (player) {
            return 0;
        };
        DalFreeloader.prototype.fetchPlayer = function (playerID, callback) {
            return true;
        };
        DalFreeloader.prototype.updatePlayer = function (player) {
            return true;
        };
        DalFreeloader.prototype.removePlayer = function (playerID) {
            return true;
        };
        DalFreeloader.prototype.createVehicle = function (vehicle) {
            return 0;
        };
        DalFreeloader.prototype.fetchVehicle = function (vehicleID, callback) {
            return true;
        };
        DalFreeloader.prototype.updateVehicle = function (vehicle) {
            return true;
        };
        DalFreeloader.prototype.removeVehicle = function (vehicleID) {
            return true;
        };
        return DalFreeloader;
    }(Dal.Dal));
    Dal.DalFreeloader = DalFreeloader;
})(Dal || (Dal = {}));

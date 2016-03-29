/// <reference path="interface/ExtDb.ts"/>
/// <reference path="impl/Mysql.ts"/>
"use strict";
var Mysql_1 = require('./impl/Mysql');
(function (ExtDbType) {
    ExtDbType[ExtDbType["E_MYSQL"] = 0] = "E_MYSQL";
})(exports.ExtDbType || (exports.ExtDbType = {}));
var ExtDbType = exports.ExtDbType;
var ExtDbFactory = (function () {
    function ExtDbFactory() {
    }
    ExtDbFactory.prototype.create = function (extDbType, params) {
        var obj = undefined;
        switch (extDbType) {
            case ExtDbType.E_MYSQL:
                obj = new Mysql_1.Mysql(params.host, params.port, params.credentials, params.dbName);
                break;
            default:
                obj = new Mysql_1.Mysql(params.host, params.port, params.credentials, params.dbName);
        }
        return obj;
    };
    return ExtDbFactory;
}());
exports.ExtDbFactory = ExtDbFactory;

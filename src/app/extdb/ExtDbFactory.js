///<reference path="interface/IExtDb.ts"/>
///<reference path="impl/Mysql.ts"/>
///<reference path="impl/PostgreSql.ts"/>
"use strict";
var Mysql_1 = require('./impl/Mysql');
var PostgreSql_1 = require('./impl/PostgreSql');
(function (ExtDbType) {
    ExtDbType[ExtDbType["E_MYSQL"] = 0] = "E_MYSQL";
    ExtDbType[ExtDbType["E_POSTGRESQL"] = 1] = "E_POSTGRESQL";
})(exports.ExtDbType || (exports.ExtDbType = {}));
var ExtDbType = exports.ExtDbType;
var ExtDbFactory = (function () {
    function ExtDbFactory() {
    }
    ExtDbFactory.prototype.create = function (extDbType) {
        var obj = undefined;
        switch (extDbType) {
            case ExtDbType.E_MYSQL:
                obj = new Mysql_1.Mysql();
                break;
            case ExtDbType.E_POSTGRESQL:
                obj = new PostgreSql_1.PostgreSql();
                break;
            default:
                obj = new Mysql_1.Mysql();
        }
        return obj;
    };
    return ExtDbFactory;
}());
exports.ExtDbFactory = ExtDbFactory;

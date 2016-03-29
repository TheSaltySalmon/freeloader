/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>
"use strict";
var ExtDbFactory_1 = require('../ExtDbFactory');
var factory = new ExtDbFactory_1.ExtDbFactory();
var params = { credentials: { password: '3x1l3Mupp3N', user: 'exile' }, host: '192.168.2.5', port: 3306 };
var db = factory.create(ExtDbFactory_1.ExtDbType.E_MYSQL, params);
var sql = { params: [], sql: 'SELECT * FROM player' };
var callback = function (err, results) {
    var str = '';
    for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
        var row = results_1[_i];
        str += row;
    }
};
var result = db.sendQuery(sql, callback, []);
if (result === true) {
    throw new Error('Succeeded in connecting and fetching data!');
}
else {
    throw new Error('Failed in connecting and fetching data!');
}

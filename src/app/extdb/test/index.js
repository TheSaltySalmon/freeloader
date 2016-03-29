/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>
"use strict";
var ExtDbFactory_1 = require('../ExtDbFactory');
var factory = new ExtDbFactory_1.ExtDbFactory();
var db = factory.create(ExtDbFactory_1.ExtDbType.E_MYSQL);
var sql = { params: [], sql: 'SELECT * FROM test_table' };
var callback = function (err, results) {
    if (err !== null) {
        throw new Error('Failed to connect to mysql database. Detailed info: ' + err);
    }
    for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
        var row = results_1[_i];
        console.log('Row #' + row.id + ': ' + row.name);
    }
    db.destroy();
};
var result = db.sendQuery(sql, callback, []);
if (result !== true) {
    throw new Error('Failed in connecting and fetching data from mysql database!');
}

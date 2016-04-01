///<reference path="../../interface/IExtDb.ts"/>
///<reference path="../../ExtDbFactory.ts"/>
"use strict";
var ExtDbFactory_1 = require('../../ExtDbFactory');
var factory = new ExtDbFactory_1.ExtDbFactory();
var db = factory.create(ExtDbFactory_1.ExtDbType.E_MYSQL);
var sql = { params: [], sql: 'SELECT * FROM test_table' };
var callback = function (results) {
    if (results.result === undefined) {
        var str = 'Failed to fetch data from the database: code: ' + results.error.code + ', cause: ' + results.error.cause;
        throw new Error(str);
    }
    for (var _i = 0, _a = results.result; _i < _a.length; _i++) {
        var row = _a[_i];
        for (var _b = 0, _c = row.cells; _b < _c.length; _b++) {
            var cell = _c[_b];
            console.log('Cell [' + cell.name + ': ' + cell.value + ']');
        }
    }
    db.destroy();
};
var result = db.sendQuery(sql, callback, []);
if (result !== true) {
    throw new Error('Failed in connecting and fetching data from mysql database!');
}

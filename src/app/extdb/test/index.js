/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>
"use strict";
var ExtDbFactory_1 = require('../ExtDbFactory');
var factory = new ExtDbFactory_1.ExtDbFactory();
var params = {
    credentials: {
        password: 'putpasswordhere',
        user: 'root'
    },
    dbName: 'test',
    host: '127.0.0.1',
    port: 3306
};
var db = factory.create(ExtDbFactory_1.ExtDbType.E_MYSQL, params);
var sql = { params: [], sql: 'SELECT * FROM test_table' };
var callback = function (err, results) {
    if (err !== null) {
        throw new Error(err);
    }
    for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
        var row = results_1[_i];
        console.log(row);
    }
    db.destroy();
};
var result = db.sendQuery(sql, callback, []);
if (result === true) {
    console.log('Succeeded in connecting and fetching data!');
}
else {
    throw new Error('Failed in connecting and fetching data!');
}

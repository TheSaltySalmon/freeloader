/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>

import {ExtDbFactory, ExtDbType} from '../ExtDbFactory';

let factory = new ExtDbFactory();
let db = factory.create(ExtDbType.E_MYSQL);
let sql = { params: [], sql: 'SELECT * FROM test_table' };

let callback = (err, results) => {
    if (err !== null) {
        throw new Error('Failed to connect to mysql database. Detailed info: ' + err);
    }
    for (let row of results) {
        console.log('Row #' + row.id + ': ' + row.name);
    }
    db.destroy();
};

let result = db.sendQuery(sql, callback, []);

if (result !== true) {
    throw new Error('Failed in connecting and fetching data from mysql database!');
}


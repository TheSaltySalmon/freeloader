/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>

import {ExtDbFactory, ExtDbType} from '../ExtDbFactory';

let factory = new ExtDbFactory();
let params = {
    credentials: {
        password: 'putpasswordhere',
        user: 'root'
    },
    dbName: 'test',
    host: '127.0.0.1',
    port: 3306
};

let db = factory.create(ExtDbType.E_MYSQL, params);

let sql = { params: [], sql: 'SELECT * FROM test_table' };
let callback = (err, results) => {
    if (err !== null) {
        throw new Error(err);
    }
    for (let row of results) {
        console.log(row);
    }
    db.destroy();
};

let result = db.sendQuery(sql, callback, []);

if (result === true) {
    console.log('Succeeded in connecting and fetching data!');
} else {
    throw new Error('Failed in connecting and fetching data!');
}


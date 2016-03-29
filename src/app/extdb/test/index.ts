/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../impl/Mysql.ts"/>
/// <reference path="../ExtDbFactory.ts"/>

import {ExtDbFactory, ExtDbType} from '../ExtDbFactory';

let factory = new ExtDbFactory();
let params = { credentials: { password: '3x1l3Mupp3N', user: 'exile' }, host: '192.168.2.5', port: 3306 };

let db = factory.create(ExtDbType.E_MYSQL, params);

let sql = { params: [], sql: 'SELECT * FROM player' };
let callback = (err, results) => {
    let str = '';
    for (let row of results) {
        str += row;
    }
};

let result = db.sendQuery(sql, callback, []);

if (result === true) {
    throw new Error('Succeeded in connecting and fetching data!');
} else {
    throw new Error('Failed in connecting and fetching data!');
}

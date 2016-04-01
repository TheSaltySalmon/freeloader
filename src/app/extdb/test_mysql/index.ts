/// <reference path="../interface/IExtDb.ts"/>
/// <reference path="../ExtDbFactory.ts"/>

// as we can see here, there's no need to import anything related to MySQL.
// it is specified for the factory as an enum only, and POOF! we have 
// an ExtDb object that is a mysql database connection :)

import {IExtDb, IQuery, IResult} from '../interface/IExtDb';
import {ExtDbFactory, ExtDbType} from '../ExtDbFactory';

let factory: ExtDbFactory = new ExtDbFactory();
let db: IExtDb = factory.create(ExtDbType.E_MYSQL);
let sql: IQuery = { params: [], sql: 'SELECT * FROM test_table' };

let callback = (results: IResult) => {

    if (results.result === undefined) {
        let str = 'Failed to fetch data from the database: code: ' + results.error.code + ', cause: ' + results.error.cause;
        throw new Error(str);
    }

    for (let row of results.result) {
        for (let cell of row.cells) {
            console.log('Cell [' + cell.name + ': ' + cell.value + ']');
        }
    }
    db.destroy();
};

let result: boolean = db.sendQuery(sql, callback, []);

if (result !== true) {
    throw new Error('Failed in connecting and fetching data from mysql database!');
}


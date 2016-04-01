/// <reference path="../interface/IExtDb.ts"/>
/// <reference path="../DatabaseConfig.ts"/>
/// <reference path="../../../../typings/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>
/// <reference path="./Utils.d.ts"/>

import {IExtDb, IExtDbCallback, IQuery, IRow} from '../interface/IExtDb';
import {DatabaseConfig, IDatabaseConfigData} from '../DatabaseConfig';
import {Utils} from './Utils';
import * as mysql from 'node-mysql-wrapper';

/**
  Mysql adapter to connect and query a MySQL database
  */
export class Mysql implements IExtDb {

    private oDb: mysql.Database;
    private bDbIsReady: boolean;
    private bReturnRowAsObject: boolean;

    /**
      Create and open a MySQL adapter connection using
      the configuration file mysql.json
      */
    public constructor () {

        let mysqlCfg: DatabaseConfig = new DatabaseConfig('db.json');
        let cfg: IDatabaseConfigData = mysqlCfg.getConfig();

        let connStr: string = 'mysql://' + cfg.user + ':' +
            cfg.password + '@' + cfg.hostname +
            '/' + cfg.dbname + '?debug=false&charset=utf8';

        this.oDb = mysql.wrap(connStr);
        if (this.oDb === undefined) {
            throw new Error('Failed to connect to ' + connStr);
        }

        this.bReturnRowAsObject = cfg.returnRowAsObject;
        this.bDbIsReady = true;

        return this;
    }

    /**
      Destroy the MySQL adapter connection
      */
    public destroy() {
        this.oDb.destroy();
    }

    /**
      Queries the MySQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder). 
      @param {any} callback - The callback function to call when query response is received. 
      @param {any[]} params - The parameter list for the query parameter placeholders. 
     */
    public sendQuery (query: IQuery, callback: IExtDbCallback, params: any[]): boolean {

        if (this.isReady() === false) {
            return false;
        }

        let obj: Mysql = this;

        let mysqlCallback = (err: any, results: any) => {

            if (err !== null) {
                callback({error: {cause: err, code: 1}, result: undefined});
            }

            let conv = new Utils();

            // map the results
            let rows: IRow[] = [];
            if (obj.returnRowAsObject()) {

                for (let row of results) {

                    rows.push ({cells: undefined, tuple: row});
                }
            } else {

                for (let row of results) {  // here we can use TypeScript iterators...

                    let cells: any[] = [];
                    let rowArray = conv.rowTupleToArray(row);
                    for (let i = 0; i < rowArray.length; i++) { // but not here, Javascript arrays have no iterators.

                        let pair = rowArray[i];
                        cells.push(pair);
                    }
                    rows.push ({cells: cells, tuple: undefined});
                }
            }
            callback({error: undefined, result: rows});
        };

        this.oDb.query(query.sql, mysqlCallback, params);
        return true;
    }

    /**
      Returns true if the ExtDb adapter is configured to return
      rows as objects instead of an array of attribute value pairs
      {name: value}
     */
    private returnRowAsObject(): boolean {
        return this.bReturnRowAsObject;
    }

    /**
      Returns true when the MySQL connection is ready to accept queries.
     */
    private isReady(): boolean {
        return this.bDbIsReady;
    }

}

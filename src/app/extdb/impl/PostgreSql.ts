///<reference path="../interface/IExtDb.ts"/>
///<reference path="../DatabaseConfig.ts"/>
///<reference path="../../../../typings/pg/pg.d.ts"/>

import {IExtDb, IQuery, IRow} from '../interface/IExtDb';
import {DatabaseConfig} from '../DatabaseConfig';
import {Utils} from './Utils';
import * as pgsql from 'pg';

/**
  PostgreSQL adapter to connect and query a PostgreSQL database
  */
export class PostgreSql implements IExtDb {

    private oDb: pgsql.Client;
    private bReturnRowAsObject: boolean;

    /**
      Create and open a MySQL adapter connection using
      the configuration file mysql.json
      */
    public constructor () {

        let pgCfg = new DatabaseConfig('db.json');
        let cfg = pgCfg.getConfig();
        let connCfg: pgsql.ConnectionConfig = {
            database: cfg.dbname,
            host: cfg.hostname,
            password: cfg.password,
            port: cfg.port,
            user: cfg.user
        };

        this.bReturnRowAsObject = cfg.returnRowAsObject;
        this.oDb = new pgsql.Client(connCfg);

        if (this.oDb === undefined) {
            throw new Error('Failed to create PostgreSQL client');
        }

        this.oDb.connect ((err: Error) => {
            throw err;
        });

        return this;
    }

    /**
      Destroy the MySQL adapter connection
      */
    public destroy() {
        this.oDb.end();
    }

    /**
      Queries the PostgreSQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder). 
      @param {any} callback - The callback function to call when query response is received. 
      @param {any[]} params - The parameter list for the query parameter placeholders. 
     */
    public sendQuery (query: IQuery, callback: any, params: any[]): boolean {

        let obj: PostgreSql = this;

        let pgCallback = (err: Error, result: pgsql.QueryResult) => {

            if (err !== undefined) {
                callback({error: {cause: err, code: 1}, result: undefined});
            }

            let conv = new Utils();

            // map results
            let rows: IRow[] = [];
            if (obj.returnRowAsObject()) {

                for (let row of result.rows) {

                    rows.push({cells: undefined, tuple: row});
                }
            } else {

                for (let row of result.rows) {

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

        this.oDb.query(query.sql, params, pgCallback);

        return true;
    }

    /**
      Returns true if the ExtDb adapter is configured to return
      rows as objects instead of an array of attribute value pairs
      {name: value}
     */
    private returnRowAsObject() {
        return this.bReturnRowAsObject;
    }

}

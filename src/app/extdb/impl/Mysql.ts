/// <reference path="../interface/IExtDb.ts"/>
/// <reference path="../DatabaseConfig.ts"/>
/// <reference path="../../../../typings/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>

import {IExtDb, IQuery} from '../interface/IExtDb';
import {DatabaseConfig} from '../DatabaseConfig';
import * as mysql from 'node-mysql-wrapper';

/**
  Mysql adapter to connect and query a MySQL database
  */
export class Mysql implements IExtDb {

    private db: mysql.Database;

    /**
      Create and open a MySQL adapter connection using
      the configuration file mysql.json
      */
    public constructor () {

        let mysqlCfg = new DatabaseConfig('db.json');
        let cfg = mysqlCfg.getConfig();

        let connStr = 'mysql://' + cfg.user + ':' +
            cfg.password + '@' + cfg.hostname +
            '/' + cfg.dbname + '?debug=false&charset=utf8';

        this.db = mysql.wrap(connStr);
        if (this.db === undefined) {
            throw new Error('Failed to connect to ' + connStr);
        }

        return this;
    }

    /**
      Destroy the MySQL adapter connection
      */
    public destroy() {
        this.db.destroy();
    }

    /**
      Queries the MySQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder). 
      @param {any} callback - The callback function to call when query response is received. 
      @param {any[]} params - The parameter list for the query parameter placeholders. 
     */
    public sendQuery (query: IQuery, callback: any, params: any[]): boolean {

        if (this.isReady() !== false) {
            return false;
        }

        this.db.query(query.sql, callback, params);

        return true;
    }

    /**
      Returns true when the MySQL connection is ready to accept queries.
     */
    private isReady(): boolean {
        return this.db.isReady;
    }

}

/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../../../definitely_typed/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>

import * as mysql from 'node-mysql-wrapper';
import {ExtDb, ICredentials, IQuery} from '../interface/ExtDb';

/**
  Mysql adapter to connect and query a MySQL database
  */
export class Mysql extends ExtDb {

    private db: NodeMysqlWrapper.Database;

    /**
      Create and open a MySQL adapter connection
      @param {string} host - The host name or IP address to connect to.
      @param {number} port - The port number to use when connecting.
      @param {ICredentials} credentials - The credentials data to use for authentication.
      */
    public constructor (host: string, port: number, credentials: ICredentials, db: string) {

        super(host, port, credentials, db);

        let connStr = 'mysql://' + this.credentials.user + ':' +
            this.credentials.password + '@' + this.host +
            '/' + this.dbName + '?debug=false&charset=utf8';

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

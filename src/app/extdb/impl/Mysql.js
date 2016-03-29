/// <reference path="../interface/ExtDb.ts"/>
/// <reference path="../../../definitely_typed/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>
"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var mysql = require('node-mysql-wrapper');
var ExtDb_1 = require('../interface/ExtDb');
/**
  Mysql adapter to connect and query a MySQL database
  */
var Mysql = (function (_super) {
    __extends(Mysql, _super);
    /**
      Create and open a MySQL adapter connection
      @param {string} host - The host name or IP address to connect to.
      @param {number} port - The port number to use when connecting.
      @param {ICredentials} credentials - The credentials data to use for authentication.
      */
    function Mysql(host, port, credentials, db) {
        _super.call(this, host, port, credentials, db);
        var connStr = 'mysql://' + this.credentials.user + ':' +
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
    Mysql.prototype.destroy = function () {
        this.db.destroy();
    };
    /**
      Queries the MySQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder).
      @param {any} callback - The callback function to call when query response is received.
      @param {any[]} params - The parameter list for the query parameter placeholders.
     */
    Mysql.prototype.sendQuery = function (query, callback, params) {
        if (this.isReady() !== false) {
            return false;
        }
        this.db.query(query.sql, callback, params);
        return true;
    };
    /**
      Returns true when the MySQL connection is ready to accept queries.
     */
    Mysql.prototype.isReady = function () {
        return this.db.isReady;
    };
    return Mysql;
}(ExtDb_1.ExtDb));
exports.Mysql = Mysql;

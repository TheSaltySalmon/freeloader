/// <reference path="../interface/IExtDb.ts"/>
/// <reference path="../DatabaseConfig.ts"/>
/// <reference path="../../../../typings/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>
"use strict";
var DatabaseConfig_1 = require('../DatabaseConfig');
var mysql = require('node-mysql-wrapper');
/**
  Mysql adapter to connect and query a MySQL database
  */
var Mysql = (function () {
    /**
      Create and open a MySQL adapter connection using
      the configuration file mysql.json
      */
    function Mysql() {
        var mysqlCfg = new DatabaseConfig_1.DatabaseConfig('db.json');
        var cfg = mysqlCfg.getConfig();
        var connStr = 'mysql://' + cfg.user + ':' +
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
}());
exports.Mysql = Mysql;

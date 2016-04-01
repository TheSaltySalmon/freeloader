///<reference path="../interface/IExtDb.ts"/>
///<reference path="../DatabaseConfig.ts"/>
///<reference path="../../../../typings/node-mysql-wrapper/node-mysql-wrapper.d.ts"/>
///<reference path="./Utils.d.ts"/>
"use strict";
var DatabaseConfig_1 = require('../DatabaseConfig');
var Utils_1 = require('./Utils');
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
    Mysql.prototype.destroy = function () {
        this.oDb.destroy();
    };
    /**
      Queries the MySQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder).
      @param {any} callback - The callback function to call when query response is received.
      @param {any[]} params - The parameter list for the query parameter placeholders.
     */
    Mysql.prototype.sendQuery = function (query, callback, params) {
        if (this.isReady() === false) {
            return false;
        }
        var obj = this;
        var mysqlCallback = function (err, results) {
            if (err !== null) {
                callback({ error: { cause: err, code: 1 }, result: undefined });
            }
            var conv = new Utils_1.Utils();
            // map the results
            var rows = [];
            if (obj.returnRowAsObject()) {
                for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
                    var row = results_1[_i];
                    rows.push({ cells: undefined, tuple: row });
                }
            }
            else {
                for (var _a = 0, results_2 = results; _a < results_2.length; _a++) {
                    var row = results_2[_a];
                    var cells = [];
                    var rowArray = conv.rowTupleToArray(row);
                    for (var i = 0; i < rowArray.length; i++) {
                        var pair = rowArray[i];
                        cells.push(pair);
                    }
                    rows.push({ cells: cells, tuple: undefined });
                }
            }
            callback({ error: undefined, result: rows });
        };
        this.oDb.query(query.sql, mysqlCallback, params);
        return true;
    };
    /**
      Returns true if the ExtDb adapter is configured to return
      rows as objects instead of an array of attribute value pairs
      {name: value}
     */
    Mysql.prototype.returnRowAsObject = function () {
        return this.bReturnRowAsObject;
    };
    /**
      Returns true when the MySQL connection is ready to accept queries.
     */
    Mysql.prototype.isReady = function () {
        return this.bDbIsReady;
    };
    return Mysql;
}());
exports.Mysql = Mysql;

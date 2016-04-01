/// <reference path="../interface/IExtDb.ts"/>
/// <reference path="../DatabaseConfig.ts"/>
/// <reference path="../../../../typings/pg/pg.d.ts"/>
"use strict";
var DatabaseConfig_1 = require('../DatabaseConfig');
var Utils_1 = require('./Utils');
var pgsql = require('pg');
/**
  PostgreSQL adapter to connect and query a PostgreSQL database
  */
var PostgreSql = (function () {
    /**
      Create and open a MySQL adapter connection using
      the configuration file mysql.json
      */
    function PostgreSql() {
        var pgCfg = new DatabaseConfig_1.DatabaseConfig('db.json');
        var cfg = pgCfg.getConfig();
        var connCfg = {
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
        this.oDb.connect(function (err) {
            throw err;
        });
        return this;
    }
    /**
      Destroy the MySQL adapter connection
      */
    PostgreSql.prototype.destroy = function () {
        this.oDb.end();
    };
    /**
      Queries the PostgreSQL database using the specified query and parameters.
      @param {IQuery} query - The SQL query to send (? is used as a parameter placeholder).
      @param {any} callback - The callback function to call when query response is received.
      @param {any[]} params - The parameter list for the query parameter placeholders.
     */
    PostgreSql.prototype.sendQuery = function (query, callback, params) {
        var obj = this;
        var pgCallback = function (err, result) {
            if (err !== undefined) {
                callback({ error: { cause: err, code: 1 }, result: undefined });
            }
            var conv = new Utils_1.Utils();
            // map results
            var rows = [];
            if (obj.returnRowAsObject()) {
                for (var _i = 0, _a = result.rows; _i < _a.length; _i++) {
                    var row = _a[_i];
                    rows.push({ cells: undefined, tuple: row });
                }
            }
            else {
                for (var _b = 0, _c = result.rows; _b < _c.length; _b++) {
                    var row = _c[_b];
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
        this.oDb.query(query.sql, params, pgCallback);
        return true;
    };
    /**
      Returns true if the ExtDb adapter is configured to return
      rows as objects instead of an array of attribute value pairs
      {name: value}
     */
    PostgreSql.prototype.returnRowAsObject = function () {
        return this.bReturnRowAsObject;
    };
    return PostgreSql;
}());
exports.PostgreSql = PostgreSql;

/// <reference path="../../../../typings/nconf/nconf.d.ts"/>
"use strict";
var nconf = require('nconf');
var MysqlCfg = (function () {
    function MysqlCfg(filename) {
        var data = undefined;
        var config = undefined;
        try {
            data = nconf.file(filename);
        }
        catch (exception) {
            throw new Error('Unable to open config file ' + filename + ', ' + exception);
        }
        config = data.get();
        if (config === null) {
            throw new Error('Unable to read config file ' + filename);
        }
        this.data = {
            'dbname': config.dbname,
            'hostname': config.hostname,
            'port': config.port,
            'user': config.user,
            'password': config.password
        };
        return this;
    }
    MysqlCfg.prototype.getCfg = function () {
        return this.data;
    };
    return MysqlCfg;
}());
exports.MysqlCfg = MysqlCfg;

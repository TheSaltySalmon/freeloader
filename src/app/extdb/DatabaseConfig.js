///<reference path="../../../typings/nconf/nconf.d.ts"/>
///<reference path="../../../typings/cryptojs/cryptojs.d.ts"/>
///<reference path="../../../typings/node/node.d.ts"/>
"use strict";
var nconf = require('nconf');
var crypto = require('crypto-js');
var fs = require('fs');
var DatabaseConfig = (function () {
    function DatabaseConfig(filename) {
        var data = undefined;
        var config = undefined;
        var res;
        try {
            res = fs.openSync(filename, 'r');
        }
        catch (exception) {
            throw new Error('Fatal error: Database configuration file ' + filename + ' was not found');
        }
        fs.close(res);
        try {
            data = nconf.file(filename);
        }
        catch (exception) {
            throw new Error('Unable to open database configuration file ' + filename + ', ' + exception);
        }
        config = data.get();
        if (config === null) {
            throw new Error('Unable to read database configuration file ' + filename);
        }
        var missingParams = [];
        if (config.dbname === undefined) {
            missingParams.push('dbname');
        }
        if (config.hostname === undefined) {
            missingParams.push('hostname');
        }
        if (config.port === undefined) {
            missingParams.push('port');
        }
        if (config.user === undefined) {
            missingParams.push('user');
        }
        if (config.password === undefined) {
            missingParams.push('password');
        }
        if (missingParams.length > 0) {
            throw new Error(filename + ': Missing mandatory parameter "' + missingParams.join(','));
        }
        if (config.returnRowAsObject === undefined) {
            config.returnRowAsObject = false;
        }
        var bin2text = crypto.enc.Latin1.stringify;
        var hex2bin = crypto.enc.Hex.parse;
        var decrypt = crypto.RC4.decrypt;
        var getCipherText = function (text) {
            return crypto.lib.CipherParams.create({ ciphertext: crypto.enc.Base64.parse(text) });
        };
        var cipherMode = { mode: crypto.mode.ECB, padding: crypto.pad.ZeroPadding };
        this.data = {
            'dbname': config.dbname,
            'hostname': config.hostname,
            'port': config.port,
            'user': config.user,
            'password': bin2text(decrypt(getCipherText(config.password), hex2bin('a8cb61274a9786bbfe797a68b91affef'), cipherMode)),
            'returnRowAsObject': config.returnRowAsObject
        };
        return this;
    }
    DatabaseConfig.prototype.getConfig = function () {
        return this.data;
    };
    return DatabaseConfig;
}());
exports.DatabaseConfig = DatabaseConfig;

/// <reference path="../../../typings/nconf/nconf.d.ts"/>
/// <reference path="../../../typings/cryptojs/cryptojs.d.ts"/>
"use strict";
var nconf = require('nconf');
var crypto = require('crypto-js');
var DatabaseConfig = (function () {
    function DatabaseConfig(filename) {
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
            'password': bin2text(decrypt(getCipherText(config.password), hex2bin('a8cb61274a9786bbfe797a68b91affef'), cipherMode))
        };
        return this;
    }
    DatabaseConfig.prototype.getConfig = function () {
        return this.data;
    };
    return DatabaseConfig;
}());
exports.DatabaseConfig = DatabaseConfig;

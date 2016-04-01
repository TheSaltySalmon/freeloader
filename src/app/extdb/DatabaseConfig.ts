///<reference path="../../../typings/nconf/nconf.d.ts"/>
///<reference path="../../../typings/cryptojs/cryptojs.d.ts"/>
///<reference path="../../../typings/node/node.d.ts"/>

import * as nconf from 'nconf';
import * as crypto from 'crypto-js';
import * as fs from 'fs';

export interface IDatabaseConfigData {

    dbname: string;
    hostname: string;
    port: number;
    user: string;
    password: string;
    returnRowAsObject: boolean;
}

export class DatabaseConfig {

    private data: IDatabaseConfigData;

    public constructor (filename: string) {

        let data: any = undefined;
        let config: any = undefined;

        let res: number;
        try {
            res = fs.openSync(filename, 'r');
        } catch(exception) {
            throw new Error('Fatal error: Database configuration file ' + filename + ' was not found');
        }
        fs.close(res);

        try {
            data = nconf.file (filename);
        } catch (exception) {
            throw new Error('Unable to open database configuration file ' + filename + ', ' + exception);
        }

        config = data.get();

        if (config === null) {
            throw new Error ('Unable to read database configuration file ' + filename);
        }

        let missingParams: string[] = [];
        if (config.dbname === undefined) {
            missingParams.push ('dbname');
        }
        if (config.hostname === undefined) {
            missingParams.push ('hostname');
        }
        if (config.port === undefined) {
            missingParams.push ('port');
        }
        if (config.user === undefined) {
            missingParams.push ('user');
        }
        if (config.password === undefined) {
            missingParams.push ('password');
        }
        if (missingParams.length > 0) {
            throw new Error (filename + ': Missing mandatory parameter "' + missingParams.join(','));
        }
        if (config.returnRowAsObject === undefined) {
            config.returnRowAsObject = false;
        }

        let bin2text = crypto.enc.Latin1.stringify;
        let hex2bin = crypto.enc.Hex.parse;
        let decrypt = crypto.RC4.decrypt;
        let getCipherText = (text) => {
            return crypto.lib.CipherParams.create ({ciphertext: crypto.enc.Base64.parse (text)});
        };
        let cipherMode = { mode: crypto.mode.ECB, padding: crypto.pad.ZeroPadding };

        this.data = {
            'dbname': config.dbname,
            'hostname': config.hostname,
            'port': config.port,
            'user': config.user,
            'password': bin2text (decrypt (getCipherText (config.password), hex2bin ('a8cb61274a9786bbfe797a68b91affef'), cipherMode)),
            'returnRowAsObject': config.returnRowAsObject
        };

        return this;
    }

    public getConfig(): IDatabaseConfigData {

        return this.data;
    }
}

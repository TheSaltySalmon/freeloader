/// <reference path="../../../typings/nconf/nconf.d.ts"/>
/// <reference path="../../../typings/cryptojs/cryptojs.d.ts"/>

import * as nconf from 'nconf';
import * as crypto from 'crypto-js';

interface IDatabaseConfigData {

    dbname: string;
    hostname: string;
    port: number;
    user: string;
    password: string;
}

export class DatabaseConfig {

    private data: IDatabaseConfigData;

    public constructor(filename: string) {

        let data: any = undefined;
        let config: any = undefined;

        try {
            data = nconf.file(filename);
        } catch (exception) {
            throw new Error('Unable to open config file ' + filename + ', ' + exception);
        }

        config = data.get();

        if (config === null) {
            throw new Error('Unable to read config file ' + filename);
        }


        let bin2text = crypto.enc.Latin1.stringify;
        let hex2bin = crypto.enc.Hex.parse;
        let decrypt = crypto.RC4.decrypt;
        let getCipherText = (text) => {
            return crypto.lib.CipherParams.create({ciphertext: crypto.enc.Base64.parse(text)});
        };
        let cipherMode = { mode: crypto.mode.ECB, padding: crypto.pad.ZeroPadding };

        this.data = {
            'dbname': config.dbname,
            'hostname': config.hostname,
            'port': config.port,
            'user': config.user,
            'password': bin2text(decrypt(getCipherText(config.password), hex2bin('a8cb61274a9786bbfe797a68b91affef'), cipherMode))
        };

        return this;
    }

    public getConfig(): IDatabaseConfigData {

        return this.data;
    }
}

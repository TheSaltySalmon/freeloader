/// <reference path="../../../../typings/nconf/nconf.d.ts"/>

import * as nconf from 'nconf';

interface IMysqlCfgData {

    dbname: string;
    hostname: string;
    port: number;
    user: string;
    password: string;
}

export class MysqlCfg {

    private data: IMysqlCfgData;

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

        this.data = {
            'dbname': config.dbname,
            'hostname': config.hostname,
            'port': config.port,
            'user': config.user,
            'password': config.password
        };

        return this;
    }

    public getCfg(): IMysqlCfgData {

        return this.data;
    }
}

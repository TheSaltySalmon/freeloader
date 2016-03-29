/// <reference path="interface/ExtDb.ts"/>
/// <reference path="impl/Mysql.ts"/>

import {ExtDb, ICredentials} from './interface/ExtDb';
import {Mysql} from './impl/Mysql';

export enum ExtDbType {
    E_MYSQL
}

export interface IExtDbParams {
    host: string;
    port: number;
    credentials: ICredentials;
}

export class ExtDbFactory {

    public create (extDbType: ExtDbType, params: IExtDbParams): ExtDb {

        let obj = undefined;
        switch (extDbType) {

            case ExtDbType.E_MYSQL:
                obj = new Mysql(params.host, params.port, params.credentials);
                break;
            default:
                obj = new Mysql(params.host, params.port, params.credentials);
        }
        return obj;
    }
}

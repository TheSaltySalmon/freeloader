/// <reference path="interface/IExtDb.ts"/>
/// <reference path="impl/Mysql.ts"/>
/// <reference path="impl/PostgreSql.ts"/>

import {IExtDb} from './interface/IExtDb';
import {Mysql} from './impl/Mysql';
import {PostgreSql} from './impl/PostgreSql';

export enum ExtDbType {
    E_MYSQL,
    E_POSTGRESQL
}

export class ExtDbFactory {

    public create (extDbType: ExtDbType): IExtDb {

        let obj = undefined;
        switch (extDbType) {

            case ExtDbType.E_MYSQL:
                obj = new Mysql();
                break;
            case ExtDbType.E_POSTGRESQL:
                obj = new PostgreSql();
                break;
            default:
                obj = new Mysql();
        }
        return obj;
    }
}

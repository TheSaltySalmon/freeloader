/// <reference path="interface/IExtDb.ts"/>
/// <reference path="impl/Mysql.ts"/>

import {IExtDb} from './interface/IExtDb';
import {Mysql} from './impl/Mysql';

export enum ExtDbType {
    E_MYSQL
}

export class ExtDbFactory {

    public create (extDbType: ExtDbType): IExtDb {

        let obj = undefined;
        switch (extDbType) {

            case ExtDbType.E_MYSQL:
                obj = new Mysql();
                break;
            default:
                obj = new Mysql();
        }
        return obj;
    }
}

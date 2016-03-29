/**
 Created by jimmypesola on 2016-03-28.
*/

export interface IQuery {
    sql: string;
    params: any[];
}

export interface IExtDb {
    sendQuery (query: IQuery, callback: any, params: any[]): boolean;
    destroy ();
}

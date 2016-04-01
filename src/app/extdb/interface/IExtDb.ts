/**
 Created by jimmypesola on 2016-03-28.
*/

export interface IQuery {
    sql: string;
    params: any[];
}

export interface IExtDbError {
    code: number;
    cause: any;
}

export interface IPair {
    name: string;
    value: any;
}

export interface IRow {
    cells: IPair[];
    tuple: any;
}

export interface IResult {
    result: IRow[];
    error: IExtDbError;
}

export interface IExtDbCallback {
    (result: IResult): void;
}

export interface IExtDb {
    sendQuery (query: IQuery, callback: any, params: any[]): boolean;
    destroy ();
}

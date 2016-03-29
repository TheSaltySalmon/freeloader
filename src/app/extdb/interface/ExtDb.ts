/**
 Created by jimmypesola on 2016-03-28.
*/

export interface ICredentials {
    user: string;
    password: string;
}

export interface IQuery {
    sql: string;
    params: any[];
}

export abstract class ExtDb {

    protected host: string;
    protected port: number;
    protected credentials: ICredentials;

    constructor (host: string, port: number, credentials: ICredentials) {
        this.host = host;
        this.port = port;
        this.credentials = {
            password: credentials.password,
            user: credentials.user
        };
        return this;
    }

    public abstract sendQuery (query: IQuery, callback: any, params: any[]): boolean;

    public abstract destroy();
}

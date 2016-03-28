/**
 * Created by jimmypesola on 2016-03-28.
 */

export interface ICredentials
{
    user: string;
    password: string;
}

export interface IQuery
{
    sql: string;
    params: Array<any>;
}

export interface IExtDb
{
    sendQuery (query: IQuery);
}

export abstract class ExtDb implements IExtDb
{
    host: string;
    port: number;
    credentials: ICredentials;

    new (host: string, port: number, credentials: ICredentials) : ExtDb
    {
        this.host = host;
        this.port = port;
        this.credentials.user = credentials.user;
        this.credentials.password = credentials.password;
        return this;
    }

    abstract sendQuery (query: IQuery);
}

import { ICredentials, IQuery, ExtDb } from '../interface/ExtDb';
import { wrap } from "node-mysql-wrapper";
class Mysql extends ExtDb
{
    mysql: any;

    new (host: string, port: number, credentials: ICredentials): Mysql
    {
        super.new(host, port, credentials);

        this.mysql = wrap(connection);
        
        return this;
    }

    sendQuery (query: IQuery)
    {
    }

}

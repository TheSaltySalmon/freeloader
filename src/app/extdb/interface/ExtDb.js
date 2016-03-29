/**
 Created by jimmypesola on 2016-03-28.
*/
"use strict";
var ExtDb = (function () {
    function ExtDb(host, port, credentials, dbName) {
        this.dbName = dbName;
        this.host = host;
        this.port = port;
        this.credentials = {
            password: credentials.password,
            user: credentials.user
        };
        return this;
    }
    return ExtDb;
}());
exports.ExtDb = ExtDb;

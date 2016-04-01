/**
 Created by jimmypesola on 2016-04-01.
*/

///<reference path="./IKey.ts"/>
namespace Dal {

    export interface IAccountCallback {
        (result: IAccount): void;
    }

    export interface IDal {

        createAccount (account: IAccount): number;
        fetchAccount (key: IKey<string>, callback: IAccountCallback): boolean;
        modifyAccount (accountID: IKey<number>, account: IAccount): boolean;
        deleteAccount (accountID: IKey<number>): boolean;
    }

    export type IAppName = string;

    export interface IPhoneNumber {
        pType: string;
        pNumber: string;
    }

    export interface IAuthToken {
        token: string;
        issuerName: string;
        issuerLink: string;
    }

    export interface IAccount {

        accountID?: number;
        typeName: string;
        firstName?: string;
        lastName?: string;
        age?: number;
        phoneNumber?: IPhoneNumber;
        emailAddress?: string;
        street?: string;
        zipCode?: number;
        city?: number;
        created: Date;
        lastUpdated: Date;
        access: IAppName[];
        authTokens: IAuthToken[];
    }
}

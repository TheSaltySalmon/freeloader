///<reference path="./interface/IDal.ts"/>

namespace Dal {

    export class Dal implements IDal {

        public constructor() {
        }

        public createAccount (account: IAccount): number {
            return 0;
        }

        public fetchAccount (key: IKey<string>, callback: IAccountCallback): boolean {
            return true;
        }

        public modifyAccount (accountID: IKey<number>, account: IAccount): boolean {
            return true;
        }

        public deleteAccount (accountID: IKey<number>): boolean {
            return true;
        }
    }
}

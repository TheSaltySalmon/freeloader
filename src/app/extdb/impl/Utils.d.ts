declare var Utils: Db.Utils;

declare module Db {
    interface IColumnPair {
        name: string;
        value: any;
    }

    interface IRow {
        tuple: IColumnPair[];
    }

    class Utils {
        public Utils(): void;
        public rowTupleToArray(columnTuple: any): IRow;
    }
}
export = Utils;

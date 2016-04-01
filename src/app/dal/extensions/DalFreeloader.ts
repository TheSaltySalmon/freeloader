/**
 Created by jimmypesola on 2016-04-01.
*/

///<reference path="../interface/IFreeloader.ts"/>
///<reference path="../Dal.ts"/>

namespace Dal {

    export class DalFreeloader extends Dal implements IFreeloader {

        public constructor() {
            super();
        }


        public createPlayer (player: IPlayer): number {
            return 0;
        }

        public fetchPlayer (playerID: IKey<string>, callback: IPlayerCallback): boolean {
            return true;
        }

        public updatePlayer (player: IPlayer): boolean {
            return true;
        }

        public removePlayer (playerID: IKey<number>): boolean {
            return true;
        }


        public createVehicle (vehicle: IVehicle): number {
            return 0;
        }

        public fetchVehicle (vehicleID: IKey<string>, callback: IVehicleCallback): boolean {
            return true;
        }

        public updateVehicle (vehicle: IVehicle): boolean {
            return true;
        }

        public removeVehicle (vehicleID: IKey<number>): boolean {
            return true;
        }
    }
}

/**
 Created by jimmypesola on 2016-04-01.
*/

///<reference path="./IKey.ts"/>

namespace Dal {

    export interface IPlayerCallback {
        (result: IPlayer): void;
    }

    export interface IVehicleCallback {
        (result: IVehicle): void;
    }

    export interface IFreeloader {


        createPlayer (player: IPlayer): number;
        fetchPlayer (playerID: IKey<string>, callback: IPlayerCallback): boolean;
        updatePlayer (player: IPlayer): boolean;
        removePlayer (playerID: IKey<number>): boolean;

        createVehicle (vehicle: IVehicle): number;
        fetchVehicle (vehicleID: IKey<string>, callback: IVehicleCallback): boolean;
        updateVehicle (vehicle: IVehicle): boolean;
        removeVehicle (vehicleID: IKey<number>): boolean;


    }

    export interface IWeapon {
        typeName: string;
        magazine: IMagazine;
        optics: string;
        muzzle: string;
        pointer: string;
    }

    export interface IMagazine {
        typeName: string;
        rounds: string;
    }

    export interface IInventory {
        weapons: IWeapon[];
        magazines: IMagazine[];
        items: string[];
        tools: string[];
    }

    export interface IBackpack extends IInventory {
        backpack: IInventory;
    }

    export interface IPlayer {
        playerID: number;
        accountID: number;
        name: string;
        xPos: number;
        yPos: number;
        world: number;
        alive: boolean;
        bleeding: boolean;
        brokenLegs: boolean;
        health: number;
        alcohol: number;
        bodyTemp: number;
        oxygenLevel: number;
        inventory: IInventory;
    }

    export interface IDamageVector {
        totalDamage: number;
        hull: number;
        engines: number[];
        wings: number[];
        fuelCells: number[];
    }

    export interface IUpgradePart {
        thrusters: string[];
        armor: string;
        cannons: string[];
        ballast: string[];
        fuelCell: string[];
    }

    export interface IVehicle {
        vehicleID: number;
        accountID: number;
        xPos: number;
        yPos: number;
        world: number;
        inventory: IInventory;
        damage: IDamageVector;
        oxygenLevel: number;
        upgradeParts: IUpgradePart;
        weight: number;
        fuel: number;
        capacity: number;
    }
}

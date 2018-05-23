﻿using FreeLoader.Components;
using FreeLoader.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Units
{
    public class Fuel : IFuel
    {
        private int _currentFuel;
        private float _fuelCombustionCounter;

        public int MaxFuel = 100;
        public int StartingFuel = 50;

        #region properties

        public int CurrentFuel
        {
            get
            {
                return _currentFuel;
            }
            set
            {
                _currentFuel = (value > MaxFuel ? MaxFuel : value);

                if (_currentFuel <= 0)
                {
                    TriggerOutOfFuelEvent();
                }            
            }
        }

        public bool IsOutOfFuel
        {
            get
            {
                return CurrentFuel <= 0;
            }
            set
            {
                CurrentFuel = value == false ? CurrentFuel : 0;
            }
        }

        #endregion

        public Fuel()
        {
            CurrentFuel = StartingFuel;
            TriggerFuelGainedEvent((int)StartingFuel);
        }

        public void CombustFuel(float combustionAmmount)
        {
            _fuelCombustionCounter += combustionAmmount;

            if (_fuelCombustionCounter > 1)
            {
                var lostFuelAmmount = (int)Math.Round(_fuelCombustionCounter, 1);

                CurrentFuel -= lostFuelAmmount;
                TriggerFuelLostEvent(lostFuelAmmount);
                _fuelCombustionCounter = 0;
            }
        }

        #region Private methods

        private void TriggerOutOfFuelEvent()
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_OUT_OF_FUEL,
                null
            );
        }

        private void TriggerFuelGainedEvent(int fuelGained)
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_GAINED_FUEL,
                new EventDataModels.Fuel
                {
                    MaxFuel = (int)MaxFuel,
                    CurrentFuel = (int)CurrentFuel,
                    FuelAmount = fuelGained,
                    Effect = EffectType.Gained
                }
            );
        }

        private void TriggerFuelLostEvent(int fuelLost)
        {
            Game.Services.EventManager.TriggerEvent(
                AvailableEvents.PLAYER_LOST_FUEL,
                new EventDataModels.Fuel
                {
                    MaxFuel = (int)MaxFuel,
                    CurrentFuel = (int)CurrentFuel,
                    FuelAmount = fuelLost,
                    Effect = EffectType.Lost
                }
            );
        }

        #endregion
    }
}
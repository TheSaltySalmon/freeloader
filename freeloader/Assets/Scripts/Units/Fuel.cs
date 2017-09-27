using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

    private int _currentFuel;
    private float _fuelCombustionCounter;
    private Rigidbody2D _rigidBody;

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

    // Use this for initialization
    void Start()
    {
        CurrentFuel = StartingFuel;
        
        TriggerFuelGainedEvent((int)StartingFuel);
    }

    public void CombustFuel(float combustionAmmount)
    {
        _fuelCombustionCounter += combustionAmmount;

        if(_fuelCombustionCounter > 1)
        {
            var lostFuelAmmount = (int)Math.Round(_fuelCombustionCounter, 1);
        
            CurrentFuel -= lostFuelAmmount;
            TriggerFuelLostEvent(lostFuelAmmount);
            _fuelCombustionCounter = 0;
        }
    }

    #region Private methods

    private void TriggerFuelGainedEvent(int fuelGained)
    {
        Scene.Events.TriggerEvent(
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
        Scene.Events.TriggerEvent(
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

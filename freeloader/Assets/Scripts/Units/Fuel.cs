using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

    private float _currentFuel;
    private Rigidbody2D _rigidBody;

    public float MaxFuel = 100;
    public float StartingFuel = 100;

    #region properties

    public float CurrentFuel
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

    public void CombustFuel(float fuelAmmount)
    {
        CurrentFuel -= fuelAmmount;

        if (Math.Round(CurrentFuel, 1) % 1 == 0)
        {
            TriggerFuelLostEvent(1);
            Debug.Log("Its a whole number: " + fuelAmmount);
        }
    }

    #region Private methods

    private void TriggerFuelGainedEvent(int fuelGained)
    {
        Scene.Events.TriggerEvent(
            AvailableEvents.PLAYER_LOST_FUEL,
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

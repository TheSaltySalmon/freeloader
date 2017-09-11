using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar {

    private GameObject _slider;

    private const string RESOURCE_SLIDER = "UI/Slider";

    public HealthBar()
    {
        LoadResourceAndSetup();
        AddEventListeners();
    }

    #region Private methods

    private void AddEventListeners()
    {
        Debug.Log(Scene.Events);

        Scene.Events.StartListening(
            AvailableEvents.PLAYER_LOST_HEALTH,
            new UnityAction<object>(UpdateHealthBar)
        );
    }
    private void UpdateHealthBar(object data)
    {
        var healthData = (EventDataModels.Health)data;

        Debug.Log("Got health " + healthData.Effect + ": " + healthData.HealthAmmount + ": " + healthData.CurrentHealth);
    }

    private void LoadResourceAndSetup()
    {
        _slider = Scene.ObjectPool.GetSingle(RESOURCE_SLIDER);
    }

    #endregion
}

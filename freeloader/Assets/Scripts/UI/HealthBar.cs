using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar {

    private GameObject _slider;

    private const string RESOURCE_SLIDER = "UI/Slider";

    public HealthBar()
    {
        LoadResourceAndSetup();	
    }

    SceneController.EventManager

    private void LoadResourceAndSetup()
    {
        _slider = SceneController.ObjectPool.GetSingle(RESOURCE_SLIDER);
    }
}

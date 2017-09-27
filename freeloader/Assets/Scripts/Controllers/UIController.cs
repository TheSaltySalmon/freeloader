using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    private UIHealthBar _healthBar;
    private UIFuelBar _fuelBar;
    private Canvas _canvas;

    void Awake()
    {
        GetComponents();
    }

    #region Private methods
    
    public void SetupUI()
    {
        _fuelBar = new UIFuelBar();
        _fuelBar.AddToUI(gameObject, _canvas);

        _healthBar = new UIHealthBar();
        _healthBar.AddToUI(gameObject, _canvas);
    }

    private void GetComponents()
    {
        _canvas = gameObject.GetComponentInParent<Canvas>();
    }

    #endregion
}

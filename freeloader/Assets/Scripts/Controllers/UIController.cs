using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    private UIHealthBar _healthBar;
    private Canvas _canvas;

    void Awake()
    {
        GetComponents();
    }

    #region Private methods
    
    public void SetupUI()
    {
        _healthBar = new UIHealthBar();
        _healthBar.AddToUI(gameObject, _canvas);
    }

    private void GetComponents()
    {
        _canvas = gameObject.GetComponentInParent<Canvas>();
    }

    #endregion
}

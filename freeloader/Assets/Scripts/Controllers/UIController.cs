using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    private HealthBar _healthBar;
    
    public UIController()
    {
        SetupUI();
    }

    #region Private methods
    private void SetupUI()
    {
        _healthBar = new HealthBar();
    }

    #endregion
}

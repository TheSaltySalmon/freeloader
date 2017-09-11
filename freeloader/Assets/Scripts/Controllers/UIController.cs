using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    private HealthBar _healthBar;

    #region Private methods
    public void SetupUI()
    {
        _healthBar = new HealthBar();
    }

    #endregion
}

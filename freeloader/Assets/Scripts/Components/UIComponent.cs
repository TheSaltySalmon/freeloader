using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.UI;

public class UIComponent : MonoBehaviour 
{
    
    private UIController _ui;

    public UIController  UI { 
        get {
            return _ui;
        }
    }

    void Awake()
    {
        _ui = new UIController(gameObject);
    }
}

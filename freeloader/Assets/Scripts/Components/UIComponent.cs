using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeLoader.GameLogic.UI;
using FreeLoader.Attributes;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-90)]
    public class UIComponent : ComponentBase
    {

        private UIController _uiController;

        public UIController UIController
        {
            get
            {
                return _uiController;
            }
        }

        void Start()
        {
            _uiController = new UIController(gameObject);
        }
    }
}

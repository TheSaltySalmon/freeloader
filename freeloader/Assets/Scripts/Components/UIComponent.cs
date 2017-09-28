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

        private UIController _ui;

        public UIController UI
        {
            get
            {
                return _ui;
            }
        }

        void Start()
        {
            _ui = new UIController(gameObject);
        }
    }
}

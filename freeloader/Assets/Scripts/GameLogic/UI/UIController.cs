using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic.UI
{
    public class UIController
    {
        private HealthBar _healthBar;
        private FuelBar _fuelBar;
        private Canvas _canvas;
        private GameObject _gameObject;

        public UIController(GameObject gameObject)
        {
            _gameObject = gameObject;

            GetComponents();

            SetupUI();
        }

        private void SetupUI()
        {
            _fuelBar = new FuelBar();
            _fuelBar.AddToUI(_gameObject, _canvas);

            _healthBar = new HealthBar();
            _healthBar.AddToUI(_gameObject, _canvas);
        }

        #region Private methods

        private void GetComponents()
        {
            _canvas = _gameObject.GetComponentInParent<Canvas>();
        }

        #endregion
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using FreeLoader.Components;

namespace FreeLoader.GameLogic.UI
{
    public class FuelBar
    {

        private const string RESOURCE_BAR = "UI/Bar";
        private readonly Color BAR_COLOR = new Color(0.5f, 0.5f, 0.9f);
        private readonly Vector2 START_POSITION = new Vector2(320, -50);

        private GameObject _bar;
        private Slider _slider;
        private RectTransform _rectTransform;

        public FuelBar()
        {
            LoadResourceAndSetup();
            AddEventListeners();
        }

        public void AddToUI(GameObject UI, Canvas canvas)
        {
            _bar.transform.SetParent(UI.transform);
            _rectTransform = _bar.GetComponent<RectTransform>();

            _rectTransform.SetParent(canvas.transform);
            _rectTransform.anchoredPosition = START_POSITION;
        }

        #region Private methods

        private void AddEventListeners()
        {
            Game.Scene.Events.StartListening(
                AvailableEvents.PLAYER_LOST_FUEL,
                new UnityAction<object>(UpdateFuelBar)
            );

            Game.Scene.Events.StartListening(
                AvailableEvents.PLAYER_GAINED_FUEL,
                new UnityAction<object>(UpdateFuelBar)
            );
        }

        private void UpdateFuelBar(object data)
        {
            var fuelData = (EventDataModels.Fuel)data;

            _slider.maxValue = fuelData.MaxFuel;
            _slider.value = fuelData.CurrentFuel;
        }

        private void LoadResourceAndSetup()
        {
            _bar = Game.Scene.ObjectPool.GetSingle(RESOURCE_BAR);
            _bar.SetActive(true);
            _slider = _bar.GetComponent<Slider>();

            SetupBarFillColor();
        }

        private void SetupBarFillColor()
        {
            var fill = _slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            if (fill != null)
            {
                fill.color = BAR_COLOR;
            }
        }

        #endregion
    }
}
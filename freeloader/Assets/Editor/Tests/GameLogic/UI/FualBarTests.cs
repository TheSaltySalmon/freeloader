using FreeLoader.GameLogic.Units;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine;
using FreeLoader.GameLogic.UI;
using UnityEngine.UI;
using UnityEngine.Events;

namespace FreeLoader
{
    // Class
    class FuelBarTests
    {
        // Constructor
        public class FuelBarConstructor : TestsBase
        {
            private GameObject _barGameObject;

            [SetUp]
            protected void BeforeEach()
            {
                _barGameObject = Resources.Load("UI/Bar") as GameObject;

                MockedObjectPool.GetSingle(Arg.Any<string>()).Returns(_barGameObject);            
            }

            [Test]
            public void It_should_set_bar_active_and_setup_bar_fill_color()
            {
                // Arrange
                var barColor = new Color(0.5f, 0.5f, 0.9f);

                // Act
                new FuelBar();

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();
                var fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
                
                Assert.That(Equals(fill.color, barColor));
                Assert.That(Equals(_barGameObject.activeSelf, true));

                MockObjectPoolServiceAndMethods();
            }

            [Test]
            public void It_should_add_event_listeners_for_player_fuel()
            {
                // Act
                new FuelBar();

                // Assert
                MockedEventManager.Received().StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_FUEL),
                    Arg.Any<UnityAction<object>>()
                );

                MockedEventManager.Received().StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_FUEL),
                    Arg.Any<UnityAction<object>>()
                );
            }

            [Test]
            public void It_should_update_fuelbar_on_triggered_player_gained_fuel_event()
            {
                // Arrange
                var fuelEventData = new EventDataModels.Fuel{ CurrentFuel = 10, MaxFuel = 20 };
                UnityAction<object> sutPlayerGainedFuelActionToInvoke = null;

                // Get delgate function to trigger.
                MockedEventManager.StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_FUEL), 
                    Arg.Do<UnityAction<object>>(x => sutPlayerGainedFuelActionToInvoke = x),
                    Arg.Any<string>()
                );

                // Act
                new FuelBar();
                sutPlayerGainedFuelActionToInvoke.Invoke(fuelEventData);

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();

                Assert.That(Equals((int)slider.value, (int)fuelEventData.CurrentFuel));
                Assert.That(Equals((int)slider.maxValue, (int)fuelEventData.MaxFuel));
            }

            [Test]
            public void It_should_update_fuelbar_on_triggered_player_lost_fuel_event()
            {
                // Arrange
                var fuelEventData = new EventDataModels.Fuel { CurrentFuel = 15, MaxFuel = 25 };
                UnityAction<object> sutPlayerLostFuelActionToInvoke = null;

                MockedEventManager.StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_FUEL),
                    Arg.Do<UnityAction<object>>(x => sutPlayerLostFuelActionToInvoke = x),
                    Arg.Any<string>()
                );

                // Act
                new FuelBar();
                sutPlayerLostFuelActionToInvoke.Invoke(fuelEventData);

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();

                Assert.That(Equals((int)slider.value, (int)fuelEventData.CurrentFuel));
                Assert.That(Equals((int)slider.maxValue, (int)fuelEventData.MaxFuel));
            }
        }

        public class AddToUI : TestsBase
        {
            private GameObject _barGameObject;
            private FuelBar _sut;
            
            [SetUp]
            protected void BeforeEach()
            {
                var barAsset = Resources.Load("UI/Bar") as GameObject;
                _barGameObject = MonoBehaviour.Instantiate(barAsset) as GameObject;
                MockedObjectPool.GetSingle(Arg.Any<string>()).Returns(_barGameObject);

                _sut = new FuelBar();
            }

            [Test]
            public void It_should_add_bar_game_object_to_ui()
            {
                // Arrange
                var mockedUI = new GameObject();
                var mockedCanvas = new Canvas();

                // mockedCanvas.transform = new GameObject();

                var rectTransform = _barGameObject.GetComponent<RectTransform>();

                // Act
                _sut.AddToUI(mockedUI, mockedCanvas);

                // Assert
                Assert.That(Equals(_barGameObject.transform.parent, mockedUI.transform));
            }
        }
    }
}

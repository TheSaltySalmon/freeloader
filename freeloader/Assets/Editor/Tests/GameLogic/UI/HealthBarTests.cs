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
    class HealthBarTests
    {
        // Constructor
        public class HealthBarConstructor : TestsBase
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
                var barColor = new Color(0.9f, 0.5f, 0.5f);

                // Act
                new HealthBar();

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();
                var fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
                
                Assert.That(Equals(fill.color, barColor));
                Assert.That(Equals(_barGameObject.activeSelf, true));

                MockObjectPoolServiceAndMethods();
            }

            [Test]
            public void It_should_add_event_listeners_for_player_health()
            {
                // Act
                new HealthBar();

                // Assert
                MockedEventManager.Received().StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_HEALTH),
                    Arg.Any<UnityAction<object>>()
                );

                MockedEventManager.Received().StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_HEALTH),
                    Arg.Any<UnityAction<object>>()
                );
            }

            [Test]
            public void It_should_update_HealthBar_on_triggered_player_gained_health_event()
            {
                // Arrange
                var healthEventData = new EventDataModels.Health{ CurrentHealth = 10, MaxHealth = 20 };
                UnityAction<object> sutPlayerGainedHealthActionToInvoke = null;

                // Get delgate function to trigger.
                MockedEventManager.StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_HEALTH), 
                    Arg.Do<UnityAction<object>>(x => sutPlayerGainedHealthActionToInvoke = x),
                    Arg.Any<string>()
                );

                // Act
                new HealthBar();
                sutPlayerGainedHealthActionToInvoke.Invoke(healthEventData);

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();

                Assert.That(Equals((int)slider.value, (int)healthEventData.CurrentHealth));
                Assert.That(Equals((int)slider.maxValue, (int)healthEventData.MaxHealth));
            }

            [Test]
            public void It_should_update_HealthBar_on_triggered_player_lost_health_event()
            {
                // Arrange
                var healthEventData = new EventDataModels.Health { CurrentHealth = 15, MaxHealth = 25 };
                UnityAction<object> sutPlayerLostHealthActionToInvoke = null;

                MockedEventManager.StartListening(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_HEALTH),
                    Arg.Do<UnityAction<object>>(x => sutPlayerLostHealthActionToInvoke = x),
                    Arg.Any<string>()
                );

                // Act
                new HealthBar();
                sutPlayerLostHealthActionToInvoke.Invoke(healthEventData);

                // Assert
                var slider = _barGameObject.GetComponent<Slider>();

                Assert.That(Equals((int)slider.value, (int)healthEventData.CurrentHealth));
                Assert.That(Equals((int)slider.maxValue, (int)healthEventData.MaxHealth));
            }
        }

        public class AddToUI : TestsBase
        {
            private GameObject _barGameObject;
            private HealthBar _sut;
            
            [SetUp]
            protected void BeforeEach()
            {
                var barAsset = Resources.Load("UI/Bar") as GameObject;
                _barGameObject = MonoBehaviour.Instantiate(barAsset) as GameObject;
                MockedObjectPool.GetSingle(Arg.Any<string>()).Returns(_barGameObject);

                _sut = new HealthBar();
            }

            [Test]
            public void It_should_add_bar_game_object_to_ui()
            {
                // Arrange
                var mockedUI = new GameObject("MockedUI", typeof(Canvas));
                var mockedCanvas = mockedUI.GetComponent<Canvas>();

                // Act
                _sut.AddToUI(mockedUI, mockedCanvas);

                // Assert
                Assert.That(Equals(_barGameObject.transform.parent, mockedUI.transform));
            }
        }
    }
}

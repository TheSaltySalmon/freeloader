using FreeLoader.GameLogic.Units;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine;

namespace FreeLoader
{
    // Class
    class FuelTests
    {

        // Constructor
        public class FuelConstructor : TestsBase
        {
            [SetUp]
            public void BeforeEach()
            {
                MockedEventManager.ClearReceivedCalls();
            }

            [Test]
            public void It_should_have_same_current_fuel_and_starting_fuel_after_construction()
            {
                // Act
                var sut = new Fuel();

                // Assert
                Assert.That(Equals(sut.CurrentFuel, sut.StartingFuel));
            }

            [Test]
            public void It_should_trigger_fuel_gained_event_with_starting_fuel_as_current_fuel_after_construction()
            {
                // Act
                var sut = new Fuel();

                // Assert
                var expectedCurrentFuel = sut.StartingFuel;

                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_FUEL),
                    Arg.Is<EventDataModels.Fuel>(x => x.CurrentFuel == expectedCurrentFuel),
                    Arg.Any<string>()
                );
            }
        }

        public class CombustFuel : TestsBase
        {
            private Fuel _sut;

            [SetUp]
            public void BeforeEach()
            {
                _sut = new Fuel();

                MockedEventManager.ClearReceivedCalls();
            }

            [Test]
            public void It_should_trigger_fuel_lost_event_only_if_total_combustion_ammount_exceeds_1()
            {
                // Act (with some assertion)
                CheckDidNotTriggerLostFuelEvent();
                _sut.CombustFuel(0.9f);
                CheckDidNotTriggerLostFuelEvent();
                _sut.CombustFuel(0.2f);

                // Assert
                MockedEventManager.ReceivedWithAnyArgs();
            }

            [Test]
            public void It_should_trigger_fuel_lost_event_with_correct_rounded_ammount(
                [Values(2.1f, 2.3f, 4.95f)] float fuelLost
            )
            {
                // Arrange
                var expectedLostFuelAmount = (int)Math.Round(fuelLost, 1);
                var expectedCurrentFuelAmmount = _sut.CurrentFuel - expectedLostFuelAmount;

                // Act
                _sut.CombustFuel(fuelLost);

                // Assert
                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_FUEL),
                    Arg.Is<EventDataModels.Fuel>(x => x.FuelAmount == expectedLostFuelAmount),
                    Arg.Any<string>()
                );

                Assert.That(Equals(_sut.CurrentFuel, expectedCurrentFuelAmmount));
            }

            [Test]
            public void It_should_trigger_out_of_fuel_event_when_out_of_fuel()
            {
                // Act
                _sut.CombustFuel(101);  

                // Assert
                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_OUT_OF_FUEL),
                    Arg.Any<object>(),
                    Arg.Any<string>()
                );
            }

            [Test]
            public void It_should_NOT_trigger_out_of_fuel_event_when_lost_some_fuel()
            {
                // Act
                _sut.CombustFuel(10.5f);

                // Assert
                MockedEventManager.DidNotReceive().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_OUT_OF_FUEL),
                    Arg.Any<object>(),
                    Arg.Any<string>()
                );
            }

            # region Private methods

            private void CheckDidNotTriggerLostFuelEvent()
            {
                MockedEventManager.DidNotReceive().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_FUEL),
                    Arg.Any<EventDataModels.Fuel>(),
                    Arg.Any<string>());
            }

            #endregion
        }

        // Methods

    }
}

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
            [Test]
            public void It_should_have_same_current_fuel_and_starting_fuel_after_construction()
            {
                var sut = new Fuel();
                Assert.That(Equals(sut.CurrentFuel, sut.StartingFuel));
            }

            [Test]
            public void It_should_trigger_fuel_gained_event_with_starting_fuel_as_current_fuel_after_construction()
            {
                var sut = new Fuel();

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
            [Test]
            public void It_should_trigger_fuel_lost_event_only_if_total_combustion_ammount_exceeds_1()
            {
                var sut = new Fuel();

                CheckDidNotTriggerLostFuelEvent();
                sut.CombustFuel(0.9f);
                CheckDidNotTriggerLostFuelEvent();
                sut.CombustFuel(0.2f);
                MockedEventManager.ReceivedWithAnyArgs();
            }

            [Test]
            public void It_should_trigger_fuel_lost_event_with_correct_rounded_ammount(
                [Values(1.3f,2.3f,4.95f)] float fuelLost
            )
            {
                var sut = new Fuel();
                var expectedLostFuelAmount = (int)Math.Round(fuelLost, 1);
                var expectedCurrentFuelAmmount = sut.CurrentFuel - expectedLostFuelAmount; 

                sut.CombustFuel(fuelLost);

                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_FUEL),
                    Arg.Is<EventDataModels.Fuel>(x => x.FuelAmount == expectedLostFuelAmount),
                    Arg.Any<string>()
                );

                Assert.That(Equals(sut.CurrentFuel, expectedCurrentFuelAmmount));

                MockedEventManager.ClearReceivedCalls();
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

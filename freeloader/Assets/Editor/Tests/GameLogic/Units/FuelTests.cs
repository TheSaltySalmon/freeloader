using FreeLoader.GameLogic.Units;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.TestTools;

namespace FreeLoader
{
    // Class
    class FuelTests : TestsBase
    {
        // Constructor
        public class FuelConstructor
        {
            [Test]
            public void It_should_have_same_current_fuel_and_starting_fuel_after_construction()
            {
                var sut = new Fuel();
                Assert.That(Equals(sut.CurrentFuel, sut.StartingFuel));
            }
        }

        // Methods

    }
}

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
using FreeLoader.GameLogic.Player;
using FreeLoader.Interfaces;

namespace FreeLoader
{
    // Class
    class PlayerActionsTests
    {
        public class HandleMovement : TestsBase
        {
            private PlayerActions _sut;

            [OneTimeSetUp]
            protected void OneTimeSetup()
            {
                base.OneTimeSetup();
            }

            [SetUp]
            protected void BeforeEach()
            {
                _sut = new PlayerActions();
            }

            [Test]
            public void It_should_fire_projectile_on_player_fire()
            {

            }
        }
    }
}
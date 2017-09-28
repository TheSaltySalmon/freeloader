using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine.TestTools;
using NSubstitute;

namespace FreeLoader
{
    class TestsBase
    {
        // Mock Services available in Scene
        [OneTimeSetUp]
        public void OneTimeSetup(){

            Game.Scene.Events = Substitute.For<Services.EventManagerService>();
            Game.Scene.ObjectPool = Substitute.For<Services.ObjectPoolService>();
            Game.Scene.UI = Substitute.For<GameLogic.UI.UIController>();
        }
    }
}

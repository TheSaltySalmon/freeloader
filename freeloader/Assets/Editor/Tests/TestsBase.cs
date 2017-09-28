using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine.Events;
using UnityEngine;
using FreeLoader.Services;

namespace FreeLoader
{
    class TestsBase
    {    
        // Mock Services available in Scene
        [OneTimeSetUp]
        public void OneTimeSetup(){

            //Game.Scene.UI = Substitute.For<GameLogic.UI.UIController>(new GameObject());

            MockObjectPoolServiceAndMethods();
            MockEventManagerServiceAndMethods();
        }

        private void MockObjectPoolServiceAndMethods()
        {
            // Mock Service
            Game.Scene.ObjectPool = Substitute.For<Services.IObjectPool>();

            // Mock Methods

            /* GetSingle(string) */
            Game.Scene.ObjectPool.GetSingle(
                Arg.Any<string>()
            ).Returns(
                new GameObject()
            );

            /* Get(string, int) */
            var numberOfGameObjectsToReturn = 0;
            Game.Scene.ObjectPool.Get(
                Arg.Any<string>(), Arg.Do<int>(x => numberOfGameObjectsToReturn = x)
            ).Returns(
                GenerateGameObjectsListWithEntries(numberOfGameObjectsToReturn)
            );
        }

        private void MockEventManagerServiceAndMethods()
        {
            // Mock Service
            Game.Scene.EventManager = Substitute.For<Services.IEventManager>();

            // Mock Methods
            Game.Scene.EventManager.TriggerEvent(
                Arg.Any<AvailableEvents>(),
                Arg.Any<object>(),
                Arg.Any<string>()
            );
            Game.Scene.EventManager.StartListening(Arg.Any<AvailableEvents>(), Arg.Any<UnityAction<object>>(), Arg.Any<string>());
            Game.Scene.EventManager.StopListening(Arg.Any<AvailableEvents>(), Arg.Any<UnityAction<object>>(), Arg.Any<string>());
        }

        private List<GameObject> GenerateGameObjectsListWithEntries(int numberOfGameObjectsToReturn)
        {
            var returnList = new List<GameObject>();

            for (var i = 0; i < numberOfGameObjectsToReturn; i++)
            {
                returnList.Add(new GameObject());
            }

            return returnList;
        }
    }
}

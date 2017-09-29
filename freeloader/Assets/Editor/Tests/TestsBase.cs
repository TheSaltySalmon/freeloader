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
        protected IEventManager MockedEventManager; 
   
        // Mock Services available in Scene
        [OneTimeSetUp]
        public void OneTimeSetup(){

            //Game.Services.UI = Substitute.For<GameLogic.UI.UIController>(new GameObject());

            MockObjectPoolServiceAndMethods();
            MockEventManagerServiceAndMethods();
        }

        private void MockObjectPoolServiceAndMethods()
        {
            // Mock Service
            Game.Services.ObjectPool = Substitute.For<Services.IObjectPool>();

            // Mock Methods

            /* GetSingle(string) */
            Game.Services.ObjectPool.GetSingle(
                Arg.Any<string>()
            ).Returns(
                new GameObject()
            );

            /* Get(string, int) */
            var numberOfGameObjectsToReturn = 0;
            Game.Services.ObjectPool.Get(
                Arg.Any<string>(), Arg.Do<int>(x => numberOfGameObjectsToReturn = x)
            ).Returns(
                GenerateGameObjectsListWithEntries(numberOfGameObjectsToReturn)
            );
        }

        private void MockEventManagerServiceAndMethods()
        {
            // Mock Service
            MockedEventManager = Substitute.For<Services.IEventManager>();

            // Mock Methods
            MockedEventManager.TriggerEvent(
                Arg.Any<AvailableEvents>(),
                Arg.Any<object>(),
                Arg.Any<string>()
            );

            MockedEventManager.StartListening(Arg.Any<AvailableEvents>(), Arg.Any<UnityAction<object>>(), Arg.Any<string>());
            MockedEventManager.StopListening(Arg.Any<AvailableEvents>(), Arg.Any<UnityAction<object>>(), Arg.Any<string>());

            Game.Services.EventManager = MockedEventManager;
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

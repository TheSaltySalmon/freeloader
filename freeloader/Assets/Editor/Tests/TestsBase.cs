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
        protected IObjectPool MockedObjectPool;
        protected IInputAdapter MockedInputAdapter;
   
        // Mock Services available in Scene
        [OneTimeSetUp]
        protected void OneTimeSetup(){

            MockObjectPoolServiceAndMethods();
            MockEventManagerServiceAndMethods();
            MockInputAdapter();
        }

        private void MockInputAdapter()
        {
            MockedInputAdapter = Substitute.For<Services.IInputAdapter>();

            Game.Services.InputAdapter = MockedInputAdapter;
        }

        protected void MockObjectPoolServiceAndMethods()
        {
            // Mock Service
            MockedObjectPool = Substitute.For<Services.IObjectPool>();

            // Mock Methods

            /* GetSingle(string) */
            MockedObjectPool.GetSingle(
                Arg.Any<string>()
            ).Returns(
                new GameObject()
            );

            /* Get(string, int) */
            int numberOfGameObjectsToReturn = 0;

            MockedObjectPool.Get(
                Arg.Any<string>(), Arg.Do<int>(x => numberOfGameObjectsToReturn = x)
            ).Returns(
                GenerateGameObjectsListWithEntries(numberOfGameObjectsToReturn)
            );

            Game.Services.ObjectPool = MockedObjectPool;
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

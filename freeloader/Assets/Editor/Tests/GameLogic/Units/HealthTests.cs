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
    class HealthTests
    {
        // Constructor
        public class HealthConstructor : TestsBase
        {
            [Test]
            public void It_should_have_same_current_health_and_starting_health_after_construction()
            {
                // Act
                var sut = new Health();

                // Assert
                Assert.That(Equals(sut.CurrentHealth, sut.StartingHealth));
            }

            [Test]
            public void It_should_trigger_health_gained_event_with_starting_health_as_current_health_after_construction()
            {
                // Act
                var sut = new Health();

                // Assert
                var expectedCurrentHealth = sut.StartingHealth;
                
                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_GAINED_HEALTH),
                    Arg.Is<EventDataModels.Health>(x => x.CurrentHealth == expectedCurrentHealth),
                    Arg.Any<string>()
                );
            }
        }

        public class HandleCollisionHealthLoss : TestsBase
        {
            private Health _sut;

            [SetUp]
            public void BeforeEach()
            {
                _sut = new Health();
                _sut.StartingHealth = 100;

                MockedEventManager.ClearReceivedCalls();
            }

            [Test]
            public void It_should_trigger_health_lost_event_with_correct_health_lost_value(
                [Values(10000.3f, 3.6f)] float lostHealthCollisionFactor,
                [Values(123.4f, 0.45f)] float collisionVelocityMagnitude
            )
            {
                // Arrange
                var expectedHealthLostAmount = (int)(collisionVelocityMagnitude * collisionVelocityMagnitude * lostHealthCollisionFactor);
                var expectedCurrentHealthAmmount = _sut.CurrentHealth - expectedHealthLostAmount;

                _sut.LostHealthCollisionFactor = lostHealthCollisionFactor;
                
                // Act
                _sut.HandleCollisionHealthLoss(collisionVelocityMagnitude);

                // Assert
                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_LOST_HEALTH),
                    Arg.Is<EventDataModels.Health>(x => x.HealthAmount == expectedHealthLostAmount),
                    Arg.Any<string>()
                );

                Assert.That(Equals(_sut.CurrentHealth, expectedCurrentHealthAmmount));
            }

            [Test]
            public void It_should_trigger_died_event_when_lost_too_much_health()
            {
                // Arrange
                var collisionVelocityMagnitude = 123456789;

                _sut.LostHealthCollisionFactor = 20;

                // Act
                _sut.HandleCollisionHealthLoss(collisionVelocityMagnitude);

                // Assert
                MockedEventManager.Received().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_DIED),
                    Arg.Any<object>(),
                    Arg.Any<string>()
                );
            }

            [Test]
            public void It_should_NOT_trigger_died_event_when_lost_some_health()
            {
                // Arrange
                var collisionVelocityMagnitude = 5;
                _sut.LostHealthCollisionFactor = 2;

                // Act
                _sut.HandleCollisionHealthLoss(collisionVelocityMagnitude);

                // Assert
                MockedEventManager.DidNotReceive().TriggerEvent(
                    Arg.Is<AvailableEvents>(x => x == AvailableEvents.PLAYER_DIED),
                    Arg.Any<object>(),
                    Arg.Any<string>()
                );
            }
        }
    }
}

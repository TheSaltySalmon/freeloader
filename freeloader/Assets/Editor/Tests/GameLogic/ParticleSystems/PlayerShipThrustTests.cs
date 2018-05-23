using FreeLoader.GameLogic.ParticleSystems;
using FreeLoader.GameLogic.Player;
using FreeLoader.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FreeLoader
{
    class PlayerShipThrustTests
    {
        // Constructor
        public class PlayerShipThrustConstructor : TestsBase
        {
            private GameObject _shipGameObject;
            private IShipMovement _shipMovement;
            
            [SetUp]
            protected void BeforeEach()
            {
                _shipGameObject = Resources.Load("Ships/CourierPlayerShip") as GameObject;
                _shipMovement = Substitute.For<IShipMovement>();

                MockedObjectPool.GetSingle(Arg.Any<string>()).Returns(_shipGameObject);
            }

            [Test]
            public void It_should_process_ship_asset_without_errors()
            {
                var sut = new PlayerShipThrust(_shipGameObject, _shipMovement);
            }
        }

        public class HandleShipThrottleParticleSystems : TestsBase
        {
            private GameObject _shipGameObject;
            private IShipMovement _shipMovement;
            private PlayerShipThrust _sut;

            [SetUp]
            protected void BeforeEach()
            {
                _shipGameObject = Resources.Load("Ships/CourierPlayerShip") as GameObject;
                _shipMovement = Substitute.For<IShipMovement>();

                MockedObjectPool.GetSingle(Arg.Any<string>()).Returns(_shipGameObject);

                _sut = new PlayerShipThrust(_shipGameObject, _shipMovement);

                _sut.MainThrottleParticleSys = Substitute.For<IParticleSystem>();
                _sut.RightThrottleParticleSys = Substitute.For<IParticleSystem>();
                _sut.LeftThrottleParticleSys = Substitute.For<IParticleSystem>();
            }

            [Test]
            public void It_should_NOT_be_playing_particle_systems_if_player_not_accelerating_or_rotating()
            {
                // Arrange
                _shipMovement.IsPlayerAcceleratingShip.Returns(false);
                _shipMovement.IsPlayerRotatingShipLeft.Returns(false);
                _shipMovement.IsPlayerRotatingShipRight.Returns(false);

                // Act
                _sut.HandleShipThrottleParticleSystems();

                // Assert
                _sut.MainThrottleParticleSys.DidNotReceive().Play();
                _sut.LeftThrottleParticleSys.DidNotReceive().Play();
                _sut.RightThrottleParticleSys.DidNotReceive().Play();
            }

            [Test]
            public void It_should_be_playing_main_throttle_particle_system_if_player_is_accelerating_ship()
            {
                // Arrange
                _shipMovement.IsPlayerAcceleratingShip.Returns(true);

                // Act
                _sut.HandleShipThrottleParticleSystems();
                
                // Assert
                _sut.MainThrottleParticleSys.Received().Play();
                _sut.LeftThrottleParticleSys.DidNotReceive().Play();
                _sut.RightThrottleParticleSys.DidNotReceive().Play();
            }

            [Test]
            public void It_should_be_playing_left_throttle_particle_system_if_player_is_rotating_ship_right()
            {
                // Arrange
                _shipMovement.IsPlayerRotatingShipRight.Returns(true);

                // Act
                _sut.HandleShipThrottleParticleSystems();

                // Assert
                _sut.MainThrottleParticleSys.DidNotReceive().Play();
                _sut.LeftThrottleParticleSys.Received().Play();
                _sut.RightThrottleParticleSys.DidNotReceive().Play();
            }

            [Test]
            public void It_should_be_playing_right_throttle_particle_system_if_player_is_rotating_ship_left()
            {
                // Arrange
                _shipMovement.IsPlayerRotatingShipLeft.Returns(true);

                // Act
                _sut.HandleShipThrottleParticleSystems();

                // Assert
                _sut.MainThrottleParticleSys.DidNotReceive().Play();
                _sut.LeftThrottleParticleSys.DidNotReceive().Play();
                _sut.RightThrottleParticleSys.Received().Play();
            }
        }
    }
}

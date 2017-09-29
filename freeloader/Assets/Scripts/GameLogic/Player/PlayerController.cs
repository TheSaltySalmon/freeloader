using FreeLoader.Components;
using FreeLoader.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Player
{
    public class PlayerController
    {
        private Units.Fuel _fuel;
        private Rigidbody2D _rigidBody;
        private ShipMovement _playerShipMovement;
        private Cameras.FollowingCamera _camera;
        private ParticleSystems.PlayerShipThrust _playerShipThrustParticles;
        private Units.Health _health;
        private GameObject _gameObject;
        private IComponent _playerComponent;

        #region Properties
        public Units.Health Health
        {
            get
            {
                return _health;
            }
        }

        #endregion

        public PlayerController(GameObject gameObject, IComponent playerComponent)
        {
            _gameObject = gameObject;
            _playerComponent = playerComponent;

            GetComponents();
            AddFunctionality();
        }

        // Fixed Update is called once per frame
        public void HandleFixedUpdate()
        {
            _playerShipMovement.HandleMovement();
            _camera.HandleFollowObject();
            _playerShipThrustParticles.HandleShipThrottleParticleSystems();
        }

        public void HandleCollision(Collision2D collision)
        {
            _health.HandleCollisionHealthLoss(collision.relativeVelocity.magnitude);
        }

        #region Private methods
        private void GetComponents()
        {
            _rigidBody = _gameObject.GetComponent<Rigidbody2D>();
        }

        private void AddFunctionality()
        {
            _health = new Units.Health();
            _fuel = new Units.Fuel();

            _playerShipMovement = new ShipMovement(_gameObject.transform, _rigidBody, _health, _fuel);
            _playerShipThrustParticles = new ParticleSystems.PlayerShipThrust(_gameObject, _playerShipMovement);
            _camera = new Cameras.FollowingCamera(_gameObject);

            new ParticleSystems.BurnDown(
                _gameObject.GetComponent<SpriteRenderer>(),
                _gameObject.transform,
                _playerComponent
            ).OnEvent(AvailableEvents.PLAYER_DIED);
        }

        #endregion
    }
}

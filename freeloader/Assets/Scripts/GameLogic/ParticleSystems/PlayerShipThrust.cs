using FreeLoader.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpromptuInterface;

namespace FreeLoader.GameLogic.ParticleSystems
{
    public class PlayerShipThrust
    {

        private const string MAIN_THROTTLE_PARTICLE_SYSTEM_NAME = "MainThrottleParticles";
        private const string RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME = "RightThrottleParticles";
        private const string LEFT_THROTTLE_PARTICLE_SYSTEM_NAME = "LeftThrottleParticles";

        public IParticleSystem MainThrottleParticleSys;
        public IParticleSystem LeftThrottleParticleSys;
        public IParticleSystem RightThrottleParticleSys;
        private Player.IShipMovement _playerShipMovement;
        private GameObject _playerShip;


        public PlayerShipThrust(GameObject playerShip, Player.IShipMovement playerShipMovement)
        {
            _playerShipMovement = playerShipMovement;
            _playerShip = playerShip;

            GetComponents();
        }

        public void HandleShipThrottleParticleSystems()
        {
            // Main Throttle
            PlayOrStopParticleSystemIfNeeded(
                _playerShipMovement.IsPlayerAcceleratingShip,
                MainThrottleParticleSys
            );

            // Right Throttle
            PlayOrStopParticleSystemIfNeeded(
                _playerShipMovement.IsPlayerRotatingShipLeft,
                RightThrottleParticleSys
            );

            // Left Throttle
            PlayOrStopParticleSystemIfNeeded(
                _playerShipMovement.IsPlayerRotatingShipRight,
                LeftThrottleParticleSys
            );
        }

        #region Private Methods

        private void GetComponents()
        {
            MainThrottleParticleSys = GetParticleSystemComponentByName(MAIN_THROTTLE_PARTICLE_SYSTEM_NAME).ActLike<IParticleSystem>();
            LeftThrottleParticleSys = GetParticleSystemComponentByName(LEFT_THROTTLE_PARTICLE_SYSTEM_NAME).ActLike<IParticleSystem>();
            RightThrottleParticleSys = GetParticleSystemComponentByName(RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME).ActLike<IParticleSystem>();
        }

        private ParticleSystem GetParticleSystemComponentByName(string particleSystemName)
        {
            foreach (ParticleSystem childParticleSystem in _playerShip.GetComponentsInChildren<ParticleSystem>())
            {
                if (childParticleSystem.name == particleSystemName)
                {
                    return childParticleSystem;
                }
            }
            return null;
        }

        private void PlayOrStopParticleSystemIfNeeded(bool shouldPlay, IParticleSystem particleSystem)
        {
            if (shouldPlay)
            {
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play();
                }
            }
            else
            {
                if (!particleSystem.isStopped)
                {
                    particleSystem.Stop();
                }
            }
        }

        #endregion
    }
}
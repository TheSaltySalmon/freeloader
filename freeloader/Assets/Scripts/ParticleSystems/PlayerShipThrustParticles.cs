using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipThrustParticles : MonoBehaviour {

    private const string MAIN_THROTTLE_PARTICLE_SYSTEM_NAME = "MainThrottleParticles";
    private const string RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME = "RightThrottleParticles";
    private const string LEFT_THROTTLE_PARTICLE_SYSTEM_NAME = "LeftThrottleParticles";

    private ParticleSystem _mainThrottleParticleSys;
    private ParticleSystem _leftThrottleParticleSys;
    private ParticleSystem _rightThrottleParticleSys;
    private PlayerShipMovement _playerShipMovement;


	// Use this for initialization
	void Start () {

        GetComponents();
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        HandleShipThrottleParticleSystems();
    }

    #region Private Methods

    private void GetComponents()
    {
        _playerShipMovement = GetComponent<PlayerShipMovement>();

        _mainThrottleParticleSys = GetParticleSystemComponentByName(MAIN_THROTTLE_PARTICLE_SYSTEM_NAME);
        _leftThrottleParticleSys = GetParticleSystemComponentByName(LEFT_THROTTLE_PARTICLE_SYSTEM_NAME);
        _rightThrottleParticleSys = GetParticleSystemComponentByName(RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME);
    }

    private ParticleSystem GetParticleSystemComponentByName(string particleSystemName)
    {
        foreach (ParticleSystem childParticleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            if (childParticleSystem.name == particleSystemName)
            {
                return childParticleSystem;
            }
        }
        return null;
    }

    private void HandleShipThrottleParticleSystems()
    {
        // Main Throttle
        PlayOrStopParticleSystemIfNeeded(
            _playerShipMovement.IsPlayerCurrentlyAcceleratingShipByInput,
            _mainThrottleParticleSys
        );

        // Right Throttle
        PlayOrStopParticleSystemIfNeeded(
            _playerShipMovement.IsPlayerCurrentlyRotationShipLeftByInput,
            _rightThrottleParticleSys
        );

        // Left Throttle
        PlayOrStopParticleSystemIfNeeded(
            _playerShipMovement.IsPlayerCurrentlyRotationShipRightByInput,
            _leftThrottleParticleSys
        );
    }

    private void PlayOrStopParticleSystemIfNeeded(bool shouldPlay, ParticleSystem particleSystem)
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

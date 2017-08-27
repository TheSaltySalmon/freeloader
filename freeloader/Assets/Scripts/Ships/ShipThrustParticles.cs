using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrustParticles : MonoBehaviour {

    private const string MAIN_THROTTLE_PARTICLE_SYSTEM_NAME = "MainThrottleParticles";
    private const string RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME = "RightThrottleParticles";
    private const string LEFT_THROTTLE_PARTICLE_SYSTEM_NAME = "LeftThrottleParticles";

    private ParticleSystem mainThrottleParticleSys;
    private ParticleSystem leftThrottleParticleSys;
    private ParticleSystem rightThrottleParticleSys;
    private PlayerShipMovement playerShipMovement;


	// Use this for initialization
	void Start () {

        playerShipMovement = GetComponent<PlayerShipMovement>();

        Debug.Log(playerShipMovement == null);

        mainThrottleParticleSys = GetParticleSystemByName(MAIN_THROTTLE_PARTICLE_SYSTEM_NAME);
        leftThrottleParticleSys = GetParticleSystemByName(LEFT_THROTTLE_PARTICLE_SYSTEM_NAME);
        rightThrottleParticleSys = GetParticleSystemByName(RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleShipThrottleParticleSystems();
    }

    #region Private Methods

    private ParticleSystem GetParticleSystemByName(string particleSystemName)
    {
        Component[] children = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem childParticleSystem in children)
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
            playerShipMovement.IsPlayerCurrentlyAcceleratingShipByInput,
            mainThrottleParticleSys
        );

        // Right Throttle
        PlayOrStopParticleSystemIfNeeded(
            playerShipMovement.IsPlayerCurrentlyRotationShipLeftByInput,
            rightThrottleParticleSys
        );

        // Left Throttle
        PlayOrStopParticleSystemIfNeeded(
            playerShipMovement.IsPlayerCurrentlyRotationShipRightByInput,
            leftThrottleParticleSys
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public bool isShipRotationUpgraded;

    private const string MAIN_THROTTLE_PARTICLE_SYSTEM_NAME = "MainThrottleParticles";
    private const string RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME = "RightThrottleParticles";
    private const string LEFT_THROTTLE_PARTICLE_SYSTEM_NAME = "LeftThrottleParticles";

    private Rigidbody2D rigidBody;
    private ParticleSystem mainThrottleParticleSys;
    private ParticleSystem leftThrottleParticleSys;
    private ParticleSystem rightThrottleParticleSys;

    #region Properties

    private bool IsPlayerCurrentlyRotationShipLeftByInput
    {
        get
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }
    }

    private bool IsPlayerCurrentlyRotationShipRightByInput
    {
        get
        {
            return Input.GetKey(KeyCode.RightArrow);
        }
    }

    private bool IsPlayerCurrentlyRotatingShipByInput {
        get {
            return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        }
    }

    private bool IsPlayerCurrentlyAcceleratingShipByInput
    {
        get
        {
            return Input.GetKey(KeyCode.UpArrow);
        }
    }

    #endregion

	// Initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        mainThrottleParticleSys = GetParticleSystemByName(MAIN_THROTTLE_PARTICLE_SYSTEM_NAME);
        leftThrottleParticleSys = GetParticleSystemByName(LEFT_THROTTLE_PARTICLE_SYSTEM_NAME);
        rightThrottleParticleSys = GetParticleSystemByName(RIGHT_THROTTLE_PARTICLE_SYSTEM_NAME);
	}
	
	// Called once per frame
	void Update () {
		
	}

    // Called once per frame (Physics)
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        AccelerateShip(verticalMovement);
        RotateShip(horizontalMovement);
        HandleShipThrottleParticleSystems();
    }

    #region Private methods

    private void HandleShipThrottleParticleSystems()
    {
        // Main Throttle
        PlayOrStopParticleSystemIfNeeded(
            IsPlayerCurrentlyAcceleratingShipByInput,
            mainThrottleParticleSys
        );

        // Right Throttle
        PlayOrStopParticleSystemIfNeeded(
            IsPlayerCurrentlyRotationShipLeftByInput,
            rightThrottleParticleSys
        );

        // Left Throttle
        PlayOrStopParticleSystemIfNeeded(
            IsPlayerCurrentlyRotationShipRightByInput,
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

    private void AccelerateShip(float verticalMovement)
    {
        // Only accelerate, we dont want the ship to move backwards.
        if (verticalMovement < 0) verticalMovement = 0; 

        rigidBody.AddForce(transform.up * verticalMovement * movementSpeed);
    }

    private void RotateShip(float horizontalMovement)
    {
        if(isShipRotationUpgraded)
        {
            float rotationValue = (horizontalMovement * rotationSpeed) * -1;
            transform.Rotate(0, 0, rotationValue);

            if (IsPlayerCurrentlyRotatingShipByInput)
            { 
                rigidBody.angularVelocity = 0;
            }
        }
        else
        {
            float rotationValue = (horizontalMovement * rotationSpeed / 10) * -1;
            rigidBody.AddTorque(rotationValue);
        }
    }

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

    #endregion
}


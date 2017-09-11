using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private Health _health;
    private Fuel _fuel;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private PlayerShipMovementService _playerShipMovement;
    private CameraService _cameraService;
    private PlayerShipThrustParticlesService _playerShipThrustParticlesService;

    #region Properties
    public Health Health
    {
        get
        {
            return _health;
        }
    }

    #endregion

    // Use this for initialization
	void Start () {
        GetComponents();
        AddComponents();
        AddServices();
	}
	
	// Fixed Update is called once per frame
    void FixedUpdate()
    {
        _playerShipMovement.HandleMovement();
        _cameraService.HandleFollowObject();
        _playerShipThrustParticlesService.HandleShipThrottleParticleSystems();
    }

    #region Private methods
    private void GetComponents()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void AddComponents()
    {
        _health = gameObject.AddComponent<Health>();
        _fuel = gameObject.AddComponent<Fuel>();
        gameObject.AddComponent<BurnDownIfDead>();
    }
    private void AddServices()
    {
        _playerShipMovement = new PlayerShipMovementService(gameObject.transform, _rigidBody, _health, _fuel);
        _playerShipThrustParticlesService = new PlayerShipThrustParticlesService(gameObject, _playerShipMovement);
        _cameraService = new CameraService(gameObject);
    }

    #endregion 
}

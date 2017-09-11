using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDownIfDead : MonoBehaviour
{

    private Health _health;
    private GameObject _burningEffect;
    private ParticleSystem.MainModule _burningParticlesMainModule;
    private TrailRenderer _burningTrail;
    private SpriteRenderer _spriteRenderer;
    private bool _hasBurnDownStarted;

    private const string RESOURCE_BURNING_EFFECT = "ParticleSystems/BurningEffect";
    private const float BURNING_RATE_WAIT_TIME = 0.4f;
    private const float BURNING_DECREASE_FACTOR = 0.05f;
    private readonly Color BURNT_COLOR = new Color(0.32f, 0.12f, 0.12f);
    private readonly Vector3 INITIAL_BURNING_EFFECT_POSITION = new Vector3(0.2f, 0.3f, -5);

	// Use this for initialization
	void Start () {
        GetComponents();
        LoadResourceAndSetup();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_health.IsAlive && !_burningEffect.activeSelf)
        {
            StartBurning();
            StartCoroutine(DecreaseBurningRate());
        }
    }

    #region Private methods

    private void GetComponents()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void StartBurning()
    {
        _spriteRenderer.color = BURNT_COLOR;
        _burningEffect.SetActive(true);
    }

    private void LoadResourceAndSetup()
    {
        _burningEffect = Scene.ObjectPool.GetSingle(RESOURCE_BURNING_EFFECT);
        _burningEffect.transform.parent = transform;
        _burningEffect.transform.position = INITIAL_BURNING_EFFECT_POSITION;

        _burningParticlesMainModule = _burningEffect.GetComponent<ParticleSystem>().main;
        _burningTrail = _burningEffect.GetComponent<TrailRenderer>();
    }


    private IEnumerator DecreaseBurningRate()
    {
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(BURNING_RATE_WAIT_TIME);

            var particleStartSizeMinMaxCurve = _burningParticlesMainModule.startSize;
            var particleStartLifeMinMaxCurve = _burningParticlesMainModule.startLifetime;

            particleStartSizeMinMaxCurve.constant -= BURNING_DECREASE_FACTOR;
            particleStartLifeMinMaxCurve.constant += BURNING_DECREASE_FACTOR * 3;

            _burningParticlesMainModule.startSize = particleStartSizeMinMaxCurve;
            _burningParticlesMainModule.startLifetime = particleStartLifeMinMaxCurve;
            _burningTrail.time -= _burningTrail.time * BURNING_DECREASE_FACTOR * 3;
        }

        // The burn out animation is done. Disable the trail.
        _burningTrail.enabled = false;
        _burningEffect.SetActive(false);
    }

    #endregion
}

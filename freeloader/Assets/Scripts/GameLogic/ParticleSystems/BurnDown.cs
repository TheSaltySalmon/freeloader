using FreeLoader.Components;
using FreeLoader.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FreeLoader.GameLogic.ParticleSystems
{
    public class BurnDown
    {
        private GameObject _burningEffect;
        private IComponent _component;
        private ParticleSystem.MainModule _burningParticlesMainModule;
        private TrailRenderer _burningTrail;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        private const string RESOURCE_BURNING_EFFECT = "ParticleSystems/BurningEffect";
        private const float BURNING_RATE_WAIT_TIME = 0.4f;
        private const float BURNING_DECREASE_FACTOR = 0.05f;
        private readonly Color BURNT_COLOR = new Color(0.32f, 0.12f, 0.12f);
        private readonly Vector3 INITIAL_BURNING_EFFECT_POSITION = new Vector3(0.2f, 0.3f, -5);

        public BurnDown(SpriteRenderer spriteRenderer, Transform transform, IComponent component)
        {
            _spriteRenderer = spriteRenderer;
            _transform = transform;
            _component = component;

            LoadResourceAndSetup();
        }

        public void OnEvent(AvailableEvents activationEvent)
        {
            Game.Services.EventManager.StartListening(
                activationEvent,
                new UnityAction<object>(StartBurning)
            );
        }

        public void StartBurning(object data)
        {
            if (!_burningEffect.activeSelf)
            {
                _spriteRenderer.color = BURNT_COLOR;
                _burningEffect.SetActive(true);
                _component.StartCoroutine(DecreaseBurningRate());
            }
        }

        #region Private methods


        private void LoadResourceAndSetup()
        {
            _burningEffect = Game.Services.ObjectPool.GetSingle(RESOURCE_BURNING_EFFECT);
            _burningEffect.transform.parent = _transform;
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
}
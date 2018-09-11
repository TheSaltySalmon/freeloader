using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeLoader.GameLogic.Projectiles;
using FreeLoader.Attributes;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-80)]
    public class PlasmaShotComponent : ComponentBase, IProjectileComponent
    {
        private IProjectile _plasmaShot;

        public void Awake()
        {
            _plasmaShot = new PlasmaShot(gameObject);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            _plasmaShot.HandleCollision(collision);
        }

        public void Fire(Transform weaponTransform) {
            _plasmaShot.HandleFire(weaponTransform);
        }
        
        public void FixedUpdate() {
            _plasmaShot.HandleFixedUpdate();
        }
    }
}

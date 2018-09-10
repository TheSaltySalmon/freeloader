using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeLoader.GameLogic.Projectiles;
using FreeLoader.Attributes;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-80)]
    public class PlasmaShotComponent : ComponentBase
    {
        private IProjectile _plasmaShot;

        public void Start()
        {
            _plasmaShot = new PlasmaShot(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {

        }
        
        void FixedUpdate() {
            _plasmaShot.HandleFixedUpdate();
        }
    }
}

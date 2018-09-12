using FreeLoader.Components;
using FreeLoader.Services;
using FreeLoader.GameLogic.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Weapons
{
    public class PlasmaCannon : IWeapon {
        private Transform _transform;

        public int ReloadTime {
            get {
                return 200;
            }
        }

        public PlasmaCannon(Transform transform)
        {
            _transform = transform;
        }

        public void Fire() {
            Game.Services.ObjectPool.GetSingle("Projectiles/PlasmaShot")
                .GetComponent<PlasmaShotComponent>().Fire(_transform);
        }
    }
}
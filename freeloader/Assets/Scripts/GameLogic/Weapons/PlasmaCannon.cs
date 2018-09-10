using FreeLoader.Components;
using FreeLoader.Services;
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
            var projectile = Game.Services.ObjectPool.GetSingle("Projectiles/PlasmaShot");

            projectile.transform.SetPositionAndRotation(
                _transform.position,
                _transform.rotation
            );

            projectile.SetActive(true);
        }
    }
}
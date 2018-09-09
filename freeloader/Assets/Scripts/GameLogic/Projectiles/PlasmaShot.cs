using FreeLoader.Components;
using FreeLoader.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Projectiles
{
    public class PlasmaShot : IProjectile {
        private GameObject _projectile;
        private int speed = 5;
        public int Damage {
            get { return 2; }
        }

        public PlasmaShot(Transform weaponTransForm)
        {
            _projectile = Game.Services.ObjectPool.GetSingle("Projectiles/Line");
            
            _projectile.transform.SetPositionAndRotation(
                weaponTransForm.position,
                weaponTransForm.rotation
            );
        }
    }
}
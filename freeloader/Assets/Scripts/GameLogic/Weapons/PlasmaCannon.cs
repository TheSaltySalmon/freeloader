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
        public int Damage {
            get {
                return 2;
            }
        }

        public PlasmaCannon(Transform transform)
        {
            _transform = transform;
        }

        public void Fire() {
            
        }
    }
}
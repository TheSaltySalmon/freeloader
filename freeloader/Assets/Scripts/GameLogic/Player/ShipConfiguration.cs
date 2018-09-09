using FreeLoader.Interfaces;
using FreeLoader.GameLogic.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Player
{
    public class ShipConfiguration : IShipConfiguration
    {
        private IWeapon _currentWeapon;
        public IWeapon CurrentWeapon {
            get { return _currentWeapon; }
            private set { _currentWeapon = value; }
        }

        public ShipConfiguration(Transform transform)
        {
            CurrentWeapon = new Weapons.PlasmaCannon(transform);
        }
    }
}
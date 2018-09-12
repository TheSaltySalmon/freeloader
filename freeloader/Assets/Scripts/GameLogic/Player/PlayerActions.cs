using FreeLoader.Interfaces;
using FreeLoader.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Player
{
    public class PlayerActions : IPlayerActions
    {
        IShipConfiguration _playerShipConfiguration;
        public PlayerActions(IShipConfiguration playerShipConfiguration)
        {
            _playerShipConfiguration = playerShipConfiguration;
        }

        // Should be called in a "FixedUpdate" (Physics)
        public void HandleActions(IInputAdapter playerInput)
        {
            if(playerInput.IsFiring) {
                FireWeapon();
            }
        }

        private void FireWeapon(){
            _playerShipConfiguration.CurrentWeapon.Fire();
        }
    }
}
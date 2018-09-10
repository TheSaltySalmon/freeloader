using FreeLoader.Components;
using FreeLoader.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using FreeLoader.Interfaces;
using UnityEngine;
using ImpromptuInterface;

namespace FreeLoader.GameLogic.Projectiles
{
    public class PlasmaShot : IProjectile {
        private GameObject _gameObject;
        private IRigidbody2D _rigidBody;

        private int speed = 200;
        public int Damage {
            get { return 2; }
        }

        public PlasmaShot(GameObject gameObject)
        {
            _gameObject = gameObject;
            GetComponents();
            // _projectile = Game.Services.ObjectPool.GetSingle("Projectiles/PlasmaShot");
            
            // _projectile.transform.SetPositionAndRotation(
            //     weaponTransForm.position,
            //     weaponTransForm.rotation
            // );

            _rigidBody.AddForce(gameObject.transform.up * speed);
        }

        public void HandleFixedUpdate() {
            _gameObject.transform.rotation = Quaternion.LookRotation(_rigidBody.velocity) * Quaternion.Euler(90, 0, 0);   
        }

        private void GetComponents()
        {
            _rigidBody = _gameObject.GetComponent<Rigidbody2D>().ActLike<IRigidbody2D>();
        }
    }
}
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
            FaceForward();
        }

        public void HandleFire(Transform weaponTransform) {
            
            _gameObject.transform.SetPositionAndRotation(
                weaponTransform.position + weaponTransform.up / 2,
                weaponTransform.rotation
            );
            _gameObject.SetActive(true);
            _rigidBody.AddForce(_gameObject.transform.up * speed);
        }

        public void HandleFixedUpdate() {
            FaceForward();   
        }

        public void HandleCollision(Collision2D collision){
            Debug.Log("Collision!");
            _gameObject.SetActive(false); 
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0;
            _gameObject.transform.rotation = Quaternion.Euler(Vector3.zero); 
        }

        private void GetComponents()
        {
            _rigidBody = _gameObject.GetComponent<Rigidbody2D>().ActLike<IRigidbody2D>();
        }

        private void FaceForward() {
            _gameObject.transform.rotation = Quaternion.LookRotation(_rigidBody.velocity) * Quaternion.Euler(-90, 0, 0);   
        }
    }
}
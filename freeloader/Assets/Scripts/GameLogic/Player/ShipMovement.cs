using FreeLoader.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Player
{
    public class ShipMovement
    {
        public float movementSpeed = 2.8f;
        public float rotationSpeed = 1.8f;
        public bool isShipRotationUpgraded = false;

        private IRigidbody2D _rigidBody;
        private Units.IHealth _health;
        private Units.IFuel _fuel;
        private Transform _transform;

        #region Properties

        public bool IsPlayerRotatingShipLeft
        {
            get
            {
                if (!_health.IsAlive || _fuel.IsOutOfFuel) { return false; }
                return Game.Services.InputAdapter.IsRotatingLeft;
            }
        }

        public bool IsPlayerRotatingShipRight
        {
            get
            {
                if (!_health.IsAlive || _fuel.IsOutOfFuel) { return false; }
                return Game.Services.InputAdapter.IsRotatingRight;
            }
        }

        public bool IsPlayerAcceleratingShip
        {
            get
            {
                if (!_health.IsAlive || _fuel.IsOutOfFuel) { return false; }
                return Game.Services.InputAdapter.IsAccelerating;
            }
        }

        public bool CanShipMove
        {
            get
            {
                return _health.IsAlive && !_fuel.IsOutOfFuel;
            }
        }

        #endregion

        // Constructor
        public ShipMovement(Transform transform, IRigidbody2D rigidBody, Units.IHealth health, Units.IFuel fuel)
        {
            _transform = transform;
            _rigidBody = rigidBody;
            _health = health;
            _fuel = fuel;
        }

        // Should be called in a "FixedUpdate" (Physics)
        public void HandleMovement(float horizontalMovement, float verticalMovement)
        {
            if (CanShipMove)
            {
                AccelerateShip(verticalMovement);
                RotateShip(horizontalMovement);
            }
        }

        #region Private methods

        private void AccelerateShip(float verticalMovement)
        {
            // Only accelerate forwards not backwards.
            if (verticalMovement > 0)
            {
                _rigidBody.AddForce(_transform.up * verticalMovement * movementSpeed);

                _fuel.CombustFuel(0.01f);
            }
        }

        private void RotateShip(float horizontalMovement)
        {
            if (horizontalMovement != 0)
            {
                if (isShipRotationUpgraded)
                {
                    _rigidBody.angularVelocity = 0;
                    float rotationValue = (horizontalMovement * rotationSpeed) * -1;
                    _rigidBody.MoveRotation(_rigidBody.rotation + rotationValue);
                }
                else
                {
                    float rotationValue = (horizontalMovement * rotationSpeed / 10) * -1;
                    _rigidBody.AddTorque(rotationValue);
                }

                _fuel.CombustFuel(0.01f);
            }
        }

        #endregion

        //class Buff
        //{
        //    public void Alter(ChangableGameObject objectToChange){
        //        objectToChange.Armor -= 1;
        //    }

        //    public void UnAlter(ChangableGameObject objectToChange){
        //        objectToChange.Armor += 1;
        //    }
        //}

        //class ChangableGameObject
        //{
        //    public int Armor = 1;
        //    private List<Buff> buffList = new List<Buff>();


        //    public void AddBuff(Buff buff)
        //    {
        //        buff.Alter(this);
        //        buffList.Add(buff);
        //    }

        //    public void RemoveBuff(Buff buff)
        //    {
        //        buff.UnAlter(this);
        //        buffList.Remove(buff);
        //    }
        //}
    }
}
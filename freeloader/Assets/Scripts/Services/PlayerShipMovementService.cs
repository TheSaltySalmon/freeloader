﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovementService
{

    public float movementSpeed = 2.8f;
    public float rotationSpeed = 1.2f;
    public bool isShipRotationUpgraded;

    private Rigidbody2D _rigidBody;
    private Health _health;
    private Transform _transform;

    #region Properties

    public bool IsPlayerCurrentlyRotationShipLeftByInput
    {
        get
        {
            if (!_health.IsAlive) { return false; }
            return Input.GetKey(KeyCode.LeftArrow);
        }
    }

    public bool IsPlayerCurrentlyRotationShipRightByInput
    {
        get
        {
            if (!_health.IsAlive) { return false; }
            return Input.GetKey(KeyCode.RightArrow);
        }
    }

    public bool IsPlayerCurrentlyRotatingShipByInput
    {
        get
        {
            if (!_health.IsAlive) { return false; }
            return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        }
    }

    public bool IsPlayerCurrentlyAcceleratingShipByInput
    {
        get
        {
            if (!_health.IsAlive) { return false; }
            return Input.GetKey(KeyCode.UpArrow);
        }
    }

    public bool CanShipMove
    {
        get
        {
            return _health.IsAlive;
        }
    }

    #endregion

    // Constructor
    public PlayerShipMovementService(Transform transform, Rigidbody2D rigidBody, Health health)
    {
        _transform = transform;
        _rigidBody = rigidBody;
        _health = health;
    }

    // Should be called in a "FixedUpdate" (Physics)
    public void HandleMovement()
    {
        if (CanShipMove)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

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
        }
    }

    private void RotateShip(float horizontalMovement)
    {
        if (isShipRotationUpgraded)
        {
            float rotationValue = (horizontalMovement * rotationSpeed) * -1;
            _transform.Rotate(0, 0, rotationValue);

            if (IsPlayerCurrentlyRotatingShipByInput)
            {
                _rigidBody.angularVelocity = 0;
            }
        }
        else
        {
            float rotationValue = (horizontalMovement * rotationSpeed / 10) * -1;
            _rigidBody.AddTorque(rotationValue);
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






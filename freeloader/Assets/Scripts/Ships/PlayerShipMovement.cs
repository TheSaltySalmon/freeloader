using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{

    public float movementSpeed;
    public float rotationSpeed;
    public bool isShipRotationUpgraded;

    private Rigidbody2D rigidBody;
    private PlayerHealth playerHealth;

    #region Properties

    public bool IsPlayerCurrentlyRotationShipLeftByInput
    {
        get
        {
            if (!playerHealth.IsAlive) { return false; }
            return Input.GetKey(KeyCode.LeftArrow);
        }
    }

    public bool IsPlayerCurrentlyRotationShipRightByInput
    {
        get
        {
            if (!playerHealth.IsAlive) { return false; }
            return Input.GetKey(KeyCode.RightArrow);
        }
    }

    public bool IsPlayerCurrentlyRotatingShipByInput
    {
        get
        {
            if (!playerHealth.IsAlive) { return false; }
            return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        }
    }

    public bool IsPlayerCurrentlyAcceleratingShipByInput
    {
        get
        {
            if (!playerHealth.IsAlive) { return false; }
            return Input.GetKey(KeyCode.UpArrow);
        }
    }

    public bool CanShipMove
    {
        get
        {
            return playerHealth.IsAlive;
        }
    }

    #endregion

    // Initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();

    }

    // Called once per frame
    void Update()
    {

    }

    // Called once per frame (Physics)
    void FixedUpdate()
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
            rigidBody.AddForce(transform.up * verticalMovement * movementSpeed);
        }
    }

    private void RotateShip(float horizontalMovement)
    {
        if (isShipRotationUpgraded)
        {
            float rotationValue = (horizontalMovement * rotationSpeed) * -1;
            transform.Rotate(0, 0, rotationValue);

            if (IsPlayerCurrentlyRotatingShipByInput)
            {
                rigidBody.angularVelocity = 0;
            }
        }
        else
        {
            float rotationValue = (horizontalMovement * rotationSpeed / 10) * -1;
            rigidBody.AddTorque(rotationValue);
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






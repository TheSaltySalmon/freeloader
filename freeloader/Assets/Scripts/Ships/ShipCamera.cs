using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour {

    private GameObject playerShip;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        playerShip = (FindObjectOfType(typeof(PlayerShipMovement)) as PlayerShipMovement).gameObject;

        // Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - playerShip.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = playerShip.transform.position + offset;
    }
}

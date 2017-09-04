using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollController : MonoBehaviour {

    private GameObject playerShip;
    public float yOffset;
    public float xOffset;
    public float scrollFactor;

    // Use this for initialization
    void Start()
    {
        playerShip = (FindObjectOfType(typeof(PlayerShipMovement)) as PlayerShipMovement).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = playerShip.transform.position.x / scrollFactor + xOffset;
        float yPos = playerShip.transform.position.y / scrollFactor + yOffset;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}

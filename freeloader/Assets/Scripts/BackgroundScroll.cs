using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private GameObject _playerShip;

    public float yOffset;
    public float xOffset;
    public float scrollFactor;

    // Use this for initialization
    void Start()
    {
        _playerShip = (FindObjectOfType(typeof(PlayerShipMovement)) as PlayerShipMovement).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = _playerShip.transform.position.x / scrollFactor + xOffset;
        float yPos = _playerShip.transform.position.y / scrollFactor + yOffset;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}

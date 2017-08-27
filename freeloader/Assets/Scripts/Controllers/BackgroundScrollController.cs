using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollController : MonoBehaviour {

    public GameObject camera;
    public float yOffset;
    public float xOffset;
    public float scrollFactor;

    // Use this for initialization
    void Start()
    {
        camera = (FindObjectOfType(typeof(ShipCamera)) as ShipCamera).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xPos = camera.transform.position.x / scrollFactor + xOffset;
        float yPos = camera.transform.position.y / scrollFactor + yOffset;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private GameObject _player;

    public float yOffset;
    public float xOffset;
    public float scrollFactor;

    // Use this for initialization
    void Start()
    {
        _player = (FindObjectOfType(typeof(PlayerController)) as PlayerController).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = _player.transform.position.x / scrollFactor + xOffset;
        float yPos = _player.transform.position.y / scrollFactor + yOffset;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}

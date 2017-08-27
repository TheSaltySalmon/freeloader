using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour {
    public GameObject playerShip;

	// Use this for initialization
	void Start () {
        playerShip = (FindObjectOfType(typeof(PlayerShipMovement)) as PlayerShipMovement).gameObject;
	}
}

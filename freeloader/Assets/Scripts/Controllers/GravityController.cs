using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

    public float sceneGravity = 0.02f;

	void Awake() {
        Physics.gravity = new Vector3(0.0f, sceneGravity, 0.0f);
    }
}

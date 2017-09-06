using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public static ObjectPoolService ObjectPool;

    public float SceneGravity = 0.02f;

	// Use this for initialization
	void Awake () {

        StartServices();
        SetGravity();
	}

	
	// Update is called once per frame
	void Update () {

    }

    #region Private methods

    private void SetGravity()
    {
        Physics.gravity = new Vector3(0.0f, SceneGravity, 0.0f);
    }

    private void StartServices(){

        ObjectPool = new ObjectPoolService();
    }

    #endregion
}

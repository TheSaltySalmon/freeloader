using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour  {

    public static ObjectPoolService ObjectPool;
    public static EventManagerService Events;
    public static PlayerController Player;
    public float SceneGravity = 0.02f;
    private HealthBar _healthBar;


	// Use this for initialization
	void Awake () {

        StartServices();
        SetGravity();
        FindObjectsAndGetReferences();
	}

	// Update is called once per frame
	void Update () {

    }

    #region Private methods
    

    private void FindObjectsAndGetReferences()
    {
        Player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    private void SetGravity()
    {
        Physics.gravity = new Vector3(0.0f, SceneGravity, 0.0f);
    }

    private void StartServices(){

        ObjectPool = new ObjectPoolService();
        EventManager = new EventManagerService();
    }

    #endregion
}

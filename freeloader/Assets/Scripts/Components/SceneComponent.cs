using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneComponent : MonoBehaviour  {

    public static Services.ObjectPoolService ObjectPool;
    public static Services.EventManagerService Events;
    public static GameLogic.UI.UIController UI;
    public static PlayerComponent Player;
    public const float SceneGravity = 0.02f;

	// Use this for initialization
	void Awake () {

        StartServices();
        FindObjectsAndGetReferences();
        Initialize();
	}

    #region Private methods
    
    private void Initialize()
    {
        SetGravity();
    }

    private void FindObjectsAndGetReferences()
    {
        Player = FindObjectOfType(typeof(PlayerComponent)) as PlayerComponent;
        UI = (FindObjectOfType(typeof(UIComponent)) as UIComponent).UI; 
    }


    private void StartServices(){

        ObjectPool = new Services.ObjectPoolService();
        Events = new Services.EventManagerService();
    }

    private void SetGravity()
    {
        Physics.gravity = new Vector3(0.0f, SceneGravity, 0.0f);
    }


    #endregion
}

using FreeLoader.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-100)]
    public class SceneComponent : ComponentBase
    {
        public Services.ObjectPoolService ObjectPool;
        public Services.EventManagerService Events;
        public GameLogic.UI.UIController UI;
        //public PlayerComponent Player;
        public const float SceneGravity = 0.02f;

        // Use this for initialization
        void Awake()
        {
            StartServices();
        }

        void Start()
        {
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
            UI = (FindObjectOfType(typeof(UIComponent)) as UIComponent).UI;
            //Player = FindObjectOfType(typeof(PlayerComponent)) as PlayerComponent;
        }


        private void StartServices()
        {

            ObjectPool = new Services.ObjectPoolService();
            Events = new Services.EventManagerService();

            Debug.Log(ObjectPool);
        }

        private void SetGravity()
        {
            Physics.gravity = new Vector3(0.0f, SceneGravity, 0.0f);
        }


        #endregion
    }
}

using FreeLoader.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-100)]
    public class SceneComponent : ComponentBase
    {
        public const float SceneGravity = 0.02f;

        // Use this for initialization
        void Awake()
        {
        }

        void Start()
        {
            Initialize();
        }

        #region Private methods

        private void Initialize()
        {
            SetGravity();
        }

        private void SetGravity()
        {
            Physics.gravity = new Vector3(0.0f, SceneGravity, 0.0f);
        }


        #endregion
    }
}

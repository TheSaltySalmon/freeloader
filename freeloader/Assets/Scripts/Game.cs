using FreeLoader.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeLoader
{
    /// <summary>
    /// A Static class for easy access of components.
    /// </summary>
    public class Game
    {
        private static SceneComponent _scene;

        public static SceneComponent Scene
        {
            get
            {
                return _scene ?? (_scene = MonoBehaviour.FindObjectOfType<SceneComponent>() as SceneComponent);
            }
        }
    }
}

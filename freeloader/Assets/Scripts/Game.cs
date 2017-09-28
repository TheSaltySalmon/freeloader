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
        private static UIComponent _ui;
        private static PlayerComponent _player;

        #region Properties

        public static SceneComponent Scene
        {
            get
            {
                return _scene ?? (_scene = MonoBehaviour.FindObjectOfType<SceneComponent>() as SceneComponent);
            }
        }

        public static UIComponent UI
        {
            get
            {
                return _ui ?? (_ui = MonoBehaviour.FindObjectOfType<UIComponent>() as UIComponent);
            }
        }

        public static PlayerComponent Player
        {
            get
            {
                return _player ?? (_player = MonoBehaviour.FindObjectOfType<PlayerComponent>() as PlayerComponent);
            }
        }

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeLoader.GameLogic.Player;
using FreeLoader.Attributes;

namespace FreeLoader.Components
{
    [ScriptExecutionOrder(-80)]
    public class PlayerComponent : ComponentBase
    {
        private PlayerController _playerController;

        public PlayerController PlayerController
        {
            get
            {
                return _playerController;
            }
        }

        public void Start()
        {
            _playerController = new PlayerController(
                gameObject,
                this
            );
        }
        void FixedUpdate()
        {
            _playerController.HandleFixedUpdate();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            _playerController.HandleCollision(collision);
        }

    }
}

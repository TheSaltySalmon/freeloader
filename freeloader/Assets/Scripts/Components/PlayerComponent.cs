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
        private PlayerController _player;

        public PlayerController Player
        {
            get
            {
                return _player;
            }
        }

        public void Start()
        {
            _player = new PlayerController(gameObject, this);
        }
        void FixedUpdate()
        {
            _player.HandleFixedUpdate();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            _player.HandleCollision(collision);
        }

    }
}

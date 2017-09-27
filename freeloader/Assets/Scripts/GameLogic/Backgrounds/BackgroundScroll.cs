using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Backgrounds
{
    public class BackgroundScroll {

        private GameObject _player;

        private float _yOffset;
        private float _xOffset;
        private float _scrollFactor;
        private Transform _transform;

        public BackgroundScroll(Transform transform, float yOffset, float xOffset, float scrollFactor)
        {
            _transform = transform;
            _yOffset = yOffset;
            _xOffset = xOffset;
            _scrollFactor = scrollFactor;

            _player = (MonoBehaviour.FindObjectOfType(typeof(PlayerComponent)) as PlayerComponent).gameObject;
        }

        public void HandleUpdate()
        {
            float xPos = _player.transform.position.x / _scrollFactor + _xOffset;
            float yPos = _player.transform.position.y / _scrollFactor + _yOffset;

            _transform.position = new Vector3(xPos, yPos, _transform.position.z);
        }
    }
}


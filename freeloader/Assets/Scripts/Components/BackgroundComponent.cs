using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FreeLoader.GameLogic.Backgrounds;

namespace FreeLoader.Components
{
    public class BackgroundComponent : ComponentBase
    {
        public float yOffset;
        public float xOffset;
        public float scrollFactor;
        private BackgroundScroll _backgroundScroll;

        void Awake()
        {
            _backgroundScroll = new BackgroundScroll(
                gameObject.transform,
                yOffset,
                xOffset,
                scrollFactor
            );
        }

        void Update()
        {
            _backgroundScroll.HandleUpdate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum DeviceType
{
    Desktop,
    Android,
    IOS
}

namespace FreeLoader.Services
{
    class InputAdapter : IInputAdapter
    {
        private DeviceType _deviceType;

        public InputAdapter(DeviceType deviceType = DeviceType.Desktop)
        {
            _deviceType = deviceType;
        }

        #region Properties

        public bool IsRotatingLeft
        {
            get
            {
                return Input.GetKey(KeyCode.LeftArrow);
            }
        }

        public bool IsRotatingRight
        {
            get
            {
                return Input.GetKey(KeyCode.RightArrow);
            }
        }

        public bool IsRotating
        {
            get
            {
                return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
            }
        }

        public bool IsAccelerating
        {
            get
            {
                return Input.GetKey(KeyCode.UpArrow);
            }
        }

        public bool IsDeAccelerating
        {
            get
            {
                return Input.GetKey(KeyCode.DownArrow);
            }
        }

        public float HorizontalAxis
        {
            get
            {
                return Input.GetAxis("Horizontal");
            }
        }

        public float VerticalAxis
        {
            get
            {
                return Input.GetAxis("Vertical");
            }
        }

        public bool IsFiring
        {
            get {
                return Input.GetKey(KeyCode.LeftControl);
            }
        }

        #endregion
    }
}

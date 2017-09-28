using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeLoader.GameLogic.Cameras
{

    public class FollowingCamera {

        private Vector3 _offset;
        private Camera _camera;
        private GameObject _objectToFollow;

        public GameObject ObjectToFollow { get; set; }

        public FollowingCamera(GameObject objectToFollow)
        {
            _camera = MonoBehaviour.FindObjectOfType(typeof(Camera)) as Camera;

            ObjectToFollow = objectToFollow;

            // Calculate and store the offset value by getting the distance between the player's position and camera's position.
            _offset = _camera.transform.position - objectToFollow.transform.position;
        }

        // Should be called each frame
        public void HandleFollowObject()
        {
            // Set the position of the camera's transform to be the same as the objects, but offset by the calculated offset distance.
            _camera.transform.position = ObjectToFollow.transform.position + _offset;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FallingDream.Player;

namespace FallingDream.Camera
{
    public class SimpleCameraMovement : MonoBehaviour
    {
        public GameObject FollowingObject;
        public float CameraOffsetX = 0;
        public float CameraOffsetY = 30f;
        public float CameraOffsetZ = 30f;

        private Vector3 _yVec;
        private Vector3 _xVec;
        private Vector3 _zVec;

        void Start()
        {
            _xVec = new Vector3(CameraOffsetX, 0, 0);
            _yVec = new Vector3(0, CameraOffsetY, 0);
            _zVec = new Vector3(0, 0, CameraOffsetZ);
        }

        void Update()
        {
            gameObject.transform.position = FollowingObject.transform.position + _xVec + _yVec + _zVec;
            gameObject.transform.LookAt(FollowingObject.transform);
        }
    }
}
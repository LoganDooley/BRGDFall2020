using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FallingDream.Player;

namespace FallingDream.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Following Object")]
        public GameObject FollowingObject;

        [Header("Position Properties")]
        public float CameraRadian;
        public float CameraRadius;
        public float RadiusMargin;
        public float YAxisMargin;

        [Header("Circular Movement Properties")]
        public float C_Acceleration;
        public float C_Velocity;
        public float C_VelocityCap;

        [Header("Radius Movement Properties")]
        public float R_Acceleration;
        public float R_Velocity;
        public float R_VelocityCap;

        private PlayerMovement _player;
        private float _radianDiff;
        private float _radiusDiff;

        void Start()
        {
            _player = FollowingObject.GetComponent<PlayerMovement>();
            _radianDiff = 0;
            _radiusDiff = 0;
        }

        void Update()
        {
            if (CameraRadian != _player.CharRadian ||
                CameraRadius != _player.CharRadius)
            {
                _radianDiff = _player.CharRadian - CameraRadian;
                _radiusDiff = _player.CharRadius - CameraRadius;

                if (_radianDiff != 0)
                {
                    if (_player.IsMovingCircular || Math.Abs(_radianDiff) > Math.PI / 16)
                    {
                        C_Velocity += _radianDiff / Math.Abs(_radianDiff) * C_Acceleration * Time.deltaTime;

                        if (C_Velocity > C_VelocityCap)
                            C_Velocity = C_VelocityCap;
                        else if (C_Velocity < C_VelocityCap * -1)
                            C_Velocity = C_VelocityCap * -1;

                        if (Math.Abs(_radianDiff) < Math.PI / 16 && Math.Abs(C_Velocity) > Math.Abs(_player.CurrentCircularSpeed))
                        {
                            C_Velocity = _player.CurrentCircularSpeed * _radianDiff / Math.Abs(_radianDiff);
                        }
                    }
                    else
                    {
                        C_Velocity -= C_Velocity / 2 * Time.deltaTime;
                        if ((_radianDiff > 0 && C_Velocity < 0) || (_radianDiff < 0 && C_Velocity > 0))
                            C_Velocity = 0;
                    }
                }

                if (_radiusDiff != 0)
                {
                    if (_player.IsMovingRadius || Math.Abs(_radiusDiff) > Math.Abs(R_Velocity))
                    {
                        R_Velocity += _radiusDiff / Math.Abs(_radiusDiff) * R_Acceleration * Time.deltaTime;

                        if (R_Velocity > R_VelocityCap)
                            R_Velocity = R_VelocityCap;
                        else if (R_Velocity < R_VelocityCap * -1)
                            R_Velocity = R_VelocityCap * -1;

                        if (Math.Abs(_radiusDiff) < 2 && Math.Abs(R_Velocity) > Math.Abs(_player.CurrentRadiusSpeed))
                        {
                            R_Velocity = _player.CurrentRadiusSpeed * _radiusDiff / Math.Abs(_radiusDiff);
                        }
                    }
                    else
                    {
                        R_Velocity -= R_Velocity / 2 * Time.deltaTime;
                        if ((_radiusDiff > 0 && R_Velocity < 0) || (_radiusDiff < 0 && R_Velocity > 0))
                            R_Velocity = 0;
                    }
                }

                CameraRadian += C_Velocity;
                CameraRadius += R_Velocity;

                gameObject.transform.position = PositionCalculate(CameraRadian, CameraRadius);
            }
        }

        private Vector3 PositionCalculate(float radian, float radius)
        {
            float xPos = (radius + RadiusMargin) * (float)Math.Cos(radian);
            float zPos = (radius + RadiusMargin) * (float)Math.Sin(radian);

            return new Vector3(xPos, YAxisMargin, zPos);
        }
    }
}
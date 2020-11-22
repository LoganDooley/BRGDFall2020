using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FallingDream.Player
{
    public class SimplePlayerMovement : MonoBehaviour
    {
        [Header("Standard Settings")]
        public float Speed = 0.05f;
        public float MaxDisplacement = 10f;
        public float LeanFactor = .1f;
        public float MaxLeanDeg = 15f;
        private float MaxLeanDegInv = 360f;

        void Start()
        {
            PositionCalculate(0, 0);
            MaxLeanDegInv = 360f - MaxLeanDeg;
        }

        void Update()
        {
            bool isMovingX = false;
            bool isMovingZ = false;
            float SpeedNormed = Speed * Time.deltaTime;

            float deltaX = 0;
            float deltaZ = 0;
            float deltaLeanX = 0;
            float deltaLeanZ = 0;

            float posX = gameObject.transform.position.x;
            float posZ = gameObject.transform.position.z;
            float rotateX = gameObject.transform.rotation.eulerAngles.x;
            float rotateZ = gameObject.transform.rotation.eulerAngles.z;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                isMovingX = true;

                if (posX < MaxDisplacement)
                {
                    deltaX = SpeedNormed;
                }

                if (rotateZ <= MaxLeanDeg || rotateZ >= MaxLeanDegInv)
                {
                    deltaLeanZ = GetLean(rotateZ, false);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                isMovingX = true;

                if (posX > -MaxDisplacement)
                {
                    deltaX = -SpeedNormed;
                }

                if (rotateZ <= MaxLeanDeg || rotateZ >= MaxLeanDegInv)
                {
                    deltaLeanZ = GetLean(rotateZ, true);
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                isMovingZ = true;

                if (posZ < MaxDisplacement)
                {
                    deltaZ = SpeedNormed;
                }

                if (rotateX <= MaxLeanDeg || rotateX >= MaxLeanDegInv)
                {
                    deltaLeanX = GetLean(rotateX, true);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                isMovingZ = true;

                if (posZ > -MaxDisplacement)
                {
                    deltaZ = -SpeedNormed;
                }

                if (rotateX <= MaxLeanDeg || rotateX >= MaxLeanDegInv)
                {
                    deltaLeanX = GetLean(rotateX, false);
                }
            }


            if (deltaX != 0 || deltaZ != 0)
            {
                gameObject.transform.position = PositionCalculate(deltaX, deltaZ);
            }

            if (deltaLeanX == 0 && !isMovingZ)
            {
                deltaLeanX = GetAutoLean(rotateX);
            }

            if (deltaLeanZ == 0 && !isMovingX)
            {
                deltaLeanZ = GetAutoLean(rotateZ);
            }

            LeanPlayer(deltaLeanX, 0, deltaLeanZ);
        }

        private Vector3 PositionCalculate(float deltaX, float deltaZ)
        {
            return gameObject.transform.position + new Vector3(deltaX, 0, deltaZ);
        }

        private void LeanPlayer(float x, float y, float z)
        {
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            x = AdjustDeltaLean(x, rot.x);
            y = AdjustDeltaLean(y, rot.y);
            z = AdjustDeltaLean(z, rot.z);
            gameObject.transform.Rotate(x, y, z);
        }

        private float AdjustDeltaLean(float deg, float currRot)
        {
            float next = currRot + deg;
            if (next < MaxLeanDegInv && next > MaxLeanDeg)
            {
                if (next - MaxLeanDeg > MaxLeanDegInv - next)
                {
                    deg = currRot - MaxLeanDegInv;
                }
                else
                {
                    deg = MaxLeanDeg - currRot;
                }
            }

            return deg;
        }

        private float GetAutoLean(float currRot)
        {
            float deltaLean = 0;

            if (currRot != 0)
            {
                if (currRot >= MaxLeanDegInv)
                {
                    deltaLean = GetLean(currRot, true);
                }
                else if (currRot <= MaxLeanDeg)
                {
                    deltaLean = GetLean(currRot, false);
                }
            }

            return deltaLean;
        }

        private float GetLean(float currRot, bool isPositive)
        {
            float lean = 0;

            if (isPositive)
            {
                if (currRot + LeanFactor >= 360f)
                {
                    // if it overshoots, then make the rotation 0
                    lean = 360f - currRot;
                }
                else
                {
                    lean = LeanFactor;
                }
            }
            else
            {
                if (currRot != 0 && currRot - LeanFactor <= 0f)
                {
                    // if it overshoots, then make the rotation 0
                    lean = -currRot;
                }
                else
                {
                    lean = -LeanFactor;
                }
            }

            return lean;
        }
    }
}
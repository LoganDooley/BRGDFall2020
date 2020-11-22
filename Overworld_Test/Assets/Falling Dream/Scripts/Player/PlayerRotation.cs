using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Facing Direction")]
        public GameObject FacingObject;

        [Header("Movement Rotation")]
        public float RotationDegreePerSecond = 18;
        public float RotationDegrees = 25;
        public float CurrentRotationAmount;

        private float timeCount = 0.0f;

        void Start()
        {

        }

        void Update()
        {
            gameObject.transform.LookAt(FacingObject.transform);


        }

        private void MovementRotation()
        {
            float baseAngle = transform.rotation.z - CurrentRotationAmount;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.AngleAxis(transform.rotation.z + (RotationDegreePerSecond * Time.deltaTime), Vector3.forward);
                CurrentRotationAmount = transform.rotation.z - baseAngle;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.AngleAxis(transform.rotation.z - (RotationDegreePerSecond * Time.deltaTime), Vector3.forward);
                CurrentRotationAmount = transform.rotation.z + baseAngle;
            }
            else
            {

            }

        }
    }
}
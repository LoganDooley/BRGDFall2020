using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.Camera
{
    public class CameraFocus : MonoBehaviour
    {
        [Header("Focused Object")]
        public GameObject FocusedObject;

        void Start()
        {
            gameObject.transform.LookAt(FocusedObject.transform);
        }


        void Update()
        {
            gameObject.transform.LookAt(FocusedObject.transform);
        }
    }
}
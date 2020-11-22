using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.System {

    public class InvisibleWallController : MonoBehaviour
    {
        public Material invisibleWallMaterial;

        void Update() {
            Vector3 p = this.transform.position;
            invisibleWallMaterial.SetVector("_PlayerPos", new Vector4(p.x, p.y, p.z, 1));
        }

    }
}
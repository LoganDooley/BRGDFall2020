using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FallingDream.Menus {

    public class MenuButton : MonoBehaviour
    {

        public string scene;

        public void TransitionToScene() {
            SceneManager.LoadScene(scene);
        }


    }
}
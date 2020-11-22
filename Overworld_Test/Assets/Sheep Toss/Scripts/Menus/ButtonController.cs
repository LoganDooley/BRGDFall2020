using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{

    public string sceneName = null;
    public int sceneId;

    public void TransitionToScene() {
        if (sceneName == null || sceneName == "") {
            SceneManager.LoadScene(sceneId);
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }

}
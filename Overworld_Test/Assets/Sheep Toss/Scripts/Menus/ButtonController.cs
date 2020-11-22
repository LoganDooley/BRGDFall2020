using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{

    public string sceneName;

    public void TransitionToScene() {
        SceneManager.LoadScene(sceneName);
    }

}
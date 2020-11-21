using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Animator transition;

    public void LoadGameSelect(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.9f);
        if(name == "Credits")
        {
            SceneManager.UnloadSceneAsync("Home");
        }
        else
        {
            SceneManager.UnloadSceneAsync("Credits");
        }
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
}

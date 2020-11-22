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

    public void LoadMiniGame(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.9f);
        if(name == "Credits")
        {
            SceneManager.UnloadSceneAsync("Home");
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("Credits");
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
    }
}

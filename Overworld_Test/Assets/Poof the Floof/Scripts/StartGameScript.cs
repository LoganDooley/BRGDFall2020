using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SheepCounting
{
    public class StartGameScript : MonoBehaviour
    {
        public void StartButton()
        {
            SceneManager.LoadScene("MainScene");
        }
        public void MainMenuButton()
        {
            SceneManager.LoadScene("HomeMusic");
        }
    }

}

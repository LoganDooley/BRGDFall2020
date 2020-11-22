using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SheepCounting
{
    public class PauseMenu : MonoBehaviour
    {

        GameObject[] pauseObjects;
        Scene currentscene;

        // Start is called before the first frame update
        void Start()
        {
            pauseObjects = GameObject.FindGameObjectsWithTag("PauseMenu");
            foreach (GameObject g in pauseObjects)
            {
                g.SetActive(false);
                Time.timeScale = 1;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    foreach (GameObject g in pauseObjects)
                    {
                        g.SetActive(true);
                    }
                }

                else if (Time.timeScale == 0)
                {
                    ContinueButton();
                }
            }
        }

        public void RestartButton()
        {
            currentscene = SceneManager.GetActiveScene();
            GameObject scoreHandler = GameObject.Find("ScoreHandler");
            Destroy(scoreHandler);
            SceneManager.LoadScene(currentscene.name);
            //Application.LoadLevel(Application.loadedLevel);
        }

        public void ContinueButton()
        {
            Time.timeScale = 1;
            foreach (GameObject g in pauseObjects)
            {
                g.SetActive(false);
            }
        }

        public void QuitButton()
        {
            //print("Game Quit");
            SceneManager.LoadScene("IntroScene");
        }
    }

}

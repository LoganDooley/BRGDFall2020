using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SheepCounting
{
    public class RestartScript : MonoBehaviour
    {   
        //The fade from black image for the scene
        public Image darkScreen;
        Color fadeColor;

        //Boolean for game restarting
        public bool restartFunc;

        // Start is called before the first frame update
        private void Start()
        {
            //Assign dark screen's color
            fadeColor = darkScreen.GetComponent<Image>().color;

            //Assign bool
            restartFunc = false;
        }
        public void Restart()
        {
            //Re-enable the fade to black dark screen and assign restart boolean
            darkScreen.GetComponent<Image>().enabled = true;
            fadeColor.a = 0;
            darkScreen.GetComponent<Image>().color = fadeColor;

            restartFunc = true;
            print("restart");
        }

        private void Update()
        {
            //If the game is restarting and the fade to black hasn't finished
            if (restartFunc == true && fadeColor.a < 1)
            {
                print(fadeColor.a);
                fadeColor.a += 0.6f * Time.deltaTime;
                darkScreen.GetComponent<Image>().color = fadeColor;
            }

            if (restartFunc == true && fadeColor.a >= 1)
            {
                //Delete previous game's score handler object so next one doesn't cause replication issues
                GameObject scoreHandler = GameObject.Find("ScoreHandler");
                Destroy(scoreHandler);

                //Load next game
                SceneManager.LoadScene("MainScene");
            }
        }

        public void Quit()
        {
            //Delete previous game's score handler object so next one doesn't cause replication issues
            GameObject scoreHandler = GameObject.Find("ScoreHandler");
            Destroy(scoreHandler);

            //Return to intro menu
            SceneManager.LoadScene("IntroScene");
        }
    }
}

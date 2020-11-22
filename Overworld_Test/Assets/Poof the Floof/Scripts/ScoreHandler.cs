using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SheepCounting
{
    public class ScoreHandler : MonoBehaviour
    {
        private Image barImage;
        public float losingThreshold = 0.3f;
        public float currentRatio = 1.0f;

        //Game objects from which to take variables for score reported at game over
        public GameObject CursorManager;
        public SheepDestroyer SheepDestroyer;
        public GameObject Stopwatch;
        public StopWatch Stopwatchscript;

        //Variables to keep for game over reporting
        public float updatedSheepRatio;
        public float timer;
        public float realsheepyeeted;
        public float fakesheepyeeted;

        //Boolean to check if the game has ended
        bool lossState;

        //Audio source for alarm clock SFX
        public AudioSource source;

        //The UI element that fades the game to black
        public Image darkScreen;
        Color fadeColor;

        private void Start()
        {
            //Make score handler object persist through game over so variables can be carried over
            DontDestroyOnLoad(this);

            //Set loss state boolean to false
            lossState = false;

            //Assign game objects and scripts for variable retrieval
            CursorManager = GameObject.Find("CursorManager");
            SheepDestroyer = CursorManager.GetComponent<SheepDestroyer>();
            Stopwatch = GameObject.Find("StopWatchText");
            Stopwatchscript = Stopwatch.GetComponent<StopWatch>();

            //Assign audio source for alarm clock sound and volume
            source = this.gameObject.GetComponent<AudioSource>();

            //Assign dark screen's color
            fadeColor = darkScreen.GetComponent<Image>().color;
        }

        // Update is called once per frame
        void Update()
        {
            //Only update if values exist to retrieve (i.e. if the game isn't over yet)
            if (lossState == false)
            {
                //Update the sheep ratio
                updatedSheepRatio = getSheepRatio();
                currentRatio = (1.0f < updatedSheepRatio) ? 1.0f : updatedSheepRatio;

                //Retrieve variables from reporting game objects
                timer = Stopwatchscript.timer;
                realsheepyeeted = SheepDestroyer.realSheepYeeted;
                fakesheepyeeted = SheepDestroyer.fakeSheepYeeted;

                //When the loss state is achieved, end the game and stop updating
                if (updatedSheepRatio <= losingThreshold)
                {
                    lossState = true;
                    //SceneManager.LoadScene("GameOver");
                    StartCoroutine(GameOver());
                }
            }

            //When the game is over, slowly reduce the alarm clock's volume to fade it out and fade to black
            if (lossState == true)
            {
                source.volume -= 0.2f * Time.deltaTime;
                if (darkScreen != null)
                {
                    fadeColor.a += 0.4f * Time.deltaTime;
                    darkScreen.GetComponent<Image>().color = fadeColor;
                }
            }
        }

        float getSheepRatio()
        {
            GameObject spawnerObj = GameObject.Find("Spawner");
            Spawner spawner = spawnerObj.GetComponent<Spawner>();

            if (spawner.realSheepCount + spawner.fakeSheepCount != 0f)
            {
                float val = (spawner.realSheepCount * 1.0f) / (spawner.realSheepCount + spawner.fakeSheepCount);
                return val;
            }
            else
            {
                return 0.5f;
            }
        }

        IEnumerator GameOver()
        {
            //Play alarm clock
            source.Play();
            //Wait a few seconds for players to realize the game is over
            yield return new WaitForSeconds(3);
            //End the game
            SceneManager.LoadScene("GameOver");
        }
    }
}

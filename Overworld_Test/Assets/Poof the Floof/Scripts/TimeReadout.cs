using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    public class TimeReadout : MonoBehaviour
    {
        public GameObject ScoreHandler;
        public ScoreHandler ScoringScript;
        public GameObject time;
        public Text txt;
        float timer;
        int ms;
        int sec;
        int min;
        int hour;

        // Start is called before the first frame update
        void Start()
        {
            //Assign score handler object and script from previous scene
            ScoreHandler = GameObject.Find("ScoreHandler");
            ScoringScript = ScoreHandler.GetComponent<ScoreHandler>();

            //Get timer from previous scene
            timer = ScoringScript.timer;

            //print(timer);

            //Calculate and print time from timer
            ms = (int) (timer * 1000) % 1000;
            string msString = ms.ToString("000");
            sec = (int) (timer % 60);
            //print(sec);
            string secstring = sec.ToString("00");
            min = (int) (timer / 60) % 60;
            //print(min);
            string minstring = min.ToString();
            hour = (int) timer / 3600;
            //print(hour);
            string hourstring = hour.ToString();
            txt.text = minstring + " : " + secstring + " : " + msString;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

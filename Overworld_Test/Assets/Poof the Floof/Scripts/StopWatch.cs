using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    public class StopWatch : MonoBehaviour
    {

        public float timer;
        private float millisec;
        private float seconds;
        private float minutes;
        private float hours;

        [SerializeField] private Text stopWatchText;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0;


        }

        // Update is called once per frame
        void Update()
        {
            StopWatchCalc();
        }

        void StopWatchCalc()
        {
            timer += Time.deltaTime;
            millisec = (int)((timer % 60) * 1000) % 1000;
            seconds = (int)(timer % 60);
            minutes = (int)(timer / 60) % 60;
            hours = (int)(timer / 3600);

            //stopWatchText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            //stopWatchText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + millisec.ToString("000");
            stopWatchText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}

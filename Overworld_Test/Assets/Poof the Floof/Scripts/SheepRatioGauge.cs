using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SheepCounting
{
    public class SheepRatioGauge : MonoBehaviour
    {
        private Transform gaugeTick;
        private const float ZERO_SPEED_ANGLE = 98;
        private const float MAX_SPEED_ANGLE = -105;
        float totalAngleSize = (ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE);

        float ratiodiff;
        float currentPlace;
        float currentFraction;

        //This object's audio source
        AudioSource source;

        //The score handler object and script
        GameObject scoreHandlerObj;
        ScoreHandler scoreHandler;

        //Warning sound boolean and threshold value
        bool warning;
        float warningThreshold;

        // Start is called before the first frame update
        void Start()
        {
            gaugeTick = transform.Find("GaugeHand");
            gaugeTick.eulerAngles = new Vector3(0, 0, ZERO_SPEED_ANGLE);

            //Find score handler object and script
            scoreHandlerObj = GameObject.Find("ScoreHandler");
            scoreHandler = scoreHandlerObj.GetComponent<ScoreHandler>();

            //Assign audio source for warning sounds and start as muted
            source = this.gameObject.GetComponent<AudioSource>();
            source.mute = true;

            //Calculate warning threshold: currently triggers at 2/3 progress toward losing
            warning = false;
            warningThreshold = scoreHandler.losingThreshold + ((1 - scoreHandler.losingThreshold) / 3);

            ratiodiff = 1 - scoreHandler.losingThreshold;
            currentPlace = scoreHandler.currentRatio - scoreHandler.losingThreshold;
        }

        // Update is called once per frame
        void Update()
        {
            //gaugeTick.eulerAngles = new Vector3(0, 0, (ZERO_SPEED_ANGLE - ((1 - scoreHandler.currentRatio) * totalAngleSize) * (1 / (scoreHandler.losingThreshold))));
            
            currentPlace = scoreHandler.currentRatio - scoreHandler.losingThreshold;
            currentFraction = currentPlace / (ratiodiff);

            gaugeTick.eulerAngles = new Vector3(0, 0, (ZERO_SPEED_ANGLE - ((1 - currentFraction) * totalAngleSize)));

            if (scoreHandler.currentRatio < scoreHandler.losingThreshold)
            {
                gaugeTick.eulerAngles = new Vector3(0, 0, MAX_SPEED_ANGLE);
            }

            //If the warning sound effect isn't currently playing, but the current sheep ratio is below the warning threshold assigned at start
            if (warning == false && scoreHandler.currentRatio < warningThreshold)
            {
                //Play the sound effect
                source.mute = false;
                warning = true;
            }

            //When the ratio goes back above the warning threshold after dipping below it
            if (warning == true && scoreHandler.currentRatio >= warningThreshold)
            {
                //Stop the sound effect
                source.Stop();
                warning = false;
            }
        }

        public float getRotation()
        {
            return ZERO_SPEED_ANGLE - (1 - scoreHandler.currentRatio) * totalAngleSize;
        }
    }
}

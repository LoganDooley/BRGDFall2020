using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    public class RealSheepReadout : MonoBehaviour
    {
        public GameObject ScoreHandler;
        public ScoreHandler ScoringScript;
        public GameObject time;
        public Text txt;
        float realcount;

        // Start is called before the first frame update
        void Start()
        {
            ScoreHandler = GameObject.Find("ScoreHandler");
            ScoringScript = ScoreHandler.GetComponent<ScoreHandler>();
            realcount = ScoringScript.realsheepyeeted;
            txt.text = realcount.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
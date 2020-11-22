using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    public class FakeSheepReadout : MonoBehaviour
    {
        public GameObject ScoreHandler;
        public ScoreHandler ScoringScript;
        public GameObject time;
        public Text txt;
        float fakecount;

        // Start is called before the first frame update
        void Start()
        {
            ScoreHandler = GameObject.Find("ScoreHandler");
            ScoringScript = ScoreHandler.GetComponent<ScoreHandler>();
            fakecount = ScoringScript.fakesheepyeeted;
            txt.text = fakecount.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
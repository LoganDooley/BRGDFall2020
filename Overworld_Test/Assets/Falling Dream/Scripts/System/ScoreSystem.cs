using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace FallingDream.System {
    public class ScoreSystem : MonoBehaviour
    {
        int score = 0;
        public Text scoreTxt;

        void Start()
        {
            updateScoreTxt();
        }

        private void updateScoreTxt() {
            scoreTxt.text = "Score: " + score;
        }

        public void IncrementScore() {
            score++;
            PlayerPrefs.SetInt("score", score);
            updateScoreTxt();

        }
    }
}

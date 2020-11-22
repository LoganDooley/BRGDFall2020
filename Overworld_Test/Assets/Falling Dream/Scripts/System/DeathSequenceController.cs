using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace FallingDream.System {

    public class DeathSequenceController : MonoBehaviour
    {

        public float waitSeconds = 3.0f;
        public Image blackOverlay;
        float currentTime = 0.0f;
        public float fadeTime = 0.6f;
        bool hasDied = false;

        public AudioSource music;

        void Start() {

        }

        void Update() {
            if (hasDied) {
                Debug.Log("Current Time");
                Debug.Log(currentTime);
                currentTime += Time.deltaTime;
                Color c = blackOverlay.color;
                blackOverlay.color = new Color(c.r, c.g, c.b, Mathf.Clamp(currentTime / fadeTime, 0, 1));
                if (currentTime > waitSeconds) {
                    SceneManager.LoadScene("EndMenu");
                }
            }
        }
        
        public void TriggerDeathSequence() {
            hasDied = true;
            blackOverlay.enabled = true;
            music.enabled = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FallingDream.Player {
    public class PlayerHealth : MonoBehaviour
    {
        public int health = 3;
        public Text healthTxt;
        public Image[] healthImages; 
        public Sprite healthLostSprite;
        public bool useImages;

        public FallingDream.System.DeathSequenceController deathSequenceController;

        void Update() { // TEMP

            if (Input.GetKeyDown(KeyCode.U)) {
                TakeDamage();
            }
        }

        void Start() {
            Assert.IsTrue(healthImages.Length == health);
            if (!useImages) {
                healthTxt.text = "Lives: " + health;
                for (int i = 0; i < healthImages.Length; i++) {
                    healthImages[i].gameObject.SetActive(false);
                }
            }
        }

        public void TakeDamage() {
            if (health > 0) {
                health--;
                if (useImages) {
                    healthImages[health].sprite = healthLostSprite;
                } else {
                    healthTxt.text = "Lives: " + health;
                }
            }
            if (health == 0) {
                Debug.Log("Gameover");
                deathSequenceController.TriggerDeathSequence();
            }
        }
    }
}

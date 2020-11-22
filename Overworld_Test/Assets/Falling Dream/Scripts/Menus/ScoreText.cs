using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

namespace FallingDream.Menus {


    public class ScoreText : MonoBehaviour
    {
            
        public Image[] numPlaces; // size 4
        public Sprite[] nums; // size 10



        void Start() {
            // PlayerPrefs.SetInt("score", 1234);
            int score = PlayerPrefs.GetInt("score");

            char[] scoreText = score.ToString().ToCharArray();
            Array.Reverse( scoreText );

            for (int i = 0; i < numPlaces.Length; i++) { // go through numPlaces in reverse
                int index = numPlaces.Length - (i + 1);

                if (i < scoreText.Length) {
                    int number = int.Parse(scoreText[i].ToString());
                    numPlaces[index].sprite = nums[number];
                } else {
                    numPlaces[index].sprite = nums[0];
                }
            }


        }


    }
}
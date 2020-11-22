using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{

    private Text scoreText;

    void Start() {
        
        scoreText = this.GetComponent<Text>();

        scoreText.text = "You tossed the sheep " + PlayerPrefs.GetInt("score", -1) + " meters!";
    }

}

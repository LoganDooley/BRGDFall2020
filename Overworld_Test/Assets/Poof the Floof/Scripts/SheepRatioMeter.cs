using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SheepCounting
{
    public class SheepRatioMeter : MonoBehaviour
    {
        private Image barImage;

        // Start is called before the first frame update
        void Start()
        {
            barImage = transform.Find("FullHealthBar").GetComponent<Image>();
            barImage.fillAmount = getSheepRatio();
        }

        // Update is called once per frame
        void Update()
        {

            if (barImage.fillAmount <= 0.5f){
                SceneManager.LoadScene("GameOver");
            }
            else if (barImage.fillAmount <= 1){
                // barImage.fillAmount -= 0.001f;
                barImage.fillAmount = getSheepRatio();
            }
        }

        float getSheepRatio()
        {
            GameObject spawnerObj = GameObject.Find("Spawner");
            Spawner spawner = spawnerObj.GetComponent<Spawner>();
            float val = (spawner.realSheepCount * 1.0f) / (spawner.realSheepCount + spawner.fakeSheepCount);
            return val;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.System {
    public class GradientScroll : MonoBehaviour
    {
        public Color[] rgbaList;
        public float secondsPerCycle = 60f;
        public Material mat;
        private Gradient grad;

        public float colorStartOffset;
        public float colorGradientOffset;

        public float time = 0;

        // Start is called before the first frame update
        void Start()
        {
            GenGradient();
        }

        // Update is called once per frame
        void Update()
        {
            EvalGradient();
        }

        void GenGradient()
        {
            time = 0.0f;

            if (rgbaList.Length > 0)
            {
                int numColors = rgbaList.Length;
                int numGradColors = numColors + 1;
                grad = new Gradient();
                GradientColorKey[] gck = new GradientColorKey[numGradColors];
                GradientAlphaKey[] gak = new GradientAlphaKey[numGradColors];

                float timeStep = 1f / numGradColors;
                for (int i = 0 ; i < numColors ; i++)
                {
                    float time = i * timeStep;
                    gck[i].color = rgbaList[i];
                    gck[i].time = time;
                    gak[i].alpha = rgbaList[i].a;
                    gak[i].time = time;
                }
                gck[numColors].color = rgbaList[0];
                gck[numColors].time = 1f;
                gak[numColors].alpha = rgbaList[0].a;
                gak[numColors].time = 1f;

                grad.SetKeys(gck, gak);
            }
        }

        void EvalGradient()
        {
            time += Time.deltaTime;
            float timeStep = time % secondsPerCycle / secondsPerCycle;
            Color newColor = grad.Evaluate((timeStep + colorStartOffset) % 1.0f);
            Color newColor2 = grad.Evaluate((timeStep + colorStartOffset + colorGradientOffset) % 1.0f);

            mat.SetColor("_ColorBot", newColor2);
            mat.SetColor("_ColorTop", newColor);

            // mat.color = newColor;
            // if (RenderSettings.skybox.HasProperty("_Tint"))
            //    RenderSettings.skybox.SetColor("_Tint", newColor);
            // else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            //    RenderSettings.skybox.SetColor("_SkyTint", newColor);
            // RenderSettings.skybox = mat;
            // foreach (Transform child in transform)
            // {
            //     child.GetComponent<Renderer>().material.color = grad.Evaluate(timeStep);
            // }
        }
    }
}

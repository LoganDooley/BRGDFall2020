using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{

    [SerializeField]
    public Slider slider;

    [SerializeField]
    SheepMove sheepMove;


    public void setMaxPower(float power)
    {
        slider.maxValue = power;

    }

    public void setPower(float power)
    {
        slider.value = power;
    }
}

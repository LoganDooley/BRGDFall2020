using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    public Button myButton;
    public void OnMouseOver()
    {
        print("mouse is over button");
        myButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1f); 
    }
    public void OnMouseExit()
    {
        print("mouse left button");
        myButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }
}

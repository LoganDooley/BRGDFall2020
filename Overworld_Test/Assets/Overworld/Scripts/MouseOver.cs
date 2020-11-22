using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MouseOver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button myButton;
    public AudioSource buttonPress;
    public AudioSource buttonDepress;
    public void OnMouseOver()
    {
        myButton.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1f); 
    }
    public void OnMouseExit()
    {
        myButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        buttonPress.Play();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");
        buttonDepress.Play();
    }
}

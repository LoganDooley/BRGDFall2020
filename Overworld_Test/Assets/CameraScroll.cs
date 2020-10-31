using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private bool transition;
    public float speed = 1f;
    private float lerp = 0, duration = 1, time = 0;
    private Vector3 homePos, selectPos;
    private int mode = 1;
    private void Start()
    {
        transition = false;
        homePos = transform.position;
        selectPos = transform.position - new Vector3(0, 2 * transform.position.y, 0);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && transition == false)
        {
            print("space was hit");
            transition = true;
            mode = mode * -1;
            time = 0;
        }
        if (transition & mode == -1)
        {
            time += Time.deltaTime/2;
            lerp = (6f/Mathf.Pow(duration, 3))*((duration/2)*Mathf.Pow(time, 2)-(1f/3f)*Mathf.Pow(time, 3));
            print(lerp);
            transform.position = Vector3.Lerp(homePos, selectPos, lerp);
            if(lerp >= 1f-0.01f && lerp <= 1f+0.01f)
            {
                transform.position = selectPos;
                transition = false;
                lerp = 0;
            }
        }
        if(transition & mode == 1)
        {
            time += Time.deltaTime/2;
            lerp = (6f / Mathf.Pow(duration, 3)) * ((duration / 2) * Mathf.Pow(time, 2) - (1f / 3f) * Mathf.Pow(time, 3));
            print(lerp);
            transform.position = Vector3.Lerp(selectPos, homePos, lerp);
            if (lerp >= 1f - 0.01f && lerp <= 1f + 0.01f)
            {
                transform.position = homePos;
                transition = false;
                lerp = 0;
            }
        }
    }
}

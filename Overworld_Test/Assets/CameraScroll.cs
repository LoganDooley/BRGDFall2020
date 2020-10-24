using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private bool transition;
    public float speed = 1f;
    private float lerp = 0, duration = 1;
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
        }
        if (transition & mode == -1)
        {
            lerp += speed*Time.deltaTime / (duration* (0.2f+Mathf.Abs(lerp - 0.5f)));
            transform.position = Vector3.Lerp(homePos, selectPos, lerp);
            if(transform.position == selectPos)
            {
                transition = false;
                lerp = 0;
            }
        }
        if(transition & mode == 1)
        {
            lerp += speed *Time.deltaTime / (duration* (0.2f+Mathf.Abs(lerp - 0.5f)));
            transform.position = Vector3.Lerp(selectPos, homePos, lerp);
            if (transform.position == homePos)
            {
                transition = false;
                lerp = 0;
            }
        }
    }
}

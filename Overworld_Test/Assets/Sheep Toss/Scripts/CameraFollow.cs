using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform sheep;

    public float offset_x = 3.5f;
    public float offset_y = 3.0f;
    public float offset_y_top = 2.0f;

    private void LateUpdate()
    {
        if (transform.position.y < sheep.position.y - offset_y_top)
        {
            transform.position = new Vector3(transform.position.x, sheep.position.y - offset_y_top, transform.position.z);
        }
        else if (transform.position.y > sheep.position.y + offset_y)
        {
            transform.position = new Vector3(transform.position.x, sheep.position.y + offset_y, transform.position.z);
        }

        if (transform.position.x < sheep.position.x - offset_x)
        {
            transform.position = new Vector3(sheep.position.x - offset_x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > sheep.position.x + offset_x)
        {
            transform.position = new Vector3(sheep.position.x + offset_x, transform.position.y, transform.position.z);
        }
    }
}

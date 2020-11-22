using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    public Camera mainCamera;
    private float spriteWidth;
    public int numAhead = 0;
    public int totalBgs = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = this.GetComponentInChildren<SpriteRenderer>().bounds;

        this.spriteWidth = bounds.size.x;
        this.transform.Translate(new Vector3(this.spriteWidth * this.numAhead, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mainCamera.transform.position.x - this.transform.position.x > this.spriteWidth)
        {
            this.transform.Translate(new Vector3(this.spriteWidth * this.totalBgs, 0f, 0f));
        }
    }
}

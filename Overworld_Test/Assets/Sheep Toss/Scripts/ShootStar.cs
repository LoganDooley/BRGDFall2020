using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStar : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = new Vector2(0.0f, speed);
        Invoke("intoBackground", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void intoBackground()
    {
        this.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Background 0");
        this.rb.velocity = new Vector2(7f, 0.0f);
        this.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        this.transform.Rotate(new Vector3(0f, 0f, 235f), Space.World);
        Invoke("kill", 1.5f);
    }

    void kill()
    {
        Destroy(gameObject);
    }
}

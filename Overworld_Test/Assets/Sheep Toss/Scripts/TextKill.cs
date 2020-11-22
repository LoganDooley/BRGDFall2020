using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextKill : MonoBehaviour
{
    public int textType;
    private float opacity = 1;
    public Sprite hit1;
    public Sprite hit2;
    public Sprite hit3;
    public Sprite miss;
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = new Vector2(speed, speed);

        if (textType == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = miss;
        }
        else if (textType == 1)
        {
            int successType = Random.Range(0, 3);
            if(successType == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = hit1;
            }
            else if(successType == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = hit2;
            }
            else if(successType == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = hit3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opacity <= 0)
        {
            Kill();
        }

     

        
    }

    private void FixedUpdate()
    {
        opacity = opacity - 0.01f;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacity);
    }

    public void setType(int type)
    {
        textType = type;
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }
}

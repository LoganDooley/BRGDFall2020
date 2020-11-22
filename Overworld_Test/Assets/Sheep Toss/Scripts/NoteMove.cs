using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float speed = 17f;
    public Rigidbody2D rb;
    public SheepMove sm;
    private bool hittable = false;
    private Vector2 origin;
    public float successAmt = 5;
    public float failAmt = -10;
    public GameObject note;
    public GameObject prevNote;

    

    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = new Vector2(-speed, 0.0f);
        this.origin = new Vector2(this.rb.position.x, this.rb.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
